using FactorySupervisor.src.Contracts;
using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using S7.Net;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Protocol
{
    public sealed class S7ProtocolClient : IProtocolClient
    {
        private Plc? _plc;
        private string? _connectError;
        private Device? _device;

        public async Task<ConnectResult> ConnectAsync(Device device, CancellationToken ct = default)
        {
            var now=DateTimeOffset.Now;
            try
            {
                if (device.ProtocolType != Domain.Enums.ProtocolType.S7)
                {
                    return new ConnectResult(false, "设备使用的协议不是 S7", now);
                }

                if (string.IsNullOrWhiteSpace(device.Ip))
                {
                    return new ConnectResult(false, "S7 设备 IP 不能为空", now);
                }

                if (string.IsNullOrWhiteSpace(device.S7CpuType))
                {
                    return new ConnectResult(false, "S7 CPU 类型不能为空", now);
                }

                if (device.S7Rack is null || device.S7Slot is null)
                {
                    return new ConnectResult(false, "S7 Rack / Slot 不能为空", now);
                }

                _device = device;

                var cpuType = ParseCpuType(device.S7CpuType);
                var rack=(short)(device.S7Rack ?? 0);
                var slot=(short)(device.S7Slot ?? 1);

                _plc=new Plc(cpuType, device.Ip, rack, slot);
                await _plc.OpenAsync(ct).WaitAsync(ct);
                _connectError =null;
                return new ConnectResult(true, null, DateTimeOffset.Now);
            }
            catch(Exception ex)
            {
                _connectError=ex.Message;
                return new ConnectResult(false, _connectError, DateTimeOffset.Now);
            }
        }

        private CpuType ParseCpuType(string? s7CpuType)
        {
            return s7CpuType switch
            {
                "S7-200" => CpuType.S7200,
                "S7-200Smart" => CpuType.S7200Smart,
                "S7-300" => CpuType.S7300,
                "S7-400" => CpuType.S7400,
                "S7-1200" => CpuType.S71200,
                "S7-1500" => CpuType.S71500,
                _ => throw new ArgumentException($"Unsupported S7 CPU type: {s7CpuType}")
            };
        }

        public Task DisconnectAsync(CancellationToken ct = default)
        {
           _plc?.Close();
            _plc = null;
            _device = null;
            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            await DisconnectAsync();
        }

        public async Task<BatchReadResult> ReadAsync(IReadOnlyList<Tag> tags, CancellationToken ct = default)
        {
            var now =DateTimeOffset.Now;

            if(_plc is null || !_plc.IsConnected|| _device is null)
            {
                var bad=tags
                    .Select(t=>TagValue.Bad(t.Id, t.DeviceId, _connectError ?? "未连接", now))
                    .ToList();

                return new BatchReadResult(false, bad, _connectError ?? "未连接", now);
            }

            var results = new List<TagValue>(tags.Count);

            foreach(var tag in tags)
            {
                try
                {
                    var value = await ReadOneAsync(tag, ct);
                    results.Add(TagValue.Good(tag.Id, tag.DeviceId, value, now));
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch(Exception ex)
                {
                    results.Add(TagValue.Bad(tag.Id, tag.DeviceId, ex.Message, now));
                }
            }

            var allBad=results.All(x=>x.Quality==Domain.Enums.TagQuality.Bad);
            return  new BatchReadResult(!allBad, results, allBad ? "全部读取失败" : null, now);
        }

        public async Task<WriteResult> WriteAsync(Tag tag, object value, CancellationToken ct = default)
        {
            var now = DateTimeOffset.Now;

            if (_plc is null || !_plc.IsConnected)
            {
                return new WriteResult(false, "S7 设备未连接", now);
            }

            try
            {
                var writeValue = ConvertWriteValue(value, tag.DataType);
                await _plc.WriteAsync(tag.Address, writeValue).WaitAsync(ct);

                return new WriteResult(true, null, now);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return new WriteResult(false, ex.Message, now);
            }

        }


        private async Task<object?> ReadOneAsync(Tag tag, CancellationToken ct)
        {
            var raw = await _plc!.ReadAsync(tag.Address).WaitAsync(ct);

            return tag.DataType switch
            {
                TagDataType.Bool => Convert.ToBoolean(raw),
                TagDataType.Int16 => unchecked((short)Convert.ToUInt16(raw)),
                TagDataType.UInt16 => Convert.ToUInt16(raw),
                TagDataType.Int32 => Convert.ToInt32(raw),
                TagDataType.UInt32 => Convert.ToUInt32(raw),
                TagDataType.Float => Convert.ToSingle(raw),
                TagDataType.Double => Convert.ToDouble(raw),
                TagDataType.String => raw?.ToString() ?? "",
                _ => raw
            };
        }

        private static object ConvertWriteValue(object value, TagDataType dataType)
        {
            return dataType switch
            {
                TagDataType.Bool => Convert.ToBoolean(value),
                TagDataType.Int16 => Convert.ToInt16(value),
                TagDataType.UInt16 => Convert.ToUInt16(value),
                TagDataType.Int32 => Convert.ToInt32(value),
                TagDataType.UInt32 => Convert.ToUInt32(value),
                TagDataType.Float => Convert.ToSingle(value),
                TagDataType.Double => Convert.ToDouble(value),
                TagDataType.String => value.ToString() ?? "",
                _ => value
            };
        }
    }
}
