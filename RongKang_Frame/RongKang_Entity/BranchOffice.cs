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
    /// 报销商业 (基础数据)
    /// </summary>
    [Table("RongKang_BranchOffice")]
    [Serializable]
    public class BranchOffice
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
        [FieldName(2, "省份", "", Validate.Required, Control_Type.SelectText)]
        public int ProvincialRegion_ID { get; set; }
        /// <summary>
        /// 商业名称
        /// </summary>
        [FieldName(1, "商业名称", "", Validate.Required, Control_Type.Text)]
        public string BranchOfficeName { get; set; }

        ///// <summary>
        ///// 管理费企业
        ///// </summary>
        //[FieldName(0, "管理费商业", "下拉数据", Validate.Required, Control_Type.SelectText)]
        //[NotMapped]
        //public string Switch_ManageType { get; set; }

        /// <summary>
        /// 省份 如：**
        /// </summary>
        [FieldName(1, "法人", "只能输入汉字", Validate.Required, Control_Type.Text)]
        public string Legal { get; set; }

        /// <summary>
        /// 省份 如：**
        /// </summary>
        [FieldName(1, "业务员", "只能输入汉字", Validate.Required, Control_Type.Text)]
        public string Salesman { get; set; }


        #region 暂时注释

        /// <summary>
        /// 预算资金 修改不能小于 BudgetFunds-AvailableBudgetFunds（已分配的）
        /// 或者UsedBudgetFunds
        /// </summary>
        //[FieldName(0, "预算资金", "只能输入数字", Validate.Number, Control_Type.NumberText)]
        //[NotMapped]
        //public string BudgetFunds_1 { get; set; }
        ///// <summary>
        ///// 可用预算资金
        ///// </summary>
        //[FieldName(0, "可用预算资金", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        //[NotMapped]
        //public string AvailableBudgetFunds_1 { get; set; }
        ///// <summary>
        ///// 已用预算资金
        ///// </summary>
        //[FieldName(0, "已用预算资金", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        //[NotMapped]
        //public string UsedBudgetFunds_1 { get; set; }

        /// <summary>
        /// 广乐总费用
        /// </summary>
        //[FieldName(0, "广乐总费用", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        //[NotMapped]
        //public string GuangLeFunds_1 { get; set; }


        ///// <summary>
        ///// 对私总费用
        ///// </summary>
        //[FieldName(0, "对私总费用", "只能输入数字", Validate.Number, Control_Type.Readonly)]
        //[NotMapped]
        //public string PersonFunds_1 { get; set; }

        #endregion


        /// <summary>
        /// 备注
        /// </summary>
        [FieldName(1, "备注", "", Validate.Empty, Control_Type.Remark)]
        public string Remark { get; set; }



        public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }
}
