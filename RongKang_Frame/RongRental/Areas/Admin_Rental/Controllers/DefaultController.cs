using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using RongKang_Entity;
using RongKang_IBll;
using RongKang_ViewModel;
using RongRental.Areas.Admin_Rental.Filters;
using Web_Common;

namespace RongRental.Areas.Admin_Rental.Controllers
{
    [RongAuthorize]
    public class DefaultController : Controller
    {

        private Message message = new Message();
        private JsonResult rs = null;
        private int User_ID = 0;

        ICustomerBll<Customer> CustomerBll;


        public DefaultController() //依赖构造函数进行对象注入 
        {
            User_ID = Cookie_Operate.GetID();
        }
 

        #region 显示列表
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="pageSize">分页条数</param>
        /// <param name="Searchtext">查询内容</param>
        /// <param name="Selecte_parameter">查询字段</param>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #endregion





    }
}