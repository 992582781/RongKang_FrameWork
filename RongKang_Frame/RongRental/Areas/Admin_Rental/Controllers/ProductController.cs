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
    public class ProductController : Controller
    {

        private Message message = new Message();
        private JsonResult rs = null;
        private int User_ID = 0;

        IProductBll<Product> ProductBll;

        public ProductController(IProductBll<Product> ProductBll) //�������캯�����ж���ע�� 
        {
            this.ProductBll = ProductBll; //�ڹ��캯���г�ʼ�����������Bll����
            User_ID = Cookie_Operate.GetID();
        }

        #region ��ǰ�˿��ŵ��������ݽӿ�
        public ActionResult ID()
        {
            var View_Rental_VehicleS = ProductBll.GetEntities(x => x.ID > 0).ToList().Select(x => new SelectData { ID = x.ID.ToString(), Name = x.ProductName }).ToList();
            return Json(View_Rental_VehicleS, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}