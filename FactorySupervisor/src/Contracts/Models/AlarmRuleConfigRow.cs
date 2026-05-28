using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts.Models
{
    public sealed class AlarmRuleConfigRow
    {
        public Guid Id { get; set; }
        public Guid TagId { get; set; }
        public string Name { get; set; } = "";
        public string Level { get; set; } = "";
        public string ConditionType { get; set; } = "";
        public string Threshold { get; set; } = "";
        public bool Enabled { get; set; }
    }
}
