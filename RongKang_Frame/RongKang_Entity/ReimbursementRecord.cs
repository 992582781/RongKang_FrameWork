using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RongKang_Entity
{
    /// <summary>
    /// 报销记录
    /// </summary>
    public class ReimbursementRecord
    {
        public int ID { get; set; }
        /// <summary>
        /// 报销日期，需要判断 年度预算是否存在；
        /// </summary>
        public DateTime ReimbursementDate { get; set; }
        /// <summary>
        /// 报销的年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 报销的月
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 报销商业
        /// </summary>
        public int BranchOfficeID { get; set; }

        /// <summary>
        /// 报销金额
        /// </summary>
        public decimal Funds { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public int ProvincialRegionID { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID { get; set; }

        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }
}
