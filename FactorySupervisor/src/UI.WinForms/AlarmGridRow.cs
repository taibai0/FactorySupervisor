using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.UI.WinForms
{
    public sealed class AlarmGridRow
    {
        public Guid RuleId { get; set; }
        public string RuleName { get; set; } = "";
        public string Level { get; set; } = "";
        public string State { get; set; } = "";
        public string TriggerValue { get; set; } = "";
        public string TriggerTime { get; set; } = "";
    }
}
