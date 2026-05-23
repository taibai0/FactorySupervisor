using FactorySupervisor.src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts.Abstractions
{
    public interface IAuditLogRepository
    {
        Task<int> InsertAsync(AuditLog log, CancellationToken ct = default);
        Task<IReadOnlyList<AuditLog>> QueryAsync(
            DateTimeOffset from,
            DateTimeOffset to,
            CancellationToken ct = default);
    }
}
