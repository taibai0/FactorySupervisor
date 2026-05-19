using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Caching
{
    public sealed class InMemoryTagValueCache:ITagValueCache
    {
        private readonly ConcurrentDictionary<Guid,TagValue> _map=new();

        public void Set(TagValue value) => _map[value.TagId] = value;

        public bool TryGet(Guid tagId,out TagValue? value)
        {
            var ok = _map.TryGetValue(tagId, out var v);
            value = v;
            return ok;
        }

        public IReadOnlyList<TagValue> GetByDevice(Guid deviceId)
            => _map.Values.Where(v => v.DeviceId == deviceId).OrderBy(v => v.TagId).ToList();
    }
}
