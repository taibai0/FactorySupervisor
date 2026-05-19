using FactorySupervisor.src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts
{  
    public sealed record BatchReadResult(
        bool Success,
        IReadOnlyList<TagValue> Values,
        string? Error,
        DateTimeOffset Time);
    
}
