using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts.Models
{
    public sealed class DeviceConfigRow
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string ProtocolType { get; set; } = "";
        public string Ip { get; set; } = "";
        public int Port { get; set; }
        public bool Enabled { get; set; }
        public string ComPort { get; set; } = "";
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public string Parity { get; set; } = "";
        public string StopBits { get; set; } = "";
        public byte UnitId { get; set; }
        public int TimeoutMs { get; set; }
    }
}
