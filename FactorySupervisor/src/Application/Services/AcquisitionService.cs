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
        
        //private readonly ILogger<AcquisitionService> _logger;

        public AcquisitionService(
            IDeviceRepository deviceRepository,
            ITagRepository tagRepository,
            IProtocolClientFactory clientFactory,
            ITagValueCache tagValueCache)
            //ILogger logger)
        {
            _deviceRepo = deviceRepository;
            _tagRepo = tagRepository;
            _clientFactory = clientFactory;
            _tagValueCache = tagValueCache;
           // _logger = logger;
        }

        public async Task RunAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                var devices = await _deviceRepo.GetEnableDevicesAsync(ct);
                foreach(var device in devices)
                {
                    try
                    {
                        var tags = await _tagRepo.GetByDeviceAsync(device.Id, ct);
                        if (tags.Count == 0) continue;

                        await using var client = _clientFactory.Create(device.ProtocolType);

                        var conn = await client.ConnectAsync(device, ct);
                        if (!conn.IsSuccess)
                        {
                            MarkDeviceBad(tags, conn.Error ?? "连接失败");
                            continue;
                        }

                        var read = await client.ReadAsync(tags, ct);
                        foreach(var v in read.Values)
                        {
                            _tagValueCache.Set(v);
                        }

                        if (!read.Success)
                            // _logger.LogWarning("Read partial/failed, device={Device}, error={Error}", device.Name, read.Error);
                            Console.WriteLine($"Read failed: {device.Name}, {read.Error}");
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch(Exception ex) 
                    {
                        //_logger.LogError(ex, "Acquisition loop failed, device={Device}", device.Name);
                        Console.WriteLine($"Acquisition failed: {device.Name}, {ex.Message}");

                        // 防止异常中断采集：把该设备点位置 Bad
                        var tags = await _tagRepo.GetByDeviceAsync(device.Id, ct);
                        MarkDeviceBad(tags, ex.Message);
                    }
                }


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
    }
}
