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
        /// �����û�Ȩ�����ݸ����������
        /// </summary>
        /// <param name="exp">Lambda������where</param>
        /// <returns></returns>
        bool InsertUserModule(IList<int> Modules, int User_ID = 0);
    }
}
