using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RongKang_IDal
{
    public interface IModuleDal<T> : IBaseDal<T>
    {
        /// <summary>
        /// ��ȡ�û�����Ȩ��RoleModule
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<T> GetUserRoleModuleModel(int User_ID);


    }
}
