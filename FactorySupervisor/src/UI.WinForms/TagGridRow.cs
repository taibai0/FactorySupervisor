using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.UI.WinForms
{
   public sealed class TagGridRow
    {
        public Guid TagId { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string Value { get; set; } = "";
        public string Quality { get; set; } = "None";
        public string Time { get; set; } = "";
        public string Error { get; set; } = "";
    }
}
