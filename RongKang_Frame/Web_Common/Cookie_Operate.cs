using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web_Common
{
    public class Cookie_Operate
    {
        /// <summary>
        /// 返回登录用户ID
        /// </summary>
        /// <returns></returns>
        public static int GetID()
        {
            if (HttpContext.Current.Request.Cookies["RongKang_User"] == null)
            {
                return 0;
            }
            else
            {
                return Number_Conversion.ObjToInt(EncryptUtil.UnDes(HttpContext.Current.Request.Cookies["RongKang_User"]["ID"]), 0);
            }
        }

        /// <summary>
        /// 返回登录用户登录名称
        /// </summary>
        /// <returns></returns>
        public static string GetUser_Name()
        {
            if (HttpContext.Current.Request.Cookies["RongKang_User"] == null)
            {
                return "";
            }
            else
            {
                return EncryptUtil.UnDes(HttpContext.Current.Request.Cookies["RongKang_User"]["User_Name"]);
            }
        }

        /// <summary>
        /// 返回登录用户权限
        /// </summary>
        /// <returns></returns>
        public static int GetFlag()
        {
            if (HttpContext.Current.Request.Cookies["RongKang_User"] == null)
            {
                return 0;
            }
            else
            {
                return Number_Conversion.ObjToInt(EncryptUtil.UnDes(HttpContext.Current.Request.Cookies["RongKang_User"]["Rental_User_Flag"]), 0);
            }
        }
 
    }
}
 