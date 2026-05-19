using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * modbus地址解析器
*/
namespace FactorySupervisor.src.Infrastructure.Protocol
{
    internal enum ModbusArea
    {
        Coil,
        DiscreteInput,
        InputRegister,
        HoldingRegister
    }

    internal readonly record struct ModbusAddress(ModbusArea Area,ushort Offset);


    internal static class ModbusAddressParser
    {
        public static ModbusAddress Parse(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Address is required");
            }

            if (!int.TryParse(address.Trim(), out var raw))
                throw new ArgumentException($"invalid Modbus address:{address}");


            return raw switch
            {
                >= 1 and <= 9999 => new(ModbusArea.Coil, (ushort)(raw - 1)),
                >= 10001 and <= 19999 => new(ModbusArea.DiscreteInput, (ushort)(raw - 10001)),
                >= 30001 and <= 39999 => new(ModbusArea.InputRegister, (ushort)(raw - 30001)),
                >= 40001 and <= 49999 => new(ModbusArea.HoldingRegister, (ushort)(raw - 40001)),
                _ => throw new ArgumentOutOfRangeException(nameof(address), $"Address out of range: {address}")
            };
        }
    }
}
