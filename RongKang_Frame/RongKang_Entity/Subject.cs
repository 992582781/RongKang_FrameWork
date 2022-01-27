using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RongKang_Entity
{
    /// <summary>
    /// 3大科目(基础数据)
    /// </summary>
    public class Subject
    {
        public int ID { get; set; }
        /// <summary>
        /// 科目名称
        /// </summary>
        public string SubjectName { get; set; }
        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }

}
