using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RongKang_IDal
{
    public interface IUserDal<T> : IBaseDal<T>
    {
        /// <summary>
        /// 用户和角色数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        bool InsertUserRole(T entity, IList<int> RoleID);

        /// <summary>
        /// 用户和角色数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        bool UpdateUserRole(T entity, IList<int> RoleID);





        /// <summary>
        /// 获取角色的所有用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<T> GetRoleUser(int Role_ID);

    }
}