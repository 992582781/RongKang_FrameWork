using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RongKang_Entity
{
    /// <summary>
    /// 省份+负责人管理费年度预算
    /// </summary>
    public class ManagementBudget
    {
        public int ID { get; set; }
        /// <summary>
        /// 省份+负责人(基础数据)
        /// </summary>
        public int ProvincialRegionID { get; set; }
        /// <summary>
        /// 所属年度
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 管理费预算资金 修改不能小于 ManagementFunds-AvailableManagementFunds（已分配的）
        /// 或者UsedManagementFunds
        /// </summary>
        public decimal ManagementFunds { get; set; }
        /// <summary>
        /// 可用管理费预算资金
        /// </summary>
        public decimal AvailableManagementFunds { get; set; }
        /// <summary>
        /// 已用管理费预算资金
        /// </summary>
        public decimal UsedManagementFunds { get; set; }
        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }
}
