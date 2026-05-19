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
    public sealed class InMemoryDeviceRepository : IDeviceRepository
    {
        public Task<IReadOnlyList<Device>> GetEnableDevicesAsync(CancellationToken ct = default)
            =>Task.FromResult((IReadOnlyList<Device>)RtuSeed.Devices.Where(d=>d.Enabled).ToList());
        
    }
}
