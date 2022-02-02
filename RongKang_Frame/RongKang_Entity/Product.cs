using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Common;

namespace RongKang_Entity
{
    /// <summary>
    /// 产品名称 (基础数据)
    /// </summary>
    [Table("RongKang_Product")]
    [Serializable]
    public class Product
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [FieldName(0, "产品名称", "", Validate.Empty, Control_Type.Text)]
        public string ProductName { get; set; }
        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }

}
