using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RongKang_IDal
{
    public interface IRoleDal<T> : IBaseDal<T>
    {
        /// <summary>
        /// 获取用户所有角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<T> GetUserRole(int User_ID);

        /// <summary>
        /// 获取所有用户和所有有角色的合集
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<T> GetUserAndRole();
    }
}
