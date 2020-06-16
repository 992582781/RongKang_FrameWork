using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RongKang_IBll
{
    public interface IModuleBll<T> : IBaseBll<T>
    {
        /// <summary>
        /// 获取用户所有权限RoleModule
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<T> GetUserRoleModuleModel(int User_ID);
    }
}
