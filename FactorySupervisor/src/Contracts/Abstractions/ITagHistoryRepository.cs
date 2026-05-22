using FactorySupervisor.src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts.Abstractions
{
    public interface ITagHistoryRepository
    {
        Task<int> InsertAsync(TagValue value, CancellationToken ct = default);

        Task<IReadOnlyList<TagValue>> QueryAsync(
            DateTimeOffset from,
            DateTimeOffset to,
            CancellationToken ct = default);    
    }
}
