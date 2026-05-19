using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Domain.Enums
{
    public enum ProtocolType
    {
        ModbusTcp=1,
        ModbusRtu=2,
        S7=3,
        Simulator=99
    }
}
