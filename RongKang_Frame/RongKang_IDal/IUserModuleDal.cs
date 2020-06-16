using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RongKang_IDal
{
    public interface IUserModuleDal<T> : IBaseDal<T>
    {
        /// <summary>
        /// 保存用户权限数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        bool InsertUserModule(IList<int> Modules, int User_ID = 0);
    }
}
