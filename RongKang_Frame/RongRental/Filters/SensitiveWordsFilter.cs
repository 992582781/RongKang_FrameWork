using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Common;

namespace RongRental.Filters
{
    public class SensitiveWordsFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var parameters = filterContext.ActionDescriptor.GetParameters();
            foreach (var parameter in parameters)
            {
                if (parameter.ParameterType == typeof(string))
                {
                    //获取字符串参数原值
                    var orginalValue = filterContext.ActionParameters[parameter.ParameterName] as string;
                    //使用过滤算法处理字符串
                    var filteredValue = PageValidate.validate_sql(orginalValue);
                    //将处理后值赋给参数
                    filterContext.ActionParameters[parameter.ParameterName] = filteredValue;
                }
            }


        }
    }
}