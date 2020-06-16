using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RongKang_Entity;
using RongKang_ViewModel;
using RongKang_IBll;
using RongKang_IDal;
namespace RongKang_Bll
{
    public class RoleBll : BaseBll<Role>, IRoleBll<Role>
    {
        private IRoleDal<Role> dal;
        public RoleBll(IRoleDal<Role> dal)
            : base(dal)
        {
            this.dal = dal;//在构造函数中初始化类的dal属性
        }

        /// <summary>
        /// 获取用户所有角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual IEnumerable<Role> GetUserRole(int User_ID)
        {

            return dal.GetUserRole(User_ID);
        }

        /// <summary>
        /// 获取所有用户和所有有角色的合集
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual IEnumerable<Role> GetUserAndRole()
        {

            return dal.GetUserAndRole();
        }
    }
}
