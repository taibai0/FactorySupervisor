using FactorySupervisor.src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts.Abstractions
{
    public interface IAlarmHistoryRepository
    {
        Task<int> InsertAsync(AlarmRecord alarm,CancellationToken ct=default);

        Task<int> UpdateRecoveredAsync(
            Guid  alarmId,
            DateTimeOffset recoverTime,
            CancellationToken ct=default);

        Task<IReadOnlyList<AlarmRecord>> QueryAsync(
              DateTimeOffset from,
              DateTimeOffset to,
              CancellationToken ct = default);
    }
}
