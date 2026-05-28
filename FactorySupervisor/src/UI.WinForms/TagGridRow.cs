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

        //当前点位的所属设备id
        public Guid DeviceId { get; set; }

        //当前点位所属的设备名称，用来显示在主界面表格里
        public string DeviceName { get; set; } = "";
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string Value { get; set; } = "";
        public string Quality { get; set; } = "None";
        public string Time { get; set; } = "";
        public string Error { get; set; } = "";
    }
}
