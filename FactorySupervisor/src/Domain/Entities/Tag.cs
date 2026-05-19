
using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Domain.Entities
{
    public sealed class Tag
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid DeviceId { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public TagDataType DataType { get; private set; }
        public int ScanMs { get; private set; }
        public bool ArchiveEnabled { get; private set; }

        public Tag(Guid deviceId, string name, string address, TagDataType dataType, int scanMs, bool archiveEnabled = true)
        {
            if (deviceId == Guid.Empty) throw new ArgumentException("DeviceId is required.", nameof(deviceId));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Tag name is required.", nameof(name));
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Tag address is required.", nameof(address));
            if (scanMs <= 0) throw new ArgumentOutOfRangeException(nameof(scanMs), "ScanMs must be greater than 0.");

            DeviceId = deviceId;
            Name = name.Trim();
            Address = address.Trim();
            DataType = dataType;
            ScanMs = scanMs;
            ArchiveEnabled = archiveEnabled;
        }

        public void SetScanMs(int scanMs)
        {
            if (scanMs <= 0) throw new ArgumentOutOfRangeException(nameof(scanMs));
            ScanMs = scanMs;
        }
    }
}
