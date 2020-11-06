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
    /// RongKang_User:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("RongKang_User")]
    [Serializable]
    public partial class ViewUser
    {
        public ViewUser()
        { }
        #region Model
        private int _id;
        private string _user_name;
        private string _user_password;
        private int? _switch_onoff = 1;
        private string _switch_onoffstr = "";
        private DateTime? _rongkang_time = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        /// </summary

        [Key]
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "用户名称", "请输入用户名称", Validate.Required, Control_Type.Text)]
        public string User_Name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "用户密码", "请输入密码", Validate.Required, Control_Type.Text)]
        public string User_PassWord
        {
            set { _user_password = value; }
            get { return _user_password; }
        }


        /// <summary>
        /// 框架必须设置字段默认值1 开启，0标志关闭
        /// </summary>
        [FieldName(2, "状态", "下拉数据", Validate.Required, Control_Type.SelectText)]
        public int? Switch_OnOff
        {
            set { _switch_onoff = value; }
            get { return _switch_onoff; }
        }

        /// <summary>
        /// 框架必须设置字段默认值1 开启，0标志关闭
        /// </summary>
        [FieldName(1, "用户状态", "下拉数据", Validate.Required, Control_Type.SelectText)]
        public string Switch_OnOffstr
        {
            get
            {
                if (_switch_onoff == 1)
                    return "启用";
                else
                    return "停用";

            }
        }

        /// <summary>
        /// 框架必须设置字段默认值1 开启，0标志关闭
        /// </summary>
        [FieldName(1, "添加时间", "下拉数据", Validate.Required, Control_Type.TimeText)]
        public DateTime? RongKang_Time
        {
            set { _rongkang_time = value; }
            get { return _rongkang_time; }
        }
        #endregion Model

    }
}
