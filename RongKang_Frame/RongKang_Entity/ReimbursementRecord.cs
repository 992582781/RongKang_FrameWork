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
    /// 报销记录
    /// </summary>
    [Table("RongKang_ReimbursementRecord")]
    [Serializable]
    public class ReimbursementRecord
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 报销日期，需要判断 年度预算是否存在；
        /// </summary>
        [FieldName(2, "报销日期", "", Validate.Required, Control_Type.TimeText)]
        public DateTime ReimbursementDate { get; set; }

        /// <summary>
        /// 报销日期，需要判断 年度预算是否存在；
        /// </summary>
        [FieldName(0, "报销日期", "", Validate.Required, Control_Type.TimeText)]
        [NotMapped]
        public string ReimbursementDateStr { get; set; }

        /// <summary>
        /// 报销的年
        /// </summary>
        [FieldName(0, "报销年度", "", Validate.Empty, Control_Type.Text)]
        public int Year { get; set; }
        /// <summary>
        /// 报销的月
        /// </summary>
        [FieldName(0, "报销月份", "", Validate.Empty, Control_Type.Text)]
        public int Month { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [FieldName(0, "所属省份", "", Validate.Empty, Control_Type.Text)]
        [NotMapped]
        public string ProvinceName { get; set; }

        /// <summary>
        /// 所属省份 (基础数据)
        /// </summary>
        [FieldName(2, "所属省份", "", Validate.Required, Control_Type.SelectText)]
        public int ProvincialRegion_ID { get; set; }

        /// <summary>
        /// 所属商业
        /// </summary>
        [FieldName(0, "所属商业", "", Validate.Empty, Control_Type.Text)]
        [NotMapped]
        public string BranchOfficeName { get; set; }

        /// <summary>
        /// 报销商业 (基础数据)
        /// </summary>
        [FieldName(2, "所属商业", "只能输入汉字", Validate.Required, Control_Type.SelectText)]
        public int BranchOffice_ID { get; set; }

        /// <summary>
        /// 未报销金额
        /// </summary>
        [FieldName(2, "未报销金额", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string AvailableBudgetFunds_2 { get; set; }

        /// <summary>
        /// 报销金额
        /// </summary>
        [FieldName(1, "报销金额", "只能输入数字", Validate.Number, Control_Type.NumberText)]
        [NotMapped]
        public string Funds_1 { get; set; }
        /// <summary>
        /// 报销金额
        /// </summary>
        public decimal? Funds { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [FieldName(1, "负责人", "只能输入汉字", Validate.Required, Control_Type.SelectText)]
        public string ProvincialRegion_Leader { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [FieldName(0, "所属项目", "", Validate.Empty, Control_Type.Text)]
        [NotMapped]
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        [FieldName(2, "所属项目", "只能输入汉字", Validate.Required, Control_Type.SelectText)]
        public int Project_ID { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [FieldName(0, "所属产品", "", Validate.Empty, Control_Type.Text)]
        [NotMapped]
        public string ProductName { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        [FieldName(2, "所属产品", "只能输入汉字", Validate.Required, Control_Type.SelectText)]
        public int Product_ID { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [FieldName(1, "备注", "", Validate.Empty, Control_Type.Remark)]
        public string Remark { get; set; }
        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }
}
