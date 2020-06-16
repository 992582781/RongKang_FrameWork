using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RongKang_IBll
{
    public interface IRoleModuleBll<T> : IBaseBll<T>
    {
         /// <summary>
        /// 保存角色权限数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
         bool InsertRoleModule(IList<int> Modules, int Role_ID = 0);

         /// <summary>
         /// 获取用户所有角色的权限Module_ID
         /// </summary>
         /// <param name="model"></param>
         /// <returns></returns>
         IEnumerable<int> GetUserRoleModule(int User_ID);
 
         
         
    }
}
