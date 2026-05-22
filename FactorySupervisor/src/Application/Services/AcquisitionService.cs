using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO: 后续接入ILogger
namespace FactorySupervisor.src.Application.Services
{
    public sealed class AcquisitionService
    {
        private readonly IDeviceRepository _deviceRepo;
        private readonly ITagRepository _tagRepo;
        private readonly IProtocolClientFactory _clientFactory;
        private readonly ITagValueCache _tagValueCache;
        private readonly ITagHistoryRepository? _tagHistoryRepository;

        //private readonly ILogger<AcquisitionService> _logger;

        private readonly Dictionary<Guid, DateTimeOffset> _lastHistoryWriteTimes = new();

        public AcquisitionService(
            IDeviceRepository deviceRepository,
            ITagRepository tagRepository,
            IProtocolClientFactory clientFactory,
            ITagValueCache tagValueCache,
            ITagHistoryRepository tagHistoryRepository)
            //ILogger logger)
        {
            _deviceRepo = deviceRepository;
            _tagRepo = tagRepository;
            _clientFactory = clientFactory;
            _tagValueCache = tagValueCache;
            _tagHistoryRepository = tagHistoryRepository;
           // _logger = logger;
        }

        public async Task RunAsync(CancellationToken ct)
        {
            try 
            { 
                while (!ct.IsCancellationRequested)
                {
                    var devices = await _deviceRepo.GetEnableDevicesAsync(ct);
                    foreach(var device in devices)
                    {
                        ct.ThrowIfCancellationRequested();
                        await PollDeviceAsync(device, ct);
                    }
                    
                }
            
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                throw;
            }
            
        }

        private void MarkDeviceBad(IReadOnlyList<Tag> tags,string error)
        {
            var now=DateTimeOffset.Now;
            foreach(var tag in tags)
            {
                _tagValueCache.Set(TagValue.Bad(tag.Id, tag.DeviceId, error, now));
            }
        }

        private bool ShouldWriteHistory(Tag tag,DateTimeOffset now)
        {
            if(!_lastHistoryWriteTimes.TryGetValue(tag.Id, out var lastWriteTime))
            {
                _lastHistoryWriteTimes[tag.Id] = now;
                return true;
            }

            if ((now - lastWriteTime).TotalMilliseconds < tag.ScanMs)
            {
                return false;
            }
            _lastHistoryWriteTimes[tag.Id] = now;
            return true;
        }


        private async Task PollDeviceAsync(Device device, CancellationToken ct)
        {
            try
            {
                var tags = await _tagRepo.GetByDeviceAsync(device.Id, ct);
                if (tags.Count == 0) return;

                await using var client = _clientFactory.Create(device.ProtocolType);

                ct.ThrowIfCancellationRequested();

                var conn = await client.ConnectAsync(device, ct);
                if (!conn.IsSuccess)
                {
                    MarkDeviceBad(tags, conn.Error ?? "连接失败");
                    return;
                }

                ct.ThrowIfCancellationRequested();

                var read = await client.ReadAsync(tags, ct);

                var tagMap = tags.ToDictionary(t => t.Id);
                var now = DateTimeOffset.Now;

                foreach (var v in read.Values)
                {
                    ct.ThrowIfCancellationRequested();

                    _tagValueCache.Set(v);

                    if (_tagHistoryRepository is not null &&
                        tagMap.TryGetValue(v.TagId, out var tag) &&
                        ShouldWriteHistory(tag, now))
                    {
                        await _tagHistoryRepository.InsertAsync(v, ct);
                    }
                }

                if (!read.Success)
                {
                    Console.WriteLine($"Read failed: {device.Name}, {read.Error}");
                }
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                // 正常 Stop，交给 RunAsync 外层结束，不标 Bad。
                throw;
            }
            catch (Exception ex)
            {              
                var tags = await _tagRepo.GetByDeviceAsync(device.Id, CancellationToken.None);
                MarkDeviceBad(tags, ex.Message);
            }
        }
    }
}
