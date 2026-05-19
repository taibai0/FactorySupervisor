using FactorySupervisor.src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts.Abstractions
{
    public interface ITagValueCache
    {
        void Set(TagValue value);
        bool TryGet(Guid tagId, out TagValue? value);
        IReadOnlyList<TagValue> GetByDevice(Guid deviceId);
    }
}
