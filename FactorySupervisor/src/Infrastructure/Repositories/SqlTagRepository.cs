using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using FactorySupervisor.src.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Repositories
{
    public sealed class SqlTagRepository : ITagRepository
    {
        public Task<IReadOnlyList<Tag>> GetByDeviceAsync(Guid deviceId, CancellationToken ct = default)
        {
            const string sql = """
                SELECT
                    Id,
                    DeviceId,
                    Name,
                    Address,
                    DataType,
                    ScanMs,
                    ArchiveEnabled
                FROM dbo.TagConfig
                WHERE DeviceId = @DeviceId
                  AND Enabled = 1
                ORDER BY Name;
                """;

            var parameters = new[]
            {
                new SqlParameter("@DeviceId", SqlDbType.UniqueIdentifier)
                {
                    Value=deviceId
                }
            };

            return DbHelper.QueryAsync(sql, reader =>
            {
                Enum.TryParse(
                    reader.GetString(reader.GetOrdinal("DataType")),
                    out TagDataType dataType);

                return new Tag(
                    reader.GetGuid(reader.GetOrdinal("DeviceId")),
                    reader.GetString(reader.GetOrdinal("Name")),
                    reader.GetString(reader.GetOrdinal("Address")),
                    dataType,
                    reader.GetInt32(reader.GetOrdinal("ScanMs")),
                    reader.GetBoolean(reader.GetOrdinal("ArchiveEnabled")))
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id"))
                };
            }, ct, parameters);
        }
    }
}
