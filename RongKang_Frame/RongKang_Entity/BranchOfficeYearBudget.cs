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
    /// 报销商业年度预算
    /// </summary>
    [Table("RongKang_BranchOfficeYearBudget")]
    [Serializable]

    public class BranchOfficeYearBudget
    {
        [Key]
        public int ID { get; set; }

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
        /// 省份
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
        /// 管理费企业
        /// </summary>
        [FieldName(1, "管理费商业", "下拉数据", Validate.Required, Control_Type.SelectText)]
        public string Switch_ManageType { get; set; }
        /// <summary>
        /// 未分配预算资金
        /// </summary>
        [FieldName(2, "未分配预算资金", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string AvailableBudgetFunds_2 { get; set; }
        /// <summary>
        /// 未分配管理费
        /// </summary>
        [FieldName(2, "未分配管理费", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string AvailableManagementFunds_2 { get; set; }
        /// <summary>
        /// 所属年度
        /// </summary>
        [FieldName(1, "所属年度", "只能输入数字", Validate.Number, Control_Type.Text)]
        public int? Year { get; set; }
        /// <summary>
        /// 商业预算资金 
        /// 或者UsedBudgetFunds
        /// </summary>
        [FieldName(1, "预算资金", "只能输入数字", Validate.Number, Control_Type.NumberText)]
        [NotMapped]
        public string BudgetFunds_1 { get; set; }
        /// <summary>
        /// 商业预算资金 
        /// 或者UsedBudgetFunds
        /// </summary>
        public decimal? BudgetFunds { get; set; }

        /// <summary>
        /// 商业可用预算资金 
        /// 或者UsedBudgetFunds
        /// </summary>
        [FieldName(1, "可用预算资金", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string AvailableBudgetFunds_1 { get; set; }

        /// <summary>
        /// 商业可用预算资金
        /// </summary>
        public decimal? AvailableBudgetFunds { get; set; }

        /// <summary>
        /// 商业已用预算资金 
        /// 或者UsedBudgetFunds
        /// </summary>
        [FieldName(1, "已用预算资金", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string UsedBudgetFunds_1 { get; set; }
        /// <summary>
        /// 商业已用预算资金
        /// </summary>
        public decimal? UsedBudgetFunds { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [FieldName(1, "备注", "", Validate.Empty, Control_Type.Remark)]
        public string Remark { get; set; }
        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }
}
