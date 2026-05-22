using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Repositories
{
    public sealed class InMemoryAlarmStateStore:IAlarmStateStore
    {
        private readonly ConcurrentDictionary<Guid, AlarmRecord> _activeAlarms = new();

        public IReadOnlyList<AlarmRecord> GetCurrentAlarms()
            =>_activeAlarms.Values
            .OrderByDescending(a=>a.TriggerTime)
            .ToList();

        public bool HasActive(Guid ruleId)
         => _activeAlarms.TryGetValue(ruleId, out var alarm)
            && alarm.State == AlarmStatus.Active;

        public void Recover(Guid ruleId, DateTimeOffset revocerTime)
        {
            if(!_activeAlarms.TryGetValue(ruleId,out var alarm))
            {
                return;
            }
            
            alarm.Recover(revocerTime);
            _activeAlarms.TryRemove(ruleId, out _);
        }

        public bool TryGetActive(Guid ruleId, out AlarmRecord? alarm)
        {
            return _activeAlarms.TryGetValue(ruleId, out alarm);
        }

        public void UpsertActive(AlarmRecord alarm)
        {
            // 一个规则同一时间只保留一条活动报警，避免每秒重复刷报警。
            _activeAlarms[alarm.RuleId] = alarm;
        }
    }
}
