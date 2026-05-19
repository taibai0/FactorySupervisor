using FactorySupervisor.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
/**
 * 根据设备信息创建协议客户端的工厂接口
 */
namespace FactorySupervisor.src.Contracts.Abstractions
{
    public interface IProtocolClientFactory
    {
        IProtocolClient Create(ProtocolType protocolType);
    }
}
