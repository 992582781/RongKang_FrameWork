using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RongKang_IBll
{
    public interface IUserBll<T> : IBaseBll<T>
    {
        /// <summary>
        /// 用户和角色数据插入事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        bool InsertUserRole(T entity,IList<int> RoleID, out string messageStr, string User_ID);
        /// <summary>
        /// 用户和角色数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        bool UpdateUserRole(T entity, IList<int> RoleID, out string messageStr, string User_ID);

        /// <summary>
        /// 获取角色的所有用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<T> GetRoleUser(int Role_ID);
    }
}
