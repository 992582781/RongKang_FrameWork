using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RongKang_Entity;
using RongKang_ViewModel;
using RongKang_IBll;
using RongKang_IDal;
namespace RongKang_Bll
{
    public class UserModuleBll : BaseBll<UserModule>, IUserModuleBll<UserModule>
    {
        private IUserModuleDal<UserModule> dal;
        public UserModuleBll(IUserModuleDal<UserModule> dal)
            : base(dal)
        {
            this.dal = dal;//在构造函数中初始化类的dal属性
        }

        /// <summary>
        /// 保存用户权限数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual bool InsertUserModule(IList<int> Modules, int User_ID = 0)
        {

            return dal.InsertUserModule(Modules, User_ID);
        }
    }
}
