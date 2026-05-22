using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Repositories
{
    public sealed class SqlAuditLogRepository : IAuditLogRepository
    {
        public Task<int> InsertAsync(AuditLog log, CancellationToken ct = default)
        {
            const string sql = """
                INSERT INTO dbo.AuditLog
                (
                    Id,
                    UserId,
                    Username,
                    Action,
                    Detail,
                    CreatedAt
                )
                VALUES
                (
                    @Id,
                    @UserId,
                    @Username,
                    @Action,
                    @Detail,
                    @CreatedAt
                );
                """;

            var parameters = new[]
            {
                new SqlParameter("@Id",SqlDbType.UniqueIdentifier){Value = log.Id},
                new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = log.UserId },
                new SqlParameter("@Username", SqlDbType.NVarChar, 100) { Value = log.Username },
                new SqlParameter("@Action", SqlDbType.NVarChar, 100) { Value = log.Action },
                new SqlParameter("@Detail", SqlDbType.NVarChar, 500) { Value = log.Detail ?? (object)DBNull.Value },
                new SqlParameter("@CreatedAt", SqlDbType.DateTimeOffset) { Value = log.CreatedAt }
            };
           return DbHelper.ExecuteNonQueryAsync(sql, ct, parameters);
        }

        public Task<IReadOnlyList<AuditLog>> QueryAsync(DateTimeOffset from, DateTimeOffset to, CancellationToken ct = default)
        {
            const string sql = """
            SELECT
                Id,
                UserId,
                Username,
                Action,
                Detail,
                CreatedAt
            FROM dbo.AuditLog
            WHERE CreatedAt >= @From AND CreatedAt <= @To
            ORDER BY CreatedAt DESC;
            """;

            var parameters = new[]
            {
                new SqlParameter("@From", SqlDbType.DateTimeOffset) { Value = from },
                new SqlParameter("@To", SqlDbType.DateTimeOffset) { Value = to }
            };

            return DbHelper.QueryAsync(sql, reader =>
            {
                return new AuditLog
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    Action = reader.GetString(reader.GetOrdinal("Action")),
                    Detail = reader.IsDBNull(reader.GetOrdinal("Detail"))
                        ? ""
                        : reader.GetString(reader.GetOrdinal("Detail")),
                    CreatedAt = reader.GetFieldValue<DateTimeOffset>(reader.GetOrdinal("CreatedAt"))
                };
            }, ct,parameters);
        }
    }
}
