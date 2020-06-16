using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RongKang_Entity;
using RongKang_IBll;
using RongKang_IDal;
using Web_Common;

namespace RongKang_Bll
{
    public class UserRoleBll : BaseBll<UserRole>, IUserRoleBll<UserRole>
    {
        private IUserRoleDal<UserRole> dal;
        public UserRoleBll(IUserRoleDal<UserRole> dal)
            : base(dal)
        {
            this.dal = dal;//在构造函数中初始化类的dal属性
        }

       


    }
}
