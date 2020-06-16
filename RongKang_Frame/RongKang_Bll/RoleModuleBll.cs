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
    public class RoleModuleBll : BaseBll<RoleModule>, IRoleModuleBll<RoleModule>
    {
        private IRoleModuleDal<RoleModule> dal;
        public RoleModuleBll(IRoleModuleDal<RoleModule> dal)
            : base(dal)
        {
            this.dal = dal;//在构造函数中初始化类的dal属性
        }

        /// <summary>
        /// 保存角色权限数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual bool InsertRoleModule(IList<int> Modules, int Role_ID = 0)
        {

            return dal.InsertRoleModule(Modules, Role_ID);
        }

        /// <summary>
        /// 获取用户所有角色的权限Module_ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual IEnumerable<int> GetUserRoleModule(int User_ID)
        {
            return dal.GetUserRoleModule(User_ID);
        }


    }
}
