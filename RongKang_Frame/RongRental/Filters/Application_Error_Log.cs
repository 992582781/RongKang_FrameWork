using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Common;

namespace RongRental.Filters
{
    public class Application_Error_Log : IExceptionFilter
    {
        #region IExceptionFilter 成员

        private static readonly object obj = new object();

        public void OnException(ExceptionContext filterContext)
        {
            lock (obj)
            {
                var httpContext = filterContext.RequestContext.HttpContext.Request;
                // 在出现未处理的错误时运行的代码
                if (!httpContext.Url.ToString().Contains("."))
                {
                    string logText = "\r\n-------------  异常信息   ---------------------------------------------------------------";
                    logText += "\r\n发生时间：" + DateTime.Now.ToString();
                    logText += "\r\n发生异常页：" + httpContext.Url.ToString();
                    logText += "\r\n异常信息：" + filterContext.Exception.Message;
                    logText += "\r\n错误源：" + filterContext.Exception.Source;
                    logText += "\r\n堆栈信息：" + filterContext.Exception.StackTrace;
                    logText += "\r\n客户端IP：" + PageValidate.GetHostAddress();
                    logText += "\r\n-----------------------------------------------------------------------------------------\r\n";
                    //日志物理路径
                    string path = httpContext.MapPath("~/Log/");
                    RongRental_Web_Log.WriteLog(logText, "");
                    filterContext.RequestContext.HttpContext.Server.ClearError();
                    //filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Error");
                }
            }
        }
        #endregion
    }
}