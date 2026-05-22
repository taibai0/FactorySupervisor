using FactorySupervisor.src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts.Abstractions
{
    public interface IAlarmStateStore
    {
        IReadOnlyList<AlarmRecord> GetCurrentAlarms();

        void UpsertActive(AlarmRecord alarm);
        void Recover(Guid ruleId, DateTimeOffset revocerTime);
        bool HasActive(Guid ruleId);

        bool TryGetActive(Guid ruleId, out AlarmRecord? alarm);
    }
}
