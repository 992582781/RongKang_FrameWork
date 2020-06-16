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
        /// �û��ͽ�ɫ���ݲ����������
        /// </summary>
        /// <param name="exp">Lambda������where</param>
        /// <returns></returns>
        bool InsertUserRole(T entity,IList<int> RoleID, out string messageStr, string User_ID);
        /// <summary>
        /// �û��ͽ�ɫ���ݸ����������
        /// </summary>
        /// <param name="exp">Lambda������where</param>
        /// <returns></returns>
        bool UpdateUserRole(T entity, IList<int> RoleID, out string messageStr, string User_ID);

        /// <summary>
        /// ��ȡ��ɫ�������û�
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<T> GetRoleUser(int Role_ID);
    }
}
