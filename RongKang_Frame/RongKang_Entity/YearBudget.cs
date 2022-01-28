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
    /// 省份+负责人年度预算
    /// </summary>
    [Table("RongKang_YearBudget")]
    [Serializable]
    public class YearBudget
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [FieldName(0, "省份", "", Validate.Empty, Control_Type.Text)]
        [NotMapped]
        public string ProvinceName { get; set; }

        /// <summary>
        /// 省份+负责人(基础数据)
        /// </summary>
        [FieldName(2, "省份", "只能输入汉字", Validate.Required, Control_Type.SelectText)]
        public int ProvincialRegion_ID { get; set; }
        /// <summary>
        /// 所属年度
        /// </summary>
        //[FieldName(1, "所属年度", "只能输入数字", Validate.Number, Control_Type.NumberText)]
        public int? Year { get; set; }
        /// <summary>
        /// 预算资金 修改不能小于 BudgetFunds-AvailableBudgetFunds（已分配的）
        /// 或者UsedBudgetFunds
        /// </summary>
        [FieldName(1, "预算资金", "只能输入数字", Validate.Number, Control_Type.NumberText)]
        [NotMapped]
        public string BudgetFunds_1 { get; set; }

        public decimal? BudgetFunds { get; set; }
        /// <summary>
        /// 可用预算资金
        /// </summary>
        [FieldName(1, "可用预算资金", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string AvailableBudgetFunds_1 { get; set; }

        public decimal? AvailableBudgetFunds { get; set; }
        /// <summary>
        /// 已用预算资金
        /// </summary>
        [FieldName(1, "已用预算资金", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string UsedBudgetFunds_1 { get; set; }

        public decimal? UsedBudgetFunds { get; set; }

        /// <summary>
        /// 管理费预算资金 修改不能小于 ManagementFunds-AvailableManagementFunds（已分配的）
        /// 或者UsedManagementFunds
        /// </summary>
        [FieldName(1, "管理费", "只能输入数字", Validate.Number, Control_Type.NumberText)]
        [NotMapped]
        public string ManagementFunds_1 { get; set; }

        public decimal? ManagementFunds { get; set; }
        /// <summary>
        /// 可用管理费预算资金
        /// </summary>
        [FieldName(1, "可用管理费", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string AvailableManagementFunds_1 { get; set; }

        public decimal? AvailableManagementFunds { get; set; }
        /// <summary>
        /// 已用管理费预算资金
        /// </summary>
        [FieldName(1, "已用管理费", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string UsedManagementFunds_1 { get; set; }

        public decimal? UsedManagementFunds { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [FieldName(1, "备注", "", Validate.Empty, Control_Type.Remark)]
        public string Remark { get; set; }

        public int UserID { get; set; }
        public DateTime InTime { get; set; }

    }
}
