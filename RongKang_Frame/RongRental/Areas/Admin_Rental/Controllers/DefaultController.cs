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


        public DefaultController() //�������캯�����ж���ע�� 
        {
            User_ID = Cookie_Operate.GetID();
        }
 

        #region ��ʾ�б�
        /// </summary>
        /// <param name="page">��ǰҳ��</param>
        /// <param name="pageSize">��ҳ����</param>
        /// <param name="Searchtext">��ѯ����</param>
        /// <param name="Selecte_parameter">��ѯ�ֶ�</param>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #endregion





    }
}