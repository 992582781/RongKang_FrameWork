using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RongKang_Entity
{
    /// <summary>
    /// 报销商业年度预算
    /// </summary>
    public class BranchOfficeYearBudget
    {
        public int ID { get; set; }
        /// <summary>
        /// 省份+负责人(基础数据)
        /// </summary>
        public int ProvincialRegionID { get; set; }
        /// <summary>
        /// 报销商业 (基础数据)
        /// </summary>
        public int BranchOfficeID { get; set; }

        /// <summary>
        /// 预算年度信息
        /// </summary>
        public int YearBudgetID { get; set; }
        /// <summary>
        /// 所属年度
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 预算资金 修改不能小于 BudgetFunds-AvailableBudgetFunds（已分配的）
        /// 或者UsedBudgetFunds
        /// </summary>
        public decimal BudgetFunds { get; set; }
        /// <summary>
        /// 可用预算资金
        /// </summary>
        public decimal AvailableBudgetFunds { get; set; }
        /// <summary>
        /// 已用预算资金
        /// </summary>
        public decimal UsedBudgetFunds { get; set; }
        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }
}
