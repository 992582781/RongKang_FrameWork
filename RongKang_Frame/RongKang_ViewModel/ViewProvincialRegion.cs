using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Common;

namespace RongKang_ViewModel
{
    public class ViewProvincialRegion
    {
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

        /// <summary>
        /// 预算资金 修改不能小于 BudgetFunds-AvailableBudgetFunds（已分配的）
        /// 或者UsedBudgetFunds
        /// </summary>
        [FieldName(1, "预算资金", "只能输入数字", Validate.Required, Control_Type.Text)]
        public decimal BudgetFunds { get; set; }
        /// <summary>
        /// 可用预算资金
        /// </summary>
        [FieldName(1, "可用资金", "只能输入数字", Validate.Required, Control_Type.Text)]
        public decimal AvailableBudgetFunds { get; set; }
        /// <summary>
        /// 已用预算资金
        /// </summary>
        [FieldName(1, "已用资金", "只能输入数字", Validate.Required, Control_Type.Text)]
        public decimal UsedBudgetFunds { get; set; }
    }
}
