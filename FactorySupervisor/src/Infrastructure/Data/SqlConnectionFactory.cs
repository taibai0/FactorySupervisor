using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Data
{
    public static class SqlConnectionFactory
    {
        public const string MasterConnectionString=
           "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

        public const string AppConnectionString=
            "Server = localhost\\SQLEXPRESS;Database=FactorySupervisor;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection CreateMasterConnection()
            => new(MasterConnectionString);

        public static SqlConnection CreateAppConnection()
            => new(AppConnectionString);


    }

    
}
