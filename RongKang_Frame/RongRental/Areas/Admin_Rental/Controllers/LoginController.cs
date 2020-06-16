using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using RongKang_Entity;
using RongKang_IBll;
using RongKang_ViewModel;
using Web_Common;


namespace RongRental.Areas.Admin_Rental.Controllers
{
    public class LoginController : Controller
    {
        private int result = 0;
        private Message message = new Message();
        private JsonResult rs = null;

        IUserBll<User> UserBll;
        IModuleBll<Module> ModuleBll;

        public LoginController(IUserBll<User> UserBll, IModuleBll<Module> ModuleBll) //依赖构造函数进行对象注入 
        {
            this.UserBll = UserBll; //在构造函数中初始化控制器类的Bll属性
            this.ModuleBll = ModuleBll;
        }

        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(User rental_user)
        {

            rental_user = UserBll.GetEntity(p => p.User_Name == rental_user.User_Name.Trim() && p.User_PassWord == rental_user.User_PassWord.Trim());

            if (rental_user != null)
            {
                HttpCookie cookie = new HttpCookie("RongKang_User");
                cookie.Values.Add("User_Name", EncryptUtil.Des(rental_user.User_Name.ToString()));
                cookie.Values.Add("ID", EncryptUtil.Des(rental_user.ID.ToString()));

                var list = ModuleBll.GetUserRoleModuleModel(rental_user.ID).ToList();

                CacheHelper.Permanent(rental_user.User_Name, EncryptUtil.Des(T_Conversion_Json.ListToJSONString(list)));

                Response.Cookies.Add(cookie);

                if (!CacheHelper.Exists("sysModule"))
                {
                    ModuleCache();
                }

                message.Status = true;
                message.Msg = "登录成功，准备跳转！";
                rs = Json(message);
                rs.ContentType = "text/html";
                return rs;
            }
            else
            {
                message.Status = false;
                message.Msg = "登录失败，用户名或密码错误！";
                rs = Json(message);
                rs.ContentType = "text/html";
                return rs;
            }
        }


        public ActionResult Logout(User rental_user)
        {
            HttpCookie RongKang_User = Request.Cookies["RongKang_User"];
            if (RongKang_User != null)
            {
                CacheHelper.Remove(Cookie_Operate.GetUser_Name());
                TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                RongKang_User.Expires = DateTime.Now.Add(ts);//删除整个Cookie，只要把过期时间设置为现在
                Response.AppendCookie(RongKang_User);
            }
            return View("../Login/Index");
        }




        /// <summary>
        /// 把所有模块的数据信息存入Cache
        /// </summary>
        public  void ModuleCache()
        {
            var list = ModuleBll.GetEntities(x => x.ID > 0).ToList();
            CacheHelper.Permanent("sysModule", EncryptUtil.Des(T_Conversion_Json.ListToJSONString(list)));
        }
    }
}