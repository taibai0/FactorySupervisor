using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Domain.Enums
{
    public enum AlarmStatus
    {   ///<summary>
        ///报警已触发，尚未恢复
        ///</summary>
        Active=1,

        /// <summary>
        /// 报警条件已消失，设备状态已恢复正常
        /// </summary>
        Recovered=2,

        /// <summary>
        /// 报警已被操作员确认
        /// </summary>
        Acknowledged=3
    }
}
