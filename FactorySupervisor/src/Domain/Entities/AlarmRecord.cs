using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Domain.Entities
{
    public sealed class AlarmRecord
    {
        public Guid Id { get; init; }=Guid.NewGuid();
        public Guid RuleId { get; init; }
        public Guid TagId {  get; init; }
        public string RuleName { get; init; } = "";
        public AlarmLevel Level { get; init; }
        public AlarmStatus State { get; private set; } = AlarmStatus.Active;
        public object? TriggerValue { get; init; }
        public DateTimeOffset TriggerTime { get; init; }=DateTimeOffset.UtcNow;
        public DateTimeOffset? RecoverTime { get; private set; }

        public void Recover(DateTimeOffset time)
        {
            if (State == AlarmStatus.Recovered) return;

            State = AlarmStatus.Recovered;
            RecoverTime = time;
        }

    }
}
