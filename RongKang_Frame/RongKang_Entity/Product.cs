using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RongKang_Entity
{
    /// 产品名称
    /// </summary>
    public class Product
    {
        public int ID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }

}
