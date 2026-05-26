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

                IF OBJECT_ID(N'dbo.AuditLog',N'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.AuditLog
                    (
                        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                        UserId UNIQUEIDENTIFIER NOT NULL,
                        Username NVARCHAR(100) NOT NULL,
                        Action NVARCHAR(100) NOT NULL,
                        Detail NVARCHAR(500) NULL,
                        CreatedAt DATETIMEOFFSET NOT NULL
                    )
                    CREATE INDEX IX_AuditLog_CreatedAt
                    ON dbo.AuditLog(CreatedAt DESC);
                END;

                IF OBJECT_ID(N'dbo.DeviceConfig',N'U') IS NULL
                BEGIN
                    CREATE TABLE  dbo.DeviceConfig
                    (
                        Id UNIQUEIDENTIFIER   NOT NULL  PRIMARY KEY ,
                        Name NVARCHAR(100) NOT NULL,
                        ProtocolType NVARCHAR(50) NOT NULL,
                        Ip NVARCHAR(100) NOT NULL,
                        Port INT NOT NULL,
                        Enabled BIT NOT NULL,
                        ComPort NVARCHAR(50) NULL,
                        BaudRate INT NOT NULL,
                        DataBits INT NOT NULL,
                        Parity NVARCHAR(20) NOT NULL,
                        StopBits NVARCHAR(20) NOT NULL,
                        UnitId TINYINT NOT NULL,
                        TimeoutMs INT NOT NULL
                    );
                END;

                IF OBJECT_ID(N'dbo.TagConfig', N'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.TagConfig
                    (
                        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                        DeviceId UNIQUEIDENTIFIER NOT NULL,
                        Name NVARCHAR(100) NOT NULL,
                        Address NVARCHAR(100) NOT NULL,
                        DataType NVARCHAR(50) NOT NULL,
                        ScanMs INT NOT NULL,
                        ArchiveEnabled BIT NOT NULL,
                        Enabled BIT NOT NULL
                    );

                    CREATE INDEX IX_TagConfig_DeviceId
                    ON dbo.TagConfig(DeviceId);
                END;

                IF OBJECT_ID(N'dbo.AlarmRuleConfig', N'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.AlarmRuleConfig
                    (
                        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                        TagId UNIQUEIDENTIFIER NOT NULL,
                        Name NVARCHAR(100) NOT NULL,
                        Level NVARCHAR(50) NOT NULL,
                        ConditionType NVARCHAR(50) NOT NULL,
                        Threshold FLOAT NOT NULL,
                        Enabled BIT NOT NULL
                    );

                    CREATE INDEX IX_AlarmRuleConfig_TagId
                    ON dbo.AlarmRuleConfig(TagId);
                END;

                if not exists(select 1 from dbo.DeviceConfig)
                begin
                    insert into dbo.DeviceConfig
                    (Id, Name, ProtocolType, Ip, Port, Enabled,
                     ComPort, BaudRate, DataBits, Parity, StopBits, UnitId, TimeoutMs
                    )
                    values
                    (
                        '11111111-1111-1111-1111-111111111111',
                        N'RTU-SLAVE-01',
                        N'ModbusRtu',
                        N'127.0.0.1',
                        1,
                        1,
                        N'COM21',
                        9600,
                        8,
                        N'None',
                        N'One',
                        1,
                        1000
                    );
                end;

                IF NOT EXISTS (SELECT 1 FROM dbo.TagConfig)
                BEGIN
                    INSERT INTO dbo.TagConfig
                    (
                        Id, DeviceId, Name, Address, DataType, ScanMs, ArchiveEnabled, Enabled
                    )
                    VALUES
                    ('22222222-2222-2222-2222-222222222221', '11111111-1111-1111-1111-111111111111', N'Running', N'00001', N'Bool', 1000, 1, 1),
                    ('22222222-2222-2222-2222-222222222222', '11111111-1111-1111-1111-111111111111', N'Temp', N'40001', N'UInt16', 1000, 1, 1),
                    ('22222222-2222-2222-2222-222222222223', '11111111-1111-1111-1111-111111111111', N'Pressure', N'40002', N'UInt16', 1000, 1, 1),
                    ('22222222-2222-2222-2222-222222222224', '11111111-1111-1111-1111-111111111111', N'Counter', N'40003', N'UInt16', 1000, 1, 1);
                END;

                IF NOT EXISTS (SELECT 1 FROM dbo.AlarmRuleConfig)
                BEGIN
                    INSERT INTO dbo.AlarmRuleConfig
                    (
                        Id, TagId, Name, Level, ConditionType, Threshold, Enabled
                    )
                    VALUES
                    ('33333333-3333-3333-3333-333333333331', '22222222-2222-2222-2222-222222222222', N'Temp High', N'High', N'GreaterThan', 80, 1),
                    ('33333333-3333-3333-3333-333333333332', '22222222-2222-2222-2222-222222222223', N'Pressure Low', N'Medium', N'LessThan', 10, 1),
                    ('33333333-3333-3333-3333-333333333333', '22222222-2222-2222-2222-222222222221', N'Machine Stopped', N'Critical', N'Equal', 0, 1);
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
