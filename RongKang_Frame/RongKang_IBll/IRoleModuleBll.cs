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
        /// �����ɫȨ�����ݸ����������
        /// </summary>
        /// <param name="exp">Lambda������where</param>
        /// <returns></returns>
         bool InsertRoleModule(IList<int> Modules, int Role_ID = 0);

         /// <summary>
         /// ��ȡ�û����н�ɫ��Ȩ��Module_ID
         /// </summary>
         /// <param name="model"></param>
         /// <returns></returns>
         IEnumerable<int> GetUserRoleModule(int User_ID);
 
         
         
    }
}
