using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts.Models
{
    public sealed class TagConfigRow
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string DataType { get; set; } = "";
        public string ScanMs { get; set; } = "";
        public bool ArchiveEnabled { get; set; }
        public bool Enabled { get; set; }
    }
}
