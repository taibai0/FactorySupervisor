using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///<summary>
///报警规则
///</summary>
namespace FactorySupervisor.src.Domain.Entities
{
    public sealed class AlarmRule
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid TagId { get; init; }
        public string Name { get; init; } = "";
        public AlarmLevel Level{ get; init; }
        public AlarmConditionType ConditionType { get; init; }
        public double Threshold { get; init; }
        public bool Enabled { get; init; } = true;
    }
}
