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
    public sealed class SqlAlarmRuleRepository : IAlarmRuleRepository
    {
        public Task<IReadOnlyList<AlarmRule>> GetEnableRulesAsync(CancellationToken ct = default)
        {
            const string sql = """
                select
                   Id,
                   TagId,
                   Name,
                   Level,
                   ConditionType,
                   Threshold,
                   Enabled
                from dbo.AlarmRuleConfig
                where Enabled=1
                order by Name;
                """;

            return DbHelper.QueryAsync(sql, reader =>
            {
                Enum.TryParse(
                    reader.GetString(reader.GetOrdinal("Level")),
                    out AlarmLevel level
                );

                Enum.TryParse(
                    reader.GetString(reader.GetOrdinal("ConditionType")),
                    out AlarmConditionType alarmConditionType
                    );

                return new AlarmRule
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    TagId = reader.GetGuid(reader.GetOrdinal("TagId")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Level = level,
                    ConditionType = alarmConditionType,
                    Threshold = reader.GetDouble(reader.GetOrdinal("Threshold")),
                    Enabled = reader.GetBoolean(reader.GetOrdinal("Enabled"))
                };
            }, ct, []);
        }
    }
}
