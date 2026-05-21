using FactorySupervisor.src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts.Abstractions
{
    public interface IAlarmRuleRepository
    {
        Task<IReadOnlyList<AlarmRule>> GetEnableRulesAsync(CancellationToken ct=default);
    }
}
