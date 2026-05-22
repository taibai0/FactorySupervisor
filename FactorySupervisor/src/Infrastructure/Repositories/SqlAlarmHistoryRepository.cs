using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using FactorySupervisor.src.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Repositories
{
    public sealed class SqlAlarmHistoryRepository : IAlarmHistoryRepository
    {
        public Task<int> InsertAsync(AlarmRecord alarm, CancellationToken ct = default)
        {
            const string sql = """
                insert into dbo.AlarmHistory
                (
                    AlarmId,
                    RuleId,
                    TagId,
                    RuleName,
                    Level,
                    State,
                    TriggerValue,
                    TriggerTime,
                    RecoverTime
                )
                values
                (
                    @AlarmId,
                    @RuleId,
                    @TagId,
                    @RuleName,
                    @Level,
                    @State,
                    @TriggerValue,
                    @TriggerTime,
                    @RecoverTime
                );
                """;

            var parameters = new[]
            {
               new SqlParameter("@AlarmId", SqlDbType.UniqueIdentifier) { Value = alarm.Id },
               new SqlParameter("@RuleId", SqlDbType.UniqueIdentifier) { Value = alarm.RuleId },
               new SqlParameter("@TagId", SqlDbType.UniqueIdentifier) { Value = alarm.TagId },
               new SqlParameter("@RuleName", SqlDbType.NVarChar, 200) { Value = alarm.RuleName },
               new SqlParameter("@Level", SqlDbType.NVarChar, 50) { Value = alarm.Level.ToString() },
               new SqlParameter("@State", SqlDbType.NVarChar, 50) { Value = alarm.State.ToString() },
               new SqlParameter("@TriggerValue", SqlDbType.NVarChar, 200) { Value = alarm.TriggerValue?.ToString() ?? (object)DBNull.Value },
               new SqlParameter("@TriggerTime", SqlDbType.DateTimeOffset) { Value = alarm.TriggerTime },
               new SqlParameter("@RecoverTime", SqlDbType.DateTimeOffset) { Value = alarm.RecoverTime ?? (object)DBNull.Value }
            };

            return DbHelper.ExecuteNonQueryAsync(sql,ct,parameters);
        }

        public Task<IReadOnlyList<AlarmRecord>> QueryAsync(DateTimeOffset from, DateTimeOffset to, CancellationToken ct = default)
        {
            const string sql = """
                select
                    AlarmId,
                    RuleId,
                    TagId,
                    RuleName,
                    Level,
                    State,
                    TriggerValue,
                    TriggerTime,
                    RecoverTime
                from dbo.AlarmHistory
                where TriggerTime>=@From and TriggerTime<=@To
                order by TriggerTime desc;
                """;

            var parameters = new SqlParameter[]
            {
                 new SqlParameter("@From", SqlDbType.DateTimeOffset) { Value = from },
                 new SqlParameter("@To", SqlDbType.DateTimeOffset) { Value = to }
            };

            return DbHelper.QueryAsync(sql, reader =>
            {
                Enum.TryParse(
                    reader.GetString(reader.GetOrdinal("Level")),
                    out AlarmLevel level);

                Enum.TryParse(
                    reader.GetString(reader.GetOrdinal("State")),
                    out AlarmStatus state);

                var alarm = new AlarmRecord
                {
                    Id = reader.GetGuid(reader.GetOrdinal("AlarmId")),
                    RuleId = reader.GetGuid(reader.GetOrdinal("RuleId")),
                    TagId = reader.GetGuid(reader.GetOrdinal("TagId")),
                    RuleName = reader.GetString(reader.GetOrdinal("RuleName")),
                    Level = level,
                    TriggerValue = reader.IsDBNull(reader.GetOrdinal("TriggerValue"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("TriggerValue")),
                    TriggerTime = reader.GetFieldValue<DateTimeOffset>(reader.GetOrdinal("TriggerTime"))
                };

                if (state == AlarmStatus.Recovered)
                {
                    var recoverTime = reader.IsDBNull(reader.GetOrdinal("RecoverTime"))
                    ? DateTimeOffset.Now
                    : reader.GetFieldValue<DateTimeOffset>(reader.GetOrdinal("RecoverTime"));

                    alarm.Recover(recoverTime);
                }
                return alarm;
            }, ct, parameters);
        }

        public  Task<int> UpdateRecoveredAsync(Guid alarmId, DateTimeOffset recoverTime, CancellationToken ct = default)
        {
            const string sql = """
                update dbo.AlarmHistory
                set State=@State,
                    RecoverTime=@RecoverTime
                where AlarmId=@AlarmId;
                """;

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@AlarmId", SqlDbType.UniqueIdentifier) { Value = alarmId },
                new SqlParameter("@State", SqlDbType.NVarChar, 50) { Value = AlarmStatus.Recovered.ToString() },
                new SqlParameter("@RecoverTime", SqlDbType.DateTimeOffset) { Value = recoverTime }
            };

            return DbHelper.ExecuteNonQueryAsync(sql, ct, parameters);
        }
    }
}
