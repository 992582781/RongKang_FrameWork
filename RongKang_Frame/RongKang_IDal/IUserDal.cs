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
        /// �û��ͽ�ɫ���ݸ����������
        /// </summary>
        /// <param name="exp">Lambda������where</param>
        /// <returns></returns>
        bool InsertUserRole(T entity, IList<int> RoleID);

        /// <summary>
        /// �û��ͽ�ɫ���ݸ����������
        /// </summary>
        /// <param name="exp">Lambda������where</param>
        /// <returns></returns>
        bool UpdateUserRole(T entity, IList<int> RoleID);





        /// <summary>
        /// ��ȡ��ɫ�������û�
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<T> GetRoleUser(int Role_ID);

    }
}