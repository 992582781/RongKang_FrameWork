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
    /// 省份+负责人(基础数据)
    /// </summary>
    [Table("RongKang_ProvincialRegion")]
    [Serializable]
    public class ProvincialRegion
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 省份 如：江苏省
        /// </summary>
        [FieldName(1, "省份", "只能输入汉字", Validate.Required, Control_Type.Text)]
        public string ProvinceName { get; set; }
        /// <summary>
        /// 负责人 如：李明
        /// </summary>
        [FieldName(1, "负责人", "只能输入汉字", Validate.Required, Control_Type.Text)]
        public string Leader { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [FieldName(1, "备注", "", Validate.Empty, Control_Type.Text)]
        public string Remark { get; set; }

        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }
}
