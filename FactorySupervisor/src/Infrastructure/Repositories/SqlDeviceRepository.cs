using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using FactorySupervisor.src.Infrastructure.Data;
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

                var device = new Device(
                    reader.GetString(reader.GetOrdinal("Name")),
                    protocolType,
                    reader.GetString(reader.GetOrdinal("Ip")),
                    reader.GetInt32(reader.GetOrdinal("Port")),
                    reader.GetBoolean(reader.GetOrdinal("Enabled")))
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id"))
                };

                if (protocolType == Domain.Enums.ProtocolType.ModbusRtu)
                {
                    device.ConfigureModbusRtu(
                        comPort: reader.IsDBNull(reader.GetOrdinal("ComPort"))
                        ? ""
                        : reader.GetString(reader.GetOrdinal("ComPort")),
                        baudRate: reader.GetInt32(reader.GetOrdinal("BaudRate")),
                        dataBits: reader.GetInt32(reader.GetOrdinal("DataBits")),
                        parity: reader.GetString(reader.GetOrdinal("Parity")),
                        stopBits: reader.GetString(reader.GetOrdinal("StopBits")),
                        unitId: reader.GetByte(reader.GetOrdinal("UnitId")),
                        timeoutMs: reader.GetInt32(reader.GetOrdinal("TimeoutMs")));
                }
                return device;
            }, ct, []);

           
        }
    }
}
