using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RongKang_Entity
{
    /// <summary>
    /// 报销商业 (基础数据)
    /// </summary>
    public class BranchOffice
    {
        public int ID { get; set; }
        /// <summary>
        /// 省份+负责人(基础数据)
        /// </summary>
        public int ProvincialRegionID { get; set; }
        /// <summary>
        /// 商业名称
        /// </summary>
        public string BranchOfficeName { get; set; }
        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }
}
