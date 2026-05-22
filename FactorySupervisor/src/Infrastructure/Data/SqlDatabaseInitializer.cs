using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Data
{
    public static class SqlDatabaseInitializer
    {
        public static async Task InitializeAsync(CancellationToken ct = default)
        {
            await EnsureDatabaseAsync(ct);
            await EnsureTablesAsync(ct);
        }

        //数据库中没有表就创建表
        private static async Task EnsureTablesAsync(CancellationToken ct)
        {
            await using var conn = SqlConnectionFactory.CreateAppConnection();
            await conn.OpenAsync();

            const string sql = """
                IF OBJECT_ID(N'dbo.TagHistory',N'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.TagHistory
                    (
                        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
                        TagId UNIQUEIDENTIFIER NOT NULL,
                        DeviceId UNIQUEIDENTIFIER NOT NULL,
                        Value NVARCHAR(200) NULL,
                        Quality NVARCHAR(50) NOT NULL,
                        Timestamp DATETIMEOFFSET NOT NULL,
                        Error NVARCHAR(500) NULL
                    );
                    CREATE INDEX IX_TagHistory_TagId_Timestamp
                    ON dbo.TagHistory(TagId,Timestamp DESC);
                END;

                IF OBJECT_ID(N'dbo.AlarmHistory', N'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.AlarmHistory
                    (
                        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
                        AlarmId UNIQUEIDENTIFIER NOT NULL,
                        RuleId UNIQUEIDENTIFIER NOT NULL,
                        TagId UNIQUEIDENTIFIER NOT NULL,
                        RuleName NVARCHAR(200) NOT NULL,
                        Level NVARCHAR(50) NOT NULL,
                        State NVARCHAR(50) NOT NULL,
                        TriggerValue NVARCHAR(200) NULL,
                        TriggerTime DATETIMEOFFSET NOT NULL,
                        RecoverTime DATETIMEOFFSET NULL
                    );

                    CREATE UNIQUE INDEX IX_AlarmHistory_AlarmId
                    ON dbo.AlarmHistory(AlarmId);

                    CREATE INDEX IX_AlarmHistory_TriggerTime
                    ON dbo.AlarmHistory(TriggerTime DESC);
                END;
                """;

            await using var cmd = new SqlCommand(sql, conn);
            await cmd.ExecuteNonQueryAsync(ct);
        }

        //没有数据库就创建数据库
        private static async Task EnsureDatabaseAsync(CancellationToken ct)
        {
           await using var conn=SqlConnectionFactory.CreateMasterConnection();
           await conn.OpenAsync();

            const string sql = """
            IF DB_ID(N'FactorySupervisor')IS NULL
            BEGIN
                CREATE DATABASE FactorySupervisor;
            END
            """;

            await using var cmd=new SqlCommand(sql, conn);
            await cmd.ExecuteNonQueryAsync(ct);
        }
    }
}
