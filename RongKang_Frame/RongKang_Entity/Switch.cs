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
    /// RongKang_Switch:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("RongKang_Switch")]
    [Serializable]
    public partial class Switch
    {
        public Switch()
        { }
        #region Model
        private int _id;
        private string _switch_name;
        private string _switch_state;
        private int _switch_typevaule;
        private string _switch_typetext;

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
        [FieldName(1, "名称", "输入名称", Validate.Required, Control_Type.Text)]
        public string Switch_Name
        {
            set { _switch_name = value; }
            get { return _switch_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "状态", "状态值", Validate.Required, Control_Type.Text)]
        public string Switch_State
        {
            set { _switch_state = value; }
            get { return _switch_state; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "类型", "类型值", Validate.Required, Control_Type.Text)]
        public int Switch_TypeVaule
        {
            set { _switch_typevaule = value; }
            get { return _switch_typevaule; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "类型", "类型文本", Validate.Required, Control_Type.Text)]
        public string Switch_TypeText
        {
            set { _switch_typetext = value; }
            get { return _switch_typetext; }
        }

        #endregion Model

    }
}

