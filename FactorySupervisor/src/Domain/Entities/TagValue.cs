using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Domain.Entities
{
    public sealed class TagValue
    {
        public Guid TagId { get; init; }
        public Guid DeviceId { get; init; }
        public object? Value { get; init; }
        public DateTimeOffset Timestamp { get; init; }
        public TagQuality Quality { get; init; }
        public string? Error { get; init; }

        public static TagValue Good(Guid tagId, Guid deviceId, object? value, DateTimeOffset ts)
            => new()
            {
                TagId = tagId,
                DeviceId = deviceId,
                Value = value,
                Timestamp = ts,
                Quality = TagQuality.Good
            };

        public static TagValue Bad(Guid tagId, Guid deviceId, string error, DateTimeOffset ts)
            => new()
            {
                TagId = tagId,
                DeviceId = deviceId,
                Timestamp = ts,
                Quality = TagQuality.Bad,
                Error = error
            };
    }
}
