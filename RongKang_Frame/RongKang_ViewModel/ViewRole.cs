using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Common;


namespace RongKang_ViewModel
{
    /// <summary>
    /// View_Module:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("RongKang_ViewRole")]
    [Serializable]
    public partial class ViewRole
    {
        public ViewRole()
        { }
        #region Model
        private int _id;
        private string _role_name;
        private string _role_remark;
        private string _switch_onoffname;
        /// <summary>
        /// 
        /// </summary>
        /// 
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
       [FieldName(1, "名称", "", Validate.Required, Control_Type.Text)]
        public string Role_Name
        {
            set { _role_name = value; }
            get { return _role_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "备注", "", Validate.Required, Control_Type.Text)]
        public string Role_Remark
        {
            set { _role_remark = value; }
            get { return _role_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "状态", "", Validate.Required, Control_Type.Text)]
        public string Switch_OnOffName
        {
            set { _switch_onoffname = value; }
            get { return _switch_onoffname; }
        }
        #endregion Model

    }
}

