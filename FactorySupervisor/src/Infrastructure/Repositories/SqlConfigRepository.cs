using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Contracts.Models;
using FactorySupervisor.src.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Repositories
{
    public sealed class SqlConfigRepository : IConfigRepository
    {
        public Task<IReadOnlyList<AlarmRuleConfigRow>> GetAlarmRulesAsync(CancellationToken ct = default)
        {
            const string sql = """
        SELECT
            Id,
            TagId,
            Name,
            Level,
            ConditionType,
            Threshold,
            Enabled
        FROM dbo.AlarmRuleConfig
        ORDER BY Name;
        """;

            return DbHelper.QueryAsync(sql, reader => new AlarmRuleConfigRow
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                TagId = reader.GetGuid(reader.GetOrdinal("TagId")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Level = reader.GetString(reader.GetOrdinal("Level")),
                ConditionType = reader.GetString(reader.GetOrdinal("ConditionType")),
                Threshold = reader.GetDouble(reader.GetOrdinal("Threshold")),
                Enabled = reader.GetBoolean(reader.GetOrdinal("Enabled"))
            }, ct, []);
        }

        public Task<IReadOnlyList<DeviceConfigRow>> GetDevicesAsync(CancellationToken ct = default)
        {
            const string sql = """
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
        ORDER BY Name;
        """;

            return DbHelper.QueryAsync(sql, reader => new DeviceConfigRow
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                ProtocolType = reader.GetString(reader.GetOrdinal("ProtocolType")),
                Ip = reader.GetString(reader.GetOrdinal("Ip")),
                Port = reader.GetInt32(reader.GetOrdinal("Port")),
                Enabled = reader.GetBoolean(reader.GetOrdinal("Enabled")),
                ComPort = reader.IsDBNull(reader.GetOrdinal("ComPort"))
                    ? ""
                    : reader.GetString(reader.GetOrdinal("ComPort")),
                BaudRate = reader.GetInt32(reader.GetOrdinal("BaudRate")),
                DataBits = reader.GetInt32(reader.GetOrdinal("DataBits")),
                Parity = reader.GetString(reader.GetOrdinal("Parity")),
                StopBits = reader.GetString(reader.GetOrdinal("StopBits")),
                UnitId = reader.GetByte(reader.GetOrdinal("UnitId")),
                TimeoutMs = reader.GetInt32(reader.GetOrdinal("TimeoutMs"))
            }, ct, []);
        }

        public Task<IReadOnlyList<TagConfigRow>> GetTagsAsync(CancellationToken ct = default)
        {
            const string sql = """
            SELECT
                Id,
                DeviceId,
                Name,
                Address,
                DataType,
                ScanMs,
                ArchiveEnabled,
                Enabled
            FROM dbo.TagConfig
            ORDER BY Name;
            """;

            return DbHelper.QueryAsync(sql, reader => new TagConfigRow
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                DeviceId = reader.GetGuid(reader.GetOrdinal("DeviceId")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Address = reader.GetString(reader.GetOrdinal("Address")),
                DataType = reader.GetString(reader.GetOrdinal("DataType")),
                ScanMs = reader.GetInt32(reader.GetOrdinal("ScanMs")),
                ArchiveEnabled = reader.GetBoolean(reader.GetOrdinal("ArchiveEnabled")),
                Enabled = reader.GetBoolean(reader.GetOrdinal("Enabled"))
            }, ct, []);
        }
    }
}
