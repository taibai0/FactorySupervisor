using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using FactorySupervisor.src.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Repositories
{
    public sealed class SqlDeviceRepository : IDeviceRepository
    {
        public Task<IReadOnlyList<Device>> GetEnableDevicesAsync(CancellationToken ct = default)
        {
            const string sql =
                """
                SELECT
                    Id,
                    Name,
                    ProtocolType,
                    Ip,
                    Port,
                    Enabled,
                    ComPort,
                    BaudRate,
                    DataBits,
                    Parity,
                    StopBits,
                    UnitId,
                    S7CpuType,
                    S7Rack,
                    S7Slot,
                    TimeoutMs
                FROM dbo.DeviceConfig
                WHERE Enabled = 1
                ORDER BY Name;
                """;

            return DbHelper.QueryAsync(sql, reader =>
            {
                Enum.TryParse(
                    reader.GetString(reader.GetOrdinal("ProtocolType")),
                    out Domain.Enums.ProtocolType protocolType);

                var name=reader.GetString(reader.GetOrdinal("Name"));
                var device = new Device(
                     name,
                     protocolType,
                     ReadRequiredString(reader, "Ip", $"设备 {name} 缺少 IP 配置"),
                     reader.IsDBNull(reader.GetOrdinal("Port"))
                         ? 0
                         : reader.GetInt32(reader.GetOrdinal("Port")),
                     reader.GetBoolean(reader.GetOrdinal("Enabled")))
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id"))
                };

                if (protocolType == ProtocolType.ModbusRtu)
                {
                    device.ConfigureModbusRtu(
                        comPort: ReadRequiredString(reader, "ComPort", $"设备 {name} 缺少 Modbus RTU 串口配置"),
                        baudRate: ReadRequiredInt(reader, "BaudRate", $"设备 {name} 缺少 Modbus RTU 波特率配置"),
                        dataBits: ReadRequiredInt(reader, "DataBits", $"设备 {name} 缺少 Modbus RTU 数据位配置"),
                        parity: ReadRequiredString(reader, "Parity", $"设备 {name} 缺少 Modbus RTU 校验位配置"),
                        stopBits: ReadRequiredString(reader, "StopBits", $"设备 {name} 缺少 Modbus RTU 停止位配置"),
                        unitId: ReadRequiredByte(reader, "UnitId", $"设备 {name} 缺少 Modbus RTU 从站 Id 配置"),
                        timeoutMs: ReadRequiredInt(reader, "TimeoutMs", $"设备 {name} 缺少超时配置"));
                }
                if (protocolType == ProtocolType.S7)
                {
                    device.ConfigureS7(
                        cpuType: ReadRequiredString(reader, "S7CpuType", $"设备 {name} 缺少 S7 CPU 类型配置"),
                        rack: ReadRequiredInt(reader, "S7Rack", $"设备 {name} 缺少 S7 Rack 配置"),
                        slot: ReadRequiredInt(reader, "S7Slot", $"设备 {name} 缺少 S7 Slot 配置"));
                }
                if (protocolType == ProtocolType.ModbusTcp)
                {
                    _ = ReadRequiredString(reader, "Ip", $"设备 {name} 缺少 Modbus TCP IP 配置");
                    _ = ReadRequiredInt(reader, "Port", $"设备 {name} 缺少 Modbus TCP 端口配置");
                    _ = ReadRequiredByte(reader, "UnitId", $"设备 {name} 缺少 Modbus TCP 从站 Id 配置");
                    _ = ReadRequiredInt(reader, "TimeoutMs", $"设备 {name} 缺少超时配置");
                }
                return device;
            }, ct, []);

           
        }

        private string ReadRequiredString(SqlDataReader reader, string columnName, string errorMessage)
        {
            var ordinal=reader.GetOrdinal(columnName);

            if(reader.IsDBNull(ordinal))
            {
                throw new InvalidOperationException(errorMessage);
            }

            var value=reader.GetString(ordinal);

            if(string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException(errorMessage);
            }

            return value;
        }

        private static int ReadRequiredInt(
            SqlDataReader reader,
            string columnName,
            string errorMessage)
        {
            var ordinal = reader.GetOrdinal(columnName);

            if (reader.IsDBNull(ordinal))
            {
                throw new InvalidOperationException(errorMessage);
            }

            return reader.GetInt32(ordinal);
        }

        private static byte ReadRequiredByte(
            SqlDataReader reader,
            string columnName,
            string errorMessage)
        {
            var ordinal = reader.GetOrdinal(columnName);

            if (reader.IsDBNull(ordinal))
            {
                throw new InvalidOperationException(errorMessage);
            }

            return reader.GetByte(ordinal);
        }
    }
}
