using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Domain.Entities
{
    public sealed class AuditLog
    {
        public Guid Id { get; init; }=Guid.NewGuid();
        public Guid UserId { get; init; }
        public string Username { get; init; } = "";
        public string Action { get; init; } = "";
        public string Detail { get; init; } = "";
        public DateTimeOffset CreatedAt { get; init; }=DateTimeOffset.Now;
    }
}
