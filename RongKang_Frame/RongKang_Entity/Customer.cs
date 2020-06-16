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
    /// RongKang_Module:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("RongKang_Customer")]
    [Serializable]
    public partial class Customer
    {
        public Customer()
        { }
        #region Model
        private int _id;
        private string _customer_name;
        private DateTime? _customer_time = DateTime.Now;
        private int? _switch_dataoperat;
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "名称", "只能输入汉子", Validate.Required, Control_Type.Text)]
        public string Customer_Name
        {
            set { _customer_name = value; }
            get { return _customer_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Customer_Time
        {
            set { _customer_time = value; }
            get { return _customer_time; }
        }
        /// <summary>
        /// 表示数据正常还是删除的标识
        /// </summary>
        [FieldName(1, "状态", "下拉数据", Validate.Required, Control_Type.SelectText)]
        public int? Switch_DataOperat
        {
            set { _switch_dataoperat = value; }
            get { return _switch_dataoperat; }
        }
        #endregion Model

    }
}

