using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
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
    public sealed class SqlTagHistoryRepository : ITagHistoryRepository
    {
        public Task<int> InsertAsync(TagValue value, CancellationToken ct = default)
        {
            const string sql = """
                insert into dbo.TagHistory
                (
                    TagId,
                    DeviceId,
                    Value,
                    Quality,
                    Timestamp,
                    Error
                )
                VALUES
                (
                    @TagId,
                    @DeviceId,
                    @Value,
                    @Quality,
                    @Timestamp,
                    @Error
                );
                """;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TagId", SqlDbType.UniqueIdentifier) { Value = value.TagId },
                new SqlParameter("@DeviceId", SqlDbType.UniqueIdentifier) { Value = value.DeviceId },
                new SqlParameter("@Value", SqlDbType.NVarChar, 200) { Value = value.Value?.ToString() ?? (object)DBNull.Value },
                new SqlParameter("@Quality", SqlDbType.NVarChar, 50) { Value = value.Quality.ToString() },
                new SqlParameter("@Timestamp", SqlDbType.DateTimeOffset) { Value = value.Timestamp },
                new SqlParameter("@Error", SqlDbType.NVarChar, 500) { Value = value.Error ?? (object)DBNull.Value }
            };

            return DbHelper.ExecuteNonQueryAsync(sql, ct, parameters);          
        }

        public  Task<IReadOnlyList<TagValue>> QueryAsync(DateTimeOffset from, DateTimeOffset to, CancellationToken ct = default)
        {
            const string sql = """
                SELECT TagId, DeviceId, Value, Quality, Timestamp, Error
                FROM dbo.TagHistory
                WHERE Timestamp >= @From AND Timestamp <= @To
                ORDER BY Timestamp DESC;
                """;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@From",from),
                new SqlParameter("@To",to)
            };

            return DbHelper.QueryAsync(sql, reader =>
            {
                Enum.TryParse(
                    reader.GetString(reader.GetOrdinal("Quality")),
                    out TagQuality quality);

                return new TagValue
                {
                    TagId = reader.GetGuid(reader.GetOrdinal("TagId")),
                    DeviceId = reader.GetGuid(reader.GetOrdinal("DeviceId")),
                    Value = reader.IsDBNull(reader.GetOrdinal("Value"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Value")),
                    Quality = quality,
                    Timestamp = reader.GetFieldValue<DateTimeOffset>(reader.GetOrdinal("Timestamp")),
                    Error = reader.IsDBNull(reader.GetOrdinal("Error"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Error"))
                };
            }, ct, parameters);
            
        }
    }
}
