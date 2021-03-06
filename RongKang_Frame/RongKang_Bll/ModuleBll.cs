using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RongKang_Entity;
using RongKang_IBll;
using RongKang_IDal;
namespace RongKang_Bll
{
    public class ModuleBll : BaseBll<Module>, IModuleBll<Module>
    {
        private IModuleDal<Module> dal;
        public ModuleBll(IModuleDal<Module> dal)
            : base(dal)
        {
            this.dal = dal;//在构造函数中初始化类的dal属性
        }


        /// <summary>
        /// 获取用户所有权限RoleModule
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual IEnumerable<Module> GetUserRoleModuleModel(int User_ID)
        {
            return dal.GetUserRoleModuleModel(User_ID);
        }
    }
}
