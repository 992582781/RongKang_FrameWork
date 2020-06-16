using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RongRental.Areas.Admin_Rental.Filters
{
    public class RongAuthorizeAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Cookies["RongKang_User"] == null)
            {
                //filterContext.HttpContext.Response.Redirect("/SunshineH5/Login");
                string ReturnUrl = filterContext.RequestContext.HttpContext.Request.Url.ToString();//当前请求的url
                filterContext.Result = new RedirectToRouteResult(
                     new RouteValueDictionary {
         { "action", "Index" },
         { "controller", "Login" },
         { "ReturnUrl", ReturnUrl}
                     }
                );
            }
        }

    }
}