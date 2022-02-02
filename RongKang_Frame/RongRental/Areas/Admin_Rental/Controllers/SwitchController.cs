using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RongKang_Entity;
using RongKang_IBll;
using Web_Common;

namespace RongRental.Areas.Admin_Rental.Controllers
{
    public class SwitchController : Controller
    {
        private Message message = new Message();
        private JsonResult rs = null;
        private int User_ID = 0;

        ISwitchBll<Switch> SwitchBll;

        public SwitchController(ISwitchBll<Switch> SwitchBll) //依赖构造函数进行对象注入 
        {
            this.SwitchBll = SwitchBll; //在构造函数中初始化控制器类的Bll属性
            User_ID = Cookie_Operate.GetID();
        }
















        #region 对前端开放的下拉数据接口
        public ActionResult OnOff()
        {
            var View_Rental_VehicleS = SwitchBll.GetEntities(x => x.Switch_TypeVaule == 1).ToList().Select(x => new SelectData { ID = x.Switch_State.ToString(), Name = x.Switch_Name }).ToList();
            return Json(View_Rental_VehicleS, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DataOperat()
        {
            var View_Rental_VehicleS = SwitchBll.GetEntities(x => x.Switch_TypeVaule == 2).ToList().Select(x => new SelectData { ID = x.Switch_State.ToString(), Name = x.Switch_Name }).ToList();
            return Json(View_Rental_VehicleS, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ModuleType()
        {
            var View_Rental_VehicleS = SwitchBll.GetEntities(x => x.Switch_TypeVaule == 3).ToList().Select(x => new SelectData { ID = x.Switch_State.ToString(), Name = x.Switch_Name }).ToList();
            return Json(View_Rental_VehicleS, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ManageType()
        {
            var View_Rental_VehicleS = SwitchBll.GetEntities(x => x.Switch_TypeVaule == 4).ToList().Select(x => new SelectData { ID = x.Switch_Name.ToString(), Name = x.Switch_Name }).ToList();
            return Json(View_Rental_VehicleS, JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}