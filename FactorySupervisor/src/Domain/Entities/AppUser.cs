using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Domain.Entities
{
    public sealed class AppUser
    {
        public Guid Id { get; init; }=Guid.NewGuid();
        public string Username { get; init; } = "";
        public string DisplayName { get; init; } = "";
        public UserRole Role { get; init; }
    }
}
