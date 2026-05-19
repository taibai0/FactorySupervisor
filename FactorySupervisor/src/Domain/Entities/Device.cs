using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Domain.Entities
{
    public sealed class Device
    {
        public Guid Id { get; init; }= Guid.NewGuid();

        public string Name { get; private set; }

        public ProtocolType ProtocolType { get; private set; }

        public string Ip {  get; private set; }

        public int Port { get; private set; }

        public bool Enabled { get; private set; }

        //modbusRTU
        public string? ComPort { get; private set; }
        public int BaudRate { get; private set; }
        public int DataBits { get; private set; }
        public string Parity { get; private set; } = "None";
        public string StopBits { get; private set; } = "One";
        public byte UnitId { get; private set; } = 1;

        public int TimeoutMs { get; private set; } = 1000;





        public Device(string name, ProtocolType protocolType, string ip, int port, bool enabled)
        {
            if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Device name cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(ip)) throw new ArgumentException("Device ip is required.", nameof(ip));
            if (port <= 0) throw new ArgumentOutOfRangeException(nameof(port), "Port must be greater than 0.");

            Name = name.Trim();
            ProtocolType = protocolType;
            Ip = ip.Trim();
            Port = port;
            Enabled = enabled;
        }

        public void SetEnabled(bool enabled)=>Enabled = enabled;

        public void ConfigureModbusRtu(
            string comPort,
            int baudRate,
            int dataBits,
            string parity,
            string stopBits,
            byte unitId,
            int timeoutMs)
        {
            if(string.IsNullOrWhiteSpace(comPort)) throw new ArgumentException("ComPort is required.", nameof(comPort));
            if (baudRate <= 0) throw new ArgumentOutOfRangeException(nameof(BaudRate), "BadRate must be greater than 0.");
            if (dataBits is <5 or >8) throw new ArgumentOutOfRangeException(nameof(dataBits), "DataBits must be greater than 0.");
            if (timeoutMs <= 0) throw new ArgumentOutOfRangeException(nameof(timeoutMs), "TimeoutMs must be greater than 0.");
            if(unitId <= 0)  throw new ArgumentOutOfRangeException(nameof(unitId), "UnitId must be greater than 0.");
            ComPort = comPort.Trim();
            this.BaudRate = baudRate;
            this.DataBits = dataBits;
            Parity = parity.Trim();
            StopBits = stopBits.Trim();
            UnitId = unitId;
            TimeoutMs = timeoutMs;
        }
    }
}
