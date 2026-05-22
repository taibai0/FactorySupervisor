using FactorySupervisor.src.Contracts;
using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using Modbus;
using Modbus.Device;
using Modbus.Message;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices; // 添加此 using 指令以解决 CS0246: 未能找到类型或命名空间名“ModbusFactory”(是否缺少 using 指令或程序集引用?)
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Protocol
{
    public sealed class ModBusRtuProtocolClient : IProtocolClient
    {
        private SerialPort? _port;
        private IModbusSerialMaster? _master;
        private Device? _device;

        public Task<ConnectResult> ConnectAsync(Device device, CancellationToken ct = default)
        {
            try
            {
                if (device.ProtocolType != Domain.Enums.ProtocolType.ModbusRtu)
                    return Task.FromResult(new ConnectResult(false, "设备使用的协议不是modbusRTU", DateTimeOffset.UtcNow));

                if (string.IsNullOrWhiteSpace(device.ComPort))
                    return Task.FromResult(new ConnectResult(false, "设备的串口号不能为空", DateTimeOffset.UtcNow));

                _device = device;

                _port = new SerialPort(
                    device.ComPort,
                    device.BaudRate,
                    ParseParity(device.Parity),
                    device.DataBits,
                    ParseStopBits(device.StopBits)
                    )
                {
                    ReadTimeout = device.TimeoutMs,
                    WriteTimeout = device.TimeoutMs
                };
                _port.Open();
                _master = ModbusSerialMaster.CreateRtu(_port);
                _master.Transport.ReadTimeout = device.TimeoutMs;
                _master.Transport.WriteTimeout = device.TimeoutMs;
                _master.Transport.Retries = 0; // 不重试，直接返回错误

                return Task.FromResult(new ConnectResult(true, null, DateTimeOffset.Now));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ConnectResult(false, $"连接失败: {ex.Message}", DateTimeOffset.Now));
            }
        }


        public Task DisconnectAsync(CancellationToken ct = default)
        {
            _master?.Dispose();
            _master = null;

            if(_port is not null)
            {
                if(_port.IsOpen)
                    _port.Close();
                _port.Dispose();
                _port = null;
            }

            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            await DisconnectAsync();
        }

        public  async Task<BatchReadResult> ReadAsync(IReadOnlyList<Tag> tags, CancellationToken ct = default)
        {
            var now = DateTimeOffset.Now;

            if(_master is null||_device is null)
            {
                var bad=tags.Select(t=>TagValue.Bad(t.Id,t.DeviceId, "设备未连接", now)).ToList();
                return new BatchReadResult(false,bad, "设备未连接", now);
            }

            var results = new List<TagValue>(tags.Count);

            foreach (var tag in tags)
            {              
                try
                {
                    var addr = ModbusAddressParser.Parse(tag.Address);
                    var value = await ReadOneAsync(_master, _device.UnitId, addr, tag.DataType, ct);
                    results.Add(TagValue.Good(tag.Id, tag.DeviceId, value, DateTimeOffset.Now));
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    results.Add(TagValue.Bad(tag.Id, tag.DeviceId, $"读取失败: {ex.Message}", now));
                }
            }

            var allBad=results.All(x=>x.Quality==TagQuality.Bad);
            return new BatchReadResult(!allBad, results, allBad? "所有标签读取失败":null, now);
        }

        private async Task<object> ReadOneAsync(
            IModbusSerialMaster master,
            byte unitId,
            ModbusAddress address, 
            TagDataType dataType, 
            CancellationToken ct)
        {
           if(address.Area is ModbusArea.Coil or ModbusArea.DiscreteInput)
            {
                bool[] bits = address.Area == ModbusArea.Coil
                    ? await master.ReadCoilsAsync(unitId, address.Offset, 1).WaitAsync(ct)
                    : await master.ReadInputsAsync(unitId,address.Offset, 1).WaitAsync(ct);

                return bits[0];
            }

            var count = GetRegisterCount(dataType);          
            ushort[] regs = address.Area == ModbusArea.InputRegister
                ? await master.ReadInputRegistersAsync(unitId, address.Offset, count).WaitAsync(ct)
                : await master.ReadHoldingRegistersAsync(unitId, address.Offset, count).WaitAsync(ct);

            return ConvertFromRegisters(regs, dataType);
        }


        //把从 Modbus 读回来的寄存器数组，转换成 C# 原始对象
        private object ConvertFromRegisters(ushort[] regs, TagDataType t)
        {
            if (t == TagDataType.Bool) return regs[0] != 0;
            if (t == TagDataType.UInt16) return regs[0];
            if(t==TagDataType.Int16) return unchecked((short)regs[0]);

            var bytes= RegistersToBigEndianBytes(regs);
            return t switch
            {
                TagDataType.Int32 => BinaryPrimitives.ReadInt32BigEndian(bytes),
                TagDataType.UInt32 => BinaryPrimitives.ReadUInt32BigEndian(bytes),
                TagDataType.Float => BitConverter.Int32BitsToSingle(BinaryPrimitives.ReadInt32BigEndian(bytes)),
                TagDataType.Double => BitConverter.Int64BitsToDouble(BinaryPrimitives.ReadInt64BigEndian(bytes)),
                TagDataType.String => Encoding.ASCII.GetString(bytes).TrimEnd('\0', ' '),
                _ => regs[0]
            };
        }


        //把寄存器数组按大端序转换成字节数组
        private byte[] RegistersToBigEndianBytes(ushort[] regs)
        {
            var bytes = new byte[regs.Length * 2];
            for(var i=0;i<regs.Length; i++)
            {
                bytes[i*2]=(byte)(regs[i] >> 8);
                bytes[i*2+1]=((byte)(regs[i] & 0xff));
            }
            return bytes;
        }

        //获取不同数据类型需要的寄存器数量
        private ushort GetRegisterCount(TagDataType t) => t switch
        {
            TagDataType.Bool => 1,
            TagDataType.Int16 => 1,
            TagDataType.UInt16 => 1,
            TagDataType.Int32 => 2,
            TagDataType.UInt32 => 2,
            TagDataType.Float => 2,
            TagDataType.Double => 4,
            TagDataType.String => 8,
            _ => 1
        };
       

        public async Task<WriteResult> WriteAsync(Tag tag, object value, CancellationToken ct = default)
        {
            var now = DateTimeOffset.Now;
            if (_master is null || _device is null)
                return new WriteResult(false, "设备未连接", now);

            try
            {
                var addr= ModbusAddressParser.Parse(tag.Address);
                if (addr.Area == ModbusArea.Coil)
                {
                    var b=Convert.ToBoolean(value??false);
                    await _master.WriteSingleCoilAsync(_device.UnitId, addr.Offset, b).WaitAsync(ct);
                    return new WriteResult(true, null, DateTimeOffset.Now);
                }

                if (addr.Area == ModbusArea.HoldingRegister)
                {
                    var registers = ConvertToRegisters(value, tag.DataType);
                    if (registers.Length == 1)
                    {
                        await _master.WriteSingleRegisterAsync(_device.UnitId, addr.Offset, registers[0]).WaitAsync(ct);
                    }
                    else
                    {
                        await _master.WriteMultipleRegistersAsync(_device.UnitId, addr.Offset, registers).WaitAsync(ct);
                    }
                }
                return new WriteResult(true, null, DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                return new WriteResult(false, $"写入失败: {ex.Message}", now);
            }
        }

        private static ushort[] ConvertToRegisters(object? value, TagDataType t)
        {
            return t switch
            {
                TagDataType.Bool => new[] { (ushort)(Convert.ToBoolean(value) ? 1 : 0) },
                TagDataType.UInt16 => new[] { (ushort)(Convert.ToUInt16(value)) },
                TagDataType.Int16 => new[] { unchecked((ushort)(Convert.ToInt16(value))) },
                TagDataType.Int32 => BigEndianBytesToRegisters(BitConverter.GetBytes(Convert.ToInt32(value)).Reverse().ToArray()),
                TagDataType.UInt32 => BigEndianBytesToRegisters(BitConverter.GetBytes(Convert.ToUInt32(value)).Reverse().ToArray()),
                TagDataType.Float => BigEndianBytesToRegisters(BitConverter.GetBytes(Convert.ToSingle(value)).Reverse().ToArray()),
                TagDataType.Double => BigEndianBytesToRegisters(BitConverter.GetBytes(Convert.ToDouble(value)).Reverse().ToArray()),
                _ => throw new NotSupportedException($"输入的数据类型暂不支持: {t}")
            };
        }

        private static ushort[] BigEndianBytesToRegisters(byte[] bytes)
        {
            if (bytes.Length % 2 != 0)
                throw new ArgumentException("字节长度必须是二的整数倍");

            var regs = new ushort[bytes.Length / 2];
            for(var i = 0; i < regs.Length; i++)
            {
                regs[i] = (ushort)((bytes[i * 2] << 8) | bytes[i*2+1]);
            }
            return regs;
        }

        private static Parity ParseParity(string s) =>
            Enum.TryParse<Parity>(s, out var p) ? p : Parity.None;

        private static StopBits ParseStopBits(string s) =>
            Enum.TryParse<StopBits>(s, true, out var sb) ? sb : StopBits.One;
    }
}
