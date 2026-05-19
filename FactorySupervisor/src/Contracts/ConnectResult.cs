using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts
{
    public sealed record ConnectResult(bool IsSuccess, string? Error, DateTimeOffset Time);
   
}
