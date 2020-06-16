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
    /// RongKang_User:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("RongKang_User")]
    [Serializable]
    public partial class User
    {
        public User()
        { }
        #region Model
        private int _id;
        private string _user_name;
        private string _user_password;
        private int? _switch_onoff = 1;
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
        [FieldName(1, "户名", "请输入用户名称", Validate.Required, Control_Type.Text)]
        public string User_Name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        [FieldName(1, "密码", "请输入密码", Validate.Required, Control_Type.Text)]
        public string User_PassWord
        {
            set { _user_password = value; }
            get { return _user_password; }
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

        /// <summary>
        /// 
        /// </summary>
        public DateTime? RongKang_Time
        {
            set { _rongkang_time = value; }
            get { return _rongkang_time; }
        }
        #endregion Model

    }
}

