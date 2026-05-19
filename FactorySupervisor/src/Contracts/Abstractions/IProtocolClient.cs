using FactorySupervisor.src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts.Abstractions
{
    public interface IProtocolClient:IAsyncDisposable
    {
        Task<ConnectResult> ConnectAsync(Device device, CancellationToken ct = default);

        Task DisconnectAsync(CancellationToken ct = default);

        Task<BatchReadResult> ReadAsync(
            IReadOnlyList<Tag> tags,
            CancellationToken ct = default);

        Task<WriteResult> WriteAsync(
            Tag tag,
            object value,
            CancellationToken ct = default);       
    }
}
