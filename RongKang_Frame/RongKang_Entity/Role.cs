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
    /// RongKang_Role:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("RongKang_Role")]
    [Serializable]
    public partial class Role
    {
        public Role()
        { }
        #region Model
        private int _id;
        private string _role_name;
        private string _role_remark;
        private DateTime? _rongkang_time = DateTime.Now;
        private int? _switch_onoff = 1;

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
        [FieldName(1, "名称", "输入角色名称", Validate.Required, Control_Type.Text)]
        public string Role_Name
        {
            set { _role_name = value; }
            get { return _role_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "备注", "请输入角色说明", Validate.Required, Control_Type.Text)]
        public string Role_Remark
        {
            set { _role_remark = value; }
            get { return _role_remark; }
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "状态", "下拉数据", Validate.Required, Control_Type.SelectText)]
        /// <summary>
        /// 表示是否开启1表示开启0表示关闭
        /// </summary>
        public int? Switch_OnOff
        {
            set { _switch_onoff = value; }
            get { return _switch_onoff; }
        }

        #endregion Model

    }
}
