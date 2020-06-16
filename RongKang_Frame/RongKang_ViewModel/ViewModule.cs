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
    [Table("RongKang_ViewModule")]
    [Serializable]
    public partial class ViewModule
    {
        public ViewModule()
        { }
        #region Model
        private int _id;
        private string _module_name;
        private string _module_url;
        private int? _module_order;
        private string _module_key;
        private string _switch_moduletypename;
        private string _parent_name;
        private string _switch_onoffname;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "模块名称", "下拉数据", Validate.Required, Control_Type.Text)]
        public string Module_Name
        {
            set { _module_name = value; }
            get { return _module_name; }
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "模块类型", "", Validate.Required, Control_Type.Text)]
        public string Switch_ModuleTypeName
        {
            set { _switch_moduletypename = value; }
            get { return _switch_moduletypename; }
        }


        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "模块地址", "", Validate.Required, Control_Type.Text)]
        public string Module_Url
        {
            set { _module_url = value; }
            get { return _module_url; }
        }

        [FieldName(1, "模块顺序", "", Validate.Required, Control_Type.NumberText)]
        public int? Module_Order
        {
            set { _module_order = value; }
            get { return _module_order; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "键值", "", Validate.Required, Control_Type.Text)]
        public string Module_Key
        {
            set { _module_key = value; }
            get { return _module_key; }
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "父类", "", Validate.Required, Control_Type.Text)]
        public string Parent_Name
        {
            set { _parent_name = value; }
            get { return _parent_name; }
        }
        [FieldName(1, "状态", "", Validate.Empty, Control_Type.Text)]
        public string Switch_OnOffName
        {
            set { _switch_onoffname = value; }
            get { return _switch_onoffname; }
        }
        #endregion Model

    }
}
