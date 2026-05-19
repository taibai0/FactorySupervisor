using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ProtocolType = FactorySupervisor.src.Domain.Enums.ProtocolType;

namespace FactorySupervisor.src.Infrastructure.Protocol
{
    public sealed class ProtocolClientFactory : IProtocolClientFactory
    {
        public IProtocolClient Create(ProtocolType protocolType)
        {
            return protocolType switch
            {
                ProtocolType.ModbusRtu => new ModBusRtuProtocolClient(),
                _ => throw new NotSupportedException($"Protocol type {protocolType} is not supported.")
            };
        }

       
    }
}
