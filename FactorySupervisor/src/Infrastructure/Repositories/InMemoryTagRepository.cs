using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Infrastructure.Sim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Repositories
{
    public sealed class InMemoryTagRepository : ITagRepository
    {
        public Task<IReadOnlyList<Tag>> GetByDeviceAsync(Guid deviceId, CancellationToken ct = default)
            => Task.FromResult((IReadOnlyList<Tag>)RtuSeed.Tags
                .Where(t => t.DeviceId == deviceId)
                .ToList());
    }
}
