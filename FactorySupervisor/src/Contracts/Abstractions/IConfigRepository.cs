using FactorySupervisor.src.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts.Abstractions
{
    public interface IConfigRepository
    {
        Task<IReadOnlyList<DeviceConfigRow>> GetDevicesAsync(CancellationToken ct = default);

        Task<IReadOnlyList<TagConfigRow>> GetTagsAsync(CancellationToken ct = default);

        Task<IReadOnlyList<AlarmRuleConfigRow>> GetAlarmRulesAsync(CancellationToken ct = default);
    }
}
