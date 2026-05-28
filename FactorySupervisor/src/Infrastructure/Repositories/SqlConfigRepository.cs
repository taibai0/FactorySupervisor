using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Contracts.Models;
using FactorySupervisor.src.Domain.Enums;
using FactorySupervisor.src.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
                Threshold = reader.GetDouble(reader.GetOrdinal("Threshold")).ToString(),
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
                ScanMs = reader.GetInt32(reader.GetOrdinal("ScanMs")).ToString(),
                ArchiveEnabled = reader.GetBoolean(reader.GetOrdinal("ArchiveEnabled")),
                Enabled = reader.GetBoolean(reader.GetOrdinal("Enabled"))
            }, ct, []);
        }

        /// <summary>
        /// 新增设备
        /// </summary>      
        /// <exception cref="NotImplementedException"></exception>
        public Task<int> InsertDeviceAsync(DeviceConfigRow row, CancellationToken ct = default)
        {
            const string sql = """
                insert into dbo.DeviceConfig
                (
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
                )
                VALUES
                (
                    @Id,
                    @Name,
                    @ProtocolType,
                    @Ip,
                    @Port,
                    @Enabled,
                    @ComPort,
                    @BaudRate,
                    @DataBits,
                    @Parity,
                    @StopBits,
                    @UnitId,
                    @TimeoutMs
                );
                """;
            var parameters = new[]
            {
               new SqlParameter("@Id", row.Id),
               new SqlParameter("@Name", row.Name),
               new SqlParameter("@ProtocolType", row.ProtocolType),
               new SqlParameter("@Ip", row.Ip),
               new SqlParameter("@Port", row.Port),
               new SqlParameter("@Enabled", row.Enabled),
               new SqlParameter("@ComPort", row.ComPort),
               new SqlParameter("@BaudRate", row.BaudRate),
               new SqlParameter("@DataBits", row.DataBits),
               new SqlParameter("@Parity", row.Parity),
               new SqlParameter("@StopBits", row.StopBits),
               new SqlParameter("@UnitId", row.UnitId),
               new SqlParameter("@TimeoutMs", row.TimeoutMs)
            };

            return DbHelper.ExecuteNonQueryAsync(sql, ct, parameters);
              
        }

        /// <summary>
        /// 新增点位
        /// </summary>
        public Task<int> InsertTagAsync(TagConfigRow row, CancellationToken ct = default)
        {
            if (!int.TryParse(row.ScanMs, out var scanMs) || scanMs <= 0)
            {
                throw new ArgumentException($"点位 {row.Name} 的 ScanMs 必须是大于 0 的整数。", nameof(row));
            }

            const string sql = """
                insert into dbo.TagConfig
                (
                    Id,
                    DeviceId,
                    Name,
                    Address,
                    DataType,
                    ScanMs,
                    ArchiveEnabled,
                    Enabled
                )
                VALUES
                (
                    @Id,
                    @DeviceId,
                    @Name,
                    @Address,
                    @DataType,
                    @ScanMs,
                    @ArchiveEnabled,
                    @Enabled
                );
                """;

            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.UniqueIdentifier){Value = row.Id},
                new SqlParameter("@DeviceId", SqlDbType.UniqueIdentifier){Value = row.DeviceId},
                new SqlParameter("@Name", SqlDbType.NVarChar, 100){Value = row.Name},
                new SqlParameter("@Address", SqlDbType.NVarChar, 50){Value = row.Address},
                new SqlParameter("@DataType", SqlDbType.NVarChar, 50){Value = row.DataType},
                new SqlParameter("@ScanMs", SqlDbType.Int){Value = scanMs},
                new SqlParameter("@ArchiveEnabled", SqlDbType.Bit){Value = row.ArchiveEnabled},
                new SqlParameter("@Enabled", SqlDbType.Bit){Value = row.Enabled}
            };

            return DbHelper.ExecuteNonQueryAsync(sql, ct, parameters);
        }

        /// <summary>
        /// 更新报警规则
        /// </summary>
        /// <param name="row"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Task<int> UpdateAlarmRuleAsync(AlarmRuleConfigRow row, CancellationToken ct = default)
        {
            if (!double.TryParse(row.Threshold, out var threshold))
            {
                throw new ArgumentException($"报警规则 {row.Name} 的阈值必须是数字。", nameof(row));
            }

            const string sql = """
                update dbo.AlarmRuleConfig
                set
                    Enabled=@Enabled,
                    Threshold=@Threshold
                where Id=@Id;
                """;

            var parameters = new[]
            {
                new SqlParameter("@Id",SqlDbType.UniqueIdentifier){Value=row.Id},
                new SqlParameter("@Enabled",SqlDbType.Bit){Value=row.Enabled},
                new SqlParameter("@Threshold",SqlDbType.Float){Value=threshold}
            };

            return DbHelper.ExecuteNonQueryAsync(sql, ct, parameters);
        }

        public Task<int> UpdateDeviceAsync(DeviceConfigRow row, CancellationToken ct = default)
        {
            const string sql = """
                update dbo.DeviceConfig
                set
                    Enabled=@Enabled
                where Id=@Id;
                """;

            var parameters = new[]
            {
                new SqlParameter("@Id",SqlDbType.UniqueIdentifier){Value=row.Id},
                new SqlParameter("@Enabled",SqlDbType.Bit){Value=row.Enabled}
            };

            return DbHelper.ExecuteNonQueryAsync(sql,ct,parameters);
        }

        public Task<int> UpdateTagAsync(TagConfigRow row, CancellationToken ct = default)
        {
            if (!int.TryParse(row.ScanMs, out var scanMs))
            {
                throw new ArgumentException($"点位 {row.Name} 的 ScanMs 必须是整数。", nameof(row));
            }

            if (string.IsNullOrWhiteSpace(row.Name))
            {
                throw new ArgumentException("点位名称不能为空。", nameof(row));
            }

            if (string.IsNullOrWhiteSpace(row.Address))
            {
                throw new ArgumentException($"点位 {row.Name} 的地址不能为空。", nameof(row));
            }

            if (!Enum.TryParse<TagDataType>(row.DataType, out _))
            {
                throw new ArgumentException($"点位 {row.Name} 的数据类型无效。", nameof(row));
            }

            const string sql = """
                update dbo.TagConfig
                set
                    Name=@Name,
                    Address=@Address,
                    DataType=@DataType,
                    ScanMs=@ScanMs,
                    ArchiveEnabled = @ArchiveEnabled,
                    Enabled=@Enabled
                where Id=@Id;
                """;

            var parameters = new[]
            {
                new SqlParameter("@Id",SqlDbType.UniqueIdentifier){Value= row.Id},
                new SqlParameter("@Name",SqlDbType.NVarChar,100){Value=row.Name},
                new SqlParameter("@Address",SqlDbType.NVarChar,50){Value=row.Address},
                new SqlParameter("@DataType",SqlDbType.NVarChar,50){Value=row.DataType},
                new SqlParameter("@ArchiveEnabled",SqlDbType.Bit){Value=row.ArchiveEnabled},
                new SqlParameter("@ScanMs",SqlDbType.Int){Value=scanMs},
                new SqlParameter("@Enabled",SqlDbType.Bit){Value=row.Enabled}
            };

            return DbHelper.ExecuteNonQueryAsync(sql, ct, parameters);
        }


    }
}
