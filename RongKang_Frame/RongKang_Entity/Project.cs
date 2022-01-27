using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RongKang_Entity
{
    /// <summary>
    /// 5个项目
    /// </summary>
    public class Project
    {
        public int ID { get; set; }
        /// <summary>
        /// 科目ID
        /// </summary>
        public string SubjectID { get; set; }
        //项目名称
        public string ProjectName { get; set; }
        /// <summary>
        /// 报销比例
        /// </summary>
        public decimal Ratio { get; set; }
        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }
}
