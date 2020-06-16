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
    /// RongKang_RoleModule:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("RongKang_RoleModule")]
    [Serializable]
    public partial class RoleModule
    {
        public RoleModule()
        { }
        #region Model
        private int _id;
        private int? _role_id;
        private int? _module_id;
        private DateTime? _rongkang_time = DateTime.Now;
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
        public int? Role_ID
        {
            set { _role_id = value; }
            get { return _role_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Module_ID
        {
            set { _module_id = value; }
            get { return _module_id; }
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

