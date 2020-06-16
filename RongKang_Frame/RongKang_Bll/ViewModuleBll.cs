using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RongKang_Entity;
using RongKang_IBll;
using RongKang_IDal;
using RongKang_ViewModel;


namespace RongKang_Bll
{
    public class ViewModuleBll : BaseBll<ViewModule>, IViewModuleBll<ViewModule>
    {
        private IViewModuleDal<ViewModule> dal;
        public ViewModuleBll(IViewModuleDal<ViewModule> dal)
            : base(dal)
        {
            this.dal = dal;//在构造函数中初始化类的dal属性
        }
    }
}