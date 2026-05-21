using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using FactorySupervisor.src.Infrastructure.Sim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Repositories
{
    public sealed class InMemoryAlarmRuleRepository : IAlarmRuleRepository
    {
        private static readonly IReadOnlyList<AlarmRule> Rules =
        [
            //温度超过80触发高温警报
            new AlarmRule
            {
                TagId = RtuSeed.Tags.First(t => t.Name == "Temp").Id,
                Name = "Temp High",
                Level = AlarmLevel.High,
                ConditionType = AlarmConditionType.GreaterThan,
                Threshold = 80,
                Enabled = true
            },

            //压力低于10触发低压警报
            new AlarmRule
            {
                TagId = RtuSeed.Tags.First(t=>t.Name=="Pressure").Id,
                Name = "Pressure Low",
                Level = AlarmLevel.Medium,
                ConditionType = AlarmConditionType.LessThan,
                Threshold = 10,
                Enabled = true
            },

             // 运行状态为 0 时触发停机报警
             new AlarmRule
            {
                TagId = RtuSeed.Tags.First(t => t.Name == "Running").Id,
                Name = "Machine Stopped",
                Level = AlarmLevel.Critical,
                ConditionType = AlarmConditionType.Equal,
                Threshold = 0,
                Enabled = true
            }
        ];

        public Task<IReadOnlyList<AlarmRule>> GetEnableRulesAsync(CancellationToken ct = default)
            => Task.FromResult((IReadOnlyList<AlarmRule>)Rules.Where(r => r.Enabled).ToList());
    }
}
