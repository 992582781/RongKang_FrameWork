using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RongKang_Entity
{
    /// <summary>
    /// 租赁信息状态
    /// </summary>
    public enum Rental_Type_enum
    {
        /// <summary>
        /// 租赁中
        /// </summary>
        Rentaling = 0,
        /// <summary>
        /// 未结算
        /// </summary>
        Rentaled = 1,
        /// <summary>
        ///已结算
        /// </summary>
        RentalOk = 2,
        /// <summary>
        ///作废
        /// </summary>
        Invalid = -1
    }
}
