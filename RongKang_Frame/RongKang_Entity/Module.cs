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
    [Table("RongKang_Module")]
    [Serializable]
    public partial class Module
    {
        public Module()
        { }
        #region Model
        private int _id;
        private int? _switch_moduletype;
        private string _module_name;
        private string _module_url;
        private int _module_parentid;
        private string _module_key;
        private int? _module_order;
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
       [FieldName(1, "模块类型", "下拉数据", Validate.Required, Control_Type.SelectText)]
        public int? Switch_ModuleType
        {
            set { _switch_moduletype = value; }
            get { return _switch_moduletype; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "模块名称", "只能输入汉子", Validate.Required, Control_Type.Text)]
        public string Module_Name
        {
            set { _module_name = value; }
            get { return _module_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "模块地址", "输入正确控制器地址", Validate.Required, Control_Type.Text)]
        public string Module_Url
        {
            set { _module_url = value; }
            get { return _module_url; }
        }
        /// <summary>
        /// 
        /// </summary>
       [FieldName(1, "选择父类", "下拉数据", Validate.Empty, Control_Type.SelectText)]
        public int Module_ParentID
        {
            set { _module_parentid = value; }
            get { return _module_parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "键值", "按钮类型请输入键值", Validate.Empty, Control_Type.Text)]
        public string Module_Key
        {
            set { _module_key = value; }
            get { return _module_key; }
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "排序", "请输入顺序", Validate.Number, Control_Type.Text)]
        public int? Module_Order
        {
            set { _module_order = value; }
            get { return _module_order; }
        }

        /// <summary>
        /// 框架必须设置字段默认值1 开启，0标志关闭
        /// </summary>
       [FieldName(1, "状态", "下拉数据", Validate.Required, Control_Type.SelectText)]
        public int? Switch_OnOff
        {
            set { _switch_onoff = value; }
            get { return _switch_onoff; }
        }
        #endregion Model

    }
}

