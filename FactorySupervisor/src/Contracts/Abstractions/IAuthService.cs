using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Contracts.Abstractions
{
    public interface IAuthService
    {
        AppUser? CurrentUser { get; }
        bool Login(string username,string password);
        void Logout();
        bool HasRole(params UserRole[] roles);
    }
}
