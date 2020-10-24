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
    public class ViewUserBll : BaseBll<ViewUser>, IViewUserBll<ViewUser>
    {
        private IViewUserDal<ViewUser> dal;
        public ViewUserBll(IViewUserDal<ViewUser> dal)
        : base(dal)
        {
            this.dal = dal;//在构造函数中初始化类的dal属性
        }
    }
}
