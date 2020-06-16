using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RongKang_IBll
{
    public interface IRoleBll<T> : IBaseBll<T>
    {
        /// <summary>
        /// ��ȡ�û����н�ɫ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<T> GetUserRole(int User_ID);

        /// <summary>
        /// ��ȡ�����û��������н�ɫ�ĺϼ�
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         IEnumerable<T> GetUserAndRole();
        
    }
}