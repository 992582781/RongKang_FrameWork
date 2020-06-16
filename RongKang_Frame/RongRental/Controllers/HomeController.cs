using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Common;
using Redis;

namespace RongRental.Controllers
{
    public class HomeController : Controller
    {

        //RedisSession  _RedisSession = new RedisSession(true, 5);

        public ActionResult Index()
        {
            //Redis_Operate r = new Redis_Operate();
            //r.Insert("wangjiankang", "30");
            //if (_RedisSession != null)
            //_RedisSession["www"] = "df444d";
 
            Response.Redirect("Admin_Rental/Module/Edit", true);
            
            return View();
        }



        public ActionResult tt(int s)
        {
            ////Redis_Operate r = new Redis_Operate();
            ////r.Insert("wangjiankang", "30");
            //if (_RedisSession == null)
            //    _RedisSession = new RedisSession(true, 1);
            //_RedisSession.Remove("www");
 
            //Response.Redirect("Admin_Rental/Module/Edit", true);

            return View();
        }

 

    }
}