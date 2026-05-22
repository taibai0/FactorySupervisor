using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FactorySupervisor.src.Infrastructure.Data
{
    public static class DbHelper
    {
        public static async Task<int> ExecuteNonQueryAsync(
            string sql,          
            CancellationToken ct = default,
            params SqlParameter[] parameters)
        {
            await using var conn = SqlConnectionFactory.CreateAppConnection();
            await conn.OpenAsync(ct);

            await using var cmd=new SqlCommand(sql, conn);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
           
            return await cmd.ExecuteNonQueryAsync(ct);
        }
        

        public static async Task<IReadOnlyList<T>> QueryAsync<T>(
            string sql,
            Func<SqlDataReader,T> map,
            CancellationToken ct=default,
            params SqlParameter[] parameters
            )
        {
            var result=new List<T>();

            await using var conn=SqlConnectionFactory.CreateAppConnection();
            await conn.OpenAsync(ct);

            await using var cmd = new SqlCommand(sql, conn);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            await using var reader=await cmd.ExecuteReaderAsync(ct);

            while(await reader.ReadAsync(ct))
            {
                result.Add(map(reader));
            }
            return result;
        }
    }
}
