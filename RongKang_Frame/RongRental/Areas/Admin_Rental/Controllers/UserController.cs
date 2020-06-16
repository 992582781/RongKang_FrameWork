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
    public class UserController : Controller
    {

        private Message message = new Message();
        private JsonResult rs = null;
        private int User_ID = 0;

        IUserBll<User> UserBll;
        IRoleBll<Role> RoleBll;
        IUserRoleBll<UserRole> UserRoleBll;

        public UserController(IUserBll<User> UserBll, IRoleBll<Role> RoleBll, IUserRoleBll<UserRole> UserRoleBll) //依赖构造函数进行对象注入 
        {
            this.UserBll = UserBll; //在构造函数中初始化控制器类的Bll属性
            this.RoleBll = RoleBll;
            this.UserRoleBll = UserRoleBll;
            User_ID = Cookie_Operate.GetID();
        }



        #region 添加修改删除代码
        public ActionResult Edit(int ID = 0)
        {
            try
            {
                User model = new User();

                if (ID == 0 || string.IsNullOrEmpty(ID.ToString().Trim()))
                {
                    ViewBag.model = model;
                    InitCategoryList(0);
                }
                else
                {
                    model = UserBll.GetEntity(x => x.ID == ID);
                    if (model != null)
                    {
                        ViewBag.model = model;
                        InitCategoryList(ID);
                    }
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Rental"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateInsert(User Model, IList<int> RoleID)
        {
            try
            {
                string messageStr = "";
                if (string.IsNullOrEmpty(Model.ID.ToString().Trim()) || Model.ID == 0)
                {
                    if (UserBll.InsertUserRole(Model,RoleID, out messageStr, User_ID.ToString()))
                    {
                        message.Status = true;
                        message.Msg = "添加成功！";
                        rs = Json(message);
                        rs.ContentType = "text/html";
                        return rs;
                    }
                    else
                    {
                        message.Status = false;
                        message.Msg = messageStr;
                        rs = Json(message);
                        rs.ContentType = "text/html";
                        return rs;
                    }
 

                }
                else
                {
                    if (UserBll.UpdateUserRole(Model, RoleID, out messageStr, User_ID.ToString()))
                    {
                        message.Status = true;
                        message.Msg = "修改成功！";
                        rs = Json(message);
                        rs.ContentType = "text/html";
                        return rs;
                    }
                    else
                    {
                        message.Status = false;
                        message.Msg = messageStr;
                        rs = Json(message);
                        rs.ContentType = "text/html";
                        return rs;
                    }
                }

            }
            catch (Exception e)
            {
                message.Status = false;
                message.Msg = "失败！" + e.ToString();
                rs = Json(message);
                rs.ContentType = "text/html";
                return rs;
            }
        }

        #endregion






        #region 显示列表
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="pageSize">分页条数</param>
        /// <param name="Searchtext">查询内容</param>
        /// <param name="Selecte_parameter">查询字段</param>
        /// <returns></returns>
        public ActionResult List(int page = 1, int pageSize = 5, string Searchtext = "", string Selecte_parameter = "")
        {
            try
            {

                //Func<ViewModule, bool> exp1;
                //exp1 = x => x.ID > 0;

                var orderName = "ID";
                var exp = "ID>0  ";
                if (!string.IsNullOrEmpty(Selecte_parameter))
                {

                    var parameter = Selecte_parameter.Split(',');
                    var Count = parameter.Count();
                    orderName = parameter[0];
                    exp = " CONVERT(varchar(100), " + parameter[0] + ", 23)" + " like '%" + Searchtext + "%'";
                    for (int i = 1; i < Count; i++)
                        exp = exp + "or " + " CONVERT(varchar(100), " + parameter[i] + ", 23)" + " like '%" + Searchtext + "%'";
                }

                var totalRecord = UserBll.GetEntitiesCount(exp);
                var totalPage = (totalRecord + pageSize - 1) / pageSize;
                var List = UserBll.GetEntitiesForPaging(page, pageSize, orderName, "asc", exp).ToList();

                ViewBag.List = List;
                ViewBag.totalPage = totalPage;
                return View();
            }
            catch
            {
                return Content("<script>alert('查询数据异常，请吴恶意操作！');window.history.back();</script>");
            }
        }

        #endregion




        #region 用户角色勾选view输出
        public void InitCategoryList(int ID)
        {
            List<UserRole> list_UserRole = new List<UserRole>();

            list_UserRole = UserRoleBll.GetEntities(x => x.User_ID == ID).ToList();//查出用户相关角色

            List<Role> list_Role = RoleBll.GetEntities(x=>x.ID>0).ToList();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < list_Role.Count; i++)
            {
                if (i % 3 == 0)
                {
                    sb.Append("<tr width='100%'>");
                }

                sb.Append("<td>");
                sb.Append("<input type='checkbox' style=' width: 15px; height: 15px; margin-bottom: 5px; border: 1px solid #d0d0d0' name='RoleID' id='RoleID' value='");
                sb.Append(list_Role[i].ID + "'");

                foreach (UserRole UserRole in list_UserRole)
                {
                    if (UserRole.Role_ID == list_Role[i].ID)
                        sb.Append("  checked");
                }
 

                sb.Append("/>");
                sb.Append(list_Role[i].Role_Name + "(" + list_Role[i].Role_Remark + ")" + "</td> ");


                if ((i + 1) % 6 == 0)
                {

                    sb.Append("</tr>");
                }

                if ((i == list_Role.Count - 1))
                {
                    sb.Append("<td colspan =" + (6 - ((i + 1) % 6)) + "></td>");

                    sb.Append("</tr>");
                }

            }

            ViewBag.UserRole = sb.ToString();
        }
        #endregion


        #region 对前端开放的下拉数据接口

        #endregion





    }
}