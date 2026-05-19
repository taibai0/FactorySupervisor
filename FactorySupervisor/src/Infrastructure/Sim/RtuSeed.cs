using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Infrastructure.Sim
{
    public static class RtuSeed
    {
        public static readonly Guid Device1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");

        public static IReadOnlyList<Device> Devices { get; } =
        [
            CreateDevice()
        ];

        public static IReadOnlyList<Tag> Tags { get; } =
        [
            new Tag(Device1Id, "Running", "00001", TagDataType.Bool, 1000),
            new Tag(Device1Id, "Temp", "40001", TagDataType.UInt16, 1000),
            new Tag(Device1Id, "Pressure", "40002", TagDataType.UInt16, 1000),
            new Tag(Device1Id, "Counter", "40003", TagDataType.UInt16, 1000)
        ];

        private static Device CreateDevice()
        {
            var device = new Device("RTU-SLAVE-01", ProtocolType.ModbusRtu, "127.0.0.1", 1, true)
            {
                Id = Device1Id
            };

            device.ConfigureModbusRtu(
                comPort: "COM21",
                baudRate: 9600,
                dataBits: 8,
                parity: "None",
                stopBits: "One",
                unitId: 1,
                timeoutMs: 1000);

            return device;
        }
    }
}
