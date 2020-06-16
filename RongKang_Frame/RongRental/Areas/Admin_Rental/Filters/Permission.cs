using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RongKang_Entity;
using Web_Common;

namespace RongRental.Areas.Admin_Rental.Filters
{
    public class Permission
    {
        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public static Boolean RightCheck(String controller, String action)
        {

            if (InitModules()==null)
            {
                return false;
            }
            else
            {

                IList<Module> List_Modules = InitModules();//获取登录用户所有用户模块
                var Iquery = List_Modules.Where(p => p.Module_Url.EndsWith(controller + "/" + action)).ToList();
                if (Iquery.Count() > 0)
                    return true;
                else
                    return false;
            }

        }


        /// <summary>
        /// 返回用户的权限模块
        /// </summary>
        /// <returns></returns>
        public static IList<Module> InitModules()
        {
            var User_Name = Cookie_Operate.GetUser_Name();

            if (string.IsNullOrEmpty(User_Name))
            {
                return null;
            }

            if (CacheHelper.Get(User_Name) == null)
            {
                return null;
            }
            else
            {
                return T_Conversion_Json.JSONStringToList<Module>(EncryptUtil.UnDes(CacheHelper.Get(User_Name).ToString()));//获取登录用户所有用户模块
            }

        }

        /// <summary>
        /// 返回用户点击的页面Module名称
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public static string ModuleName(String controller, String action)
        {

            if (InitModules()==null)
            {
                return "";
            }
            else
            {

                IList<Module> List_Modules = T_Conversion_Json.JSONStringToList<Module>(EncryptUtil.UnDes(CacheHelper.Get("sysModule").ToString()));//获取所有模块
                var Iquery = List_Modules.Where(p => p.Module_Url.EndsWith(controller + "/" + action)).FirstOrDefault();
                if (Iquery != null)
                {
                    var IqueryParent = List_Modules.Where(p => p.ID == Iquery.Module_ParentID).FirstOrDefault();

                    if (IqueryParent != null)
                    {
                        return IqueryParent.Module_Name + ">>" + Iquery.Module_Name;
                    }
                    else
                    {
                        return Iquery.Module_Name;
                    }
                }
                else
                {
                    return "";
                }
            }
        }

    }
}