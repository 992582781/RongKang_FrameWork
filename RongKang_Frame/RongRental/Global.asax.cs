using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using RongRental.App_Start;
using RongRental.Filters;

namespace RongRental
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer<DbContext>(null);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalFilters.Filters.Add(new Application_Error_Log());
            GlobalFilters.Filters.Add(new SensitiveWordsFilter());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
             
            //第一： 在网站一启动的时候就初始化AutoFac的相关功能
            /*
             1.0 告诉AutoFac初始化数据仓储层FB.CMS.Repository.dll中所有类的对象实例。这些对象实例以其接口的形式保存在AutoFac容器中
             2.0 告诉AutoFac初始化业务逻辑层FB.CMS.Services.dll中所有类的对象实例。这些对象实例以其接口的形式保存在AutoFac容器中
             3.0 将MVC默认的控制器工厂替换成AutoFac的工厂
             */

            //具体做法就是我们去App_Start文件夹下创建一个AutoFacConfig类，具体实现什么功能去这个类中实现。然后再这里调用下这个类
            AutoFacConfig.Register();
           

        }
    }
}
