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
        [FieldName(1, "所属省份", "只能输入汉字", Validate.Required, Control_Type.Text)]
        public string ProvinceName { get; set; }
        /// <summary>
        /// 负责人 如：李明
        /// </summary>
        [FieldName(1, "所属负责人", "只能输入汉字", Validate.Required, Control_Type.Text)]
        public string Leader { get; set; }
        /// <summary>
        /// 预算资金 修改不能小于 BudgetFunds-AvailableBudgetFunds（已分配的）
        /// 或者UsedBudgetFunds
        /// </summary>
        [FieldName(0, "预算资金", "只能输入数字", Validate.Number, Control_Type.NumberText)]
        [NotMapped]
        public string BudgetFunds_1 { get; set; }
        /// <summary>
        /// 未分配预算资金
        /// </summary>
        [FieldName(0, "未分配预算资金", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string AvailableBudgetFunds_1 { get; set; }
        /// <summary>
        /// 已分配预算资金
        /// </summary>
        [FieldName(0, "已分配预算资金", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string UsedBudgetFunds_1 { get; set; }
        /// <summary>
        /// 管理费预算资金 修改不能小于 ManagementFunds-AvailableManagementFunds（已分配的）
        /// 或者UsedManagementFunds
        /// </summary>
        [FieldName(0, "管理费", "只能输入数字", Validate.Number, Control_Type.NumberText)]
        [NotMapped]
        public string ManagementFunds_1 { get; set; }
        /// <summary>
        /// 未分配管理费
        /// </summary>
        [FieldName(0, "未分配管理费", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string AvailableManagementFunds_1 { get; set; }
        /// <summary>
        /// 已分配管理费
        /// </summary>
        [FieldName(0, "已分配管理费", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        [NotMapped]
        public string UsedManagementFunds_1 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [FieldName(1, "备注", "", Validate.Empty, Control_Type.Remark)]
        public string Remark { get; set; }

        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }
}
