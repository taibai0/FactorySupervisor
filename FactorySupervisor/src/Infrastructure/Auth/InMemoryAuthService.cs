using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Auth
{
    public sealed class InMemoryAuthService:IAuthService
    {
        private readonly List<(AppUser user, string password)> _users =
        [
            (
                 new AppUser
                {
                    Username = "admin",
                    DisplayName = "管理员",
                    Role = UserRole.Admin
                },
                "123456"
            ),
            (
                new AppUser
                {
                    Username = "engineer",
                    DisplayName = "工程师",
                    Role = UserRole.Engineer
                },
                "123456"
            ),
            (
                new AppUser
                {
                    Username = "operator",
                    DisplayName = "操作员",
                    Role = UserRole.Operator
                },
                "123456"
            )
        ];

        public AppUser? CurrentUser { get; private set; }

        public bool HasRole(params UserRole[] roles)
        {
            return CurrentUser is not null && roles.Contains(CurrentUser.Role);
        }

        public bool Login(string username, string password)
        {
            var item = _users.FirstOrDefault(x =>
            string.Equals(x.user.Username, username, StringComparison.OrdinalIgnoreCase)
            && x.password == password);

            if(item.user is null)
            {
                return false;
            }

            CurrentUser = item.user;
            return true;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
