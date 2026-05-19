using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.Domain.Enums
{
    /// <summary>
    /// 报警条件类型，用于描述实时值和阈值之间的比较关系。
    /// </summary>
    public enum AlarmConditionType
    {
        /// <summary>
        /// 实时值大于阈值时触发报警。
        /// 例如：Temp > 80。
        /// </summary>
        GreaterThan = 1,

        /// <summary>
        /// 实时值大于等于阈值时触发报警。
        /// 例如：Temp >= 80。
        /// </summary>
        GreaterThanOrEqual = 2,

        /// <summary>
        /// 实时值小于阈值时触发报警。
        /// 例如：Pressure < 10。
        /// </summary>
        LessThan = 3,

        /// <summary>
        /// 实时值小于等于阈值时触发报警。
        /// 例如：Pressure <= 10。
        /// </summary>
        LessThanOrEqual = 4,

        /// <summary>
        /// 实时值等于阈值时触发报警。
        /// 例如：Running == 0。
        /// </summary>
        Equal = 5,

        /// <summary>
        /// 实时值不等于阈值时触发报警。
        /// 例如：Mode != 1。
        /// </summary>
        NotEqual = 6
    }
}
