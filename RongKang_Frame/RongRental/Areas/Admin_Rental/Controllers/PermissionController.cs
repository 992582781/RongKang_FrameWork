using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RongKang_Entity;
using RongKang_IBll;
using RongKang_ViewModel;
using Web_Common;
using Repository;
using System.Data.SqlClient;
using System.Text;
using RongRental.Areas.Admin_Rental.Filters;

namespace RongRental.Areas.Admin_Rental.Controllers
{
    [RongAuthorize]
    public class PermissionController : Controller
    {
        
        private Message message = new Message();
        private JsonResult rs = null;
        private int User_ID = 0;

        IUserBll<User> UserBll;
        IRoleBll<Role> RoleBll;
        IModuleBll<Module> ModuleBll;
        IUserModuleBll<UserModule> UserModuleBll;
        IRoleModuleBll<RoleModule> RoleModuleBll;

        public PermissionController(IUserBll<User> UserBll, IRoleBll<Role> RoleBll,IModuleBll<Module> ModuleBll, IUserModuleBll<UserModule> UserModuleBll, IRoleModuleBll<RoleModule> RoleModuleBll) //依赖构造函数进行对象注入 
        {
            this.UserBll = UserBll; //在构造函数中初始化控制器类的Bll属性
            this.RoleBll = RoleBll;
            this.ModuleBll = ModuleBll;
            this.UserModuleBll = UserModuleBll;
            this.RoleModuleBll = RoleModuleBll;
            User_ID = Cookie_Operate.GetID();
        }

        public ActionResult Index()
        {
            //Role_Remark是1表示是用户 2 表示是角色
            ViewBag.List =RoleBll.GetUserAndRole().ToList();
            return View();
        }


        /// <summary>
        ///  加载用户和角色的所有权限
        /// </summary>
        /// <param name="Id">userid</param>
        /// <param name="Type">1表示用户，2表示是角色</param>
        /// <param name="t">当传入的值是1的时候表示勾选了显示用户角色权限</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult CheckboxList1(int Id = 0, int Type = 1, int Status = 0)
        {
            List<int> List_RoleModule = new List<int>();
            List<UserModule> List_UserModule = new List<UserModule>();

            if (Type != 1) return null;

            //获取用户单独的权限
            List_UserModule = UserModuleBll.GetEntities(x => x.User_ID == Id).ToList()
               .Select(n => new UserModule()
               {
                   Module_ID = n.Module_ID
               }).ToList();


            //获取用户所在角色的权限
            if (Status == 1)
            {
                List_RoleModule = RoleModuleBll.GetUserRoleModule(Id).ToList();
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul id='root'>");
            sb.Append("<li>");

            sb.Append("<label><input type='checkbox' name='' disabled=‘disabled’ value=''/><a style='color:red' href='javascript:;' _fcksavedurl='javascript:;'>权限勾选</a></label>");
            sb.Append("<ul class='two'>");

            List<Module> list_Module = ModuleBll.GetEntities(x => x.Module_ParentID == 0 && x.Switch_OnOff == 1).OrderBy(x => x.Module_Order).ToList();

            foreach (Module p in list_Module)
            {
                sb.Append("<li>");

                sb.Append("<label><input type='checkbox' name='ModuleId' value='");
                sb.Append(p.ID + "'");
                foreach (UserModule usermodule in List_UserModule)
                {
                    if (usermodule.Module_ID == p.ID)
                        sb.Append("  checked");
                }

                if (List_RoleModule != null && List_RoleModule.Count() > 0)
                {
                    foreach (int rolemodule in List_RoleModule)
                    {
                        if (rolemodule == p.ID)
                        {
                            sb.Append("  checked");
                            sb.Append("  disabled=true");
                            //break;
                        }

                    }
                }

                sb.Append(" /><a href='javascript:;' _fcksavedurl='javascript:;'>" + p.Module_Name + "</a></label>");


                List<Module> list_Module1 = ModuleBll.GetEntities(x => x.Module_ParentID == p.ID && x.Switch_OnOff == 1).OrderBy(x => x.Module_Order).ToList();
                if (list_Module1 != null && list_Module1.Count() > 0)
                {
                    sb.Append("<ul class='two'>");
                }
                foreach (Module p1 in list_Module1)
                {
                    sb.Append("<li>");

                    sb.Append("<label><input type='checkbox' name='ModuleId' value='");
                    sb.Append(p1.ID + "'");
                    foreach (UserModule usermodule in List_UserModule)
                    {
                        if (usermodule.Module_ID == p1.ID)
                            sb.Append("  checked");
                    }

                    if (List_RoleModule != null && List_RoleModule.Count() > 0)
                    {
                        foreach (int rolemodule in List_RoleModule)
                        {
                            if (rolemodule == p1.ID)
                            {
                                sb.Append("  checked");
                                sb.Append("  disabled=true");
                            }
                        }
                    }


                    sb.Append(" /><a href='javascript:;' _fcksavedurl='javascript:;'>" + p1.Module_Name + "</a></label>");



                    List<Module> list_Module2 = ModuleBll.GetEntities(x => x.Module_ParentID == p1.ID && x.Switch_OnOff == 1).OrderBy(x => x.Module_Order).ToList();
                    if (list_Module2 != null && list_Module2.Count() > 0)
                    {
                        sb.Append("<ul class='two'>");
                    }

                    foreach (Module p2 in list_Module2)
                    {
                        sb.Append("<li><label><input type='checkbox' name='ModuleId' value='");
                        sb.Append(p2.ID + "'");

                        foreach (UserModule usermodul in List_UserModule)
                        {
                            if (usermodul.Module_ID == p2.ID)
                                sb.Append("  checked");

                        }

                        if (List_RoleModule != null && List_RoleModule.Count() > 0)
                        {
                            foreach (int rolemodule in List_RoleModule)
                            {
                                if (rolemodule == p2.ID)
                                {
                                    sb.Append("  checked");
                                    sb.Append("  disabled");
                                }

                            }
                        }

                        sb.Append(" /><a href='javascript:;' _fcksavedurl='javascript:;'>" + p2.Module_Name + "</a></label></li>");

                    }

                    if (list_Module2 != null && list_Module2.Count() > 0)
                    {
                        sb.Append("</ul>");
                    }
                    sb.Append("</li>");

                }
                if (list_Module1 != null && list_Module1.Count() > 0)
                {
                    sb.Append("</ul>");
                }
                sb.Append("</li>");
            }



            sb.Append("</ul>");

            sb.Append("</li>");
            sb.Append("</ul>");

            var Message = new List<string>();
            Message.Add(sb.ToString());

            return PartialView("CheckboxList1", Message);
        }



        /// <summary>
        /// 加载角色的所有权限
        /// </summary>
        /// <param name="Id">roleid</param>
        /// <param name="Type">1表示用户，2表示是角色</param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult CheckboxList2(int Id = 0, int Type = 2, int Status = 0)
        {
            if (Type != 2) return null;
            List<RoleModule> List_RoleModule = RoleModuleBll.GetEntities(x => x.Role_ID == Id).ToList()
                .Select(n => new RoleModule()
                {
                    Module_ID = n.Module_ID
                }).ToList();


            StringBuilder sb = new StringBuilder();
            sb.Append("<ul id='root'>");
            sb.Append("<li>");

            sb.Append("<label><input type='checkbox' name='' disabled=‘disabled’ value=''/><a style='color:red' href='javascript:;' _fcksavedurl='javascript:;'>权限勾选</a></label>");
            sb.Append("<ul class='two'>");

            List<Module> list_Module = ModuleBll.GetEntities(x => x.Module_ParentID == 0 && x.Switch_OnOff==1).OrderBy(x => x.Module_Order).ToList();

            foreach (Module p in list_Module)
            {
                sb.Append("<li>");

                sb.Append("<label><input type='checkbox' name='ModuleId' value='");
                sb.Append(p.ID + "'");
                foreach (RoleModule rolemodule in List_RoleModule)
                {
                    if (rolemodule.Module_ID == p.ID)
                        sb.Append("  checked");
                }
                sb.Append(" /><a href='javascript:;' _fcksavedurl='javascript:;'>" + p.Module_Name + "</a></label>");



                List<Module> list_Module1 = ModuleBll.GetEntities(x => x.Module_ParentID == p.ID && x.Switch_OnOff == 1).OrderBy(x => x.Module_Order).ToList();

                if (list_Module1 != null && list_Module1.Count() > 0)
                {
                    sb.Append("<ul class='two'>");
                }

                foreach (Module p1 in list_Module1)
                {
                    sb.Append("<li>");

                    sb.Append("<label><input type='checkbox' name='ModuleId' value='");
                    sb.Append(p1.ID + "'");
                    foreach (RoleModule rolemodule in List_RoleModule)
                    {
                        if (rolemodule.Module_ID == p1.ID)
                            sb.Append("  checked");
                    }
                    sb.Append(" /><a href='javascript:;' _fcksavedurl='javascript:;'>" + p1.Module_Name + "</a></label>");


                    List<Module> list_Module2 = ModuleBll.GetEntities(x => x.Module_ParentID == p1.ID && x.Switch_OnOff == 1).OrderBy(x => x.Module_Order).ToList();

                    if (list_Module2 != null && list_Module2.Count() > 0)
                    {
                        sb.Append("<ul class='two'>");
                    }

                    foreach (Module p2 in list_Module2)
                    {
                        sb.Append("<li><label><input type='checkbox' name='ModuleId' value='");
                        sb.Append(p2.ID + "'");
                        foreach (RoleModule rolemodule in List_RoleModule)
                        {
                            if (rolemodule.Module_ID == p2.ID)
                                sb.Append("  checked");
                        }
                        sb.Append(" /><a href='javascript:;' _fcksavedurl='javascript:;'>" + p2.Module_Name + "</a></label></li>");

                    }

                    if (list_Module2 != null && list_Module2.Count() > 0)
                    {
                        sb.Append("</ul>");
                    }
                    sb.Append("</li>");

                }
                if (list_Module1 != null && list_Module1.Count() > 0)
                {
                    sb.Append("</ul>");
                }
                sb.Append("</li>");
            }



            sb.Append("</ul>");

            sb.Append("</li>");
            sb.Append("</ul>");

            var Message = new List<string>();
            Message.Add(sb.ToString());

            return PartialView("CheckboxList2", Message);
        }


        /// <summary>
        /// 加载角色所有用户
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Type"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult LoadUserList(int Id = 0, int Type = 0, int Status = 0)
        {

            ViewBag.List = UserBll.GetRoleUser(Id).ToList();
            return PartialView("LoadUserList");
        }


        /// <summary>
        /// 加载用户所有角色
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Type"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult LoadUserRoleList(int Id = 0, int Type = 0, int Status = 0)
        {

            ViewBag.List = RoleBll.GetUserRole(Id).ToList();
            return PartialView("LoadUserRoleList");
        }





        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="Modules"></param>
        /// <param name="Role_ID">角色的id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(IList<int> Modules, int RoleId = 0)
        {

            try
            {
                if (RoleModuleBll.InsertRoleModule(Modules, RoleId))
                {
                    message.Status = true;
                    message.Msg = "保存成功！";
                    rs = Json(message);
                    rs.ContentType = "text/html";
                    return rs;
                }
                else
                {
                    message.Status = false;
                    message.Msg = "保存失败！";
                    rs = Json(message);
                    rs.ContentType = "text/html";
                    return rs;
                }

            }
            catch (Exception e)
            {
                message.Status = false;
                message.Msg = e.ToString();
                message.Location_Url = "/Authority/Index";
                rs = Json(message);
                rs.ContentType = "text/html";
                return rs;
            }

        }


        /// <summary>
        /// 保存用户权限
        /// </summary>
        /// <param name="Modules">勾选的数据</param>
        /// <param name="RoleId">用户id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit1(IList<int> Modules, int RoleId = 0)
        {
            try
            {
                if (UserModuleBll.InsertUserModule(Modules, RoleId))
                {
                    message.Status = true;
                    message.Msg = "保存成功！";
                    rs = Json(message);
                    rs.ContentType = "text/html";
                    return rs;
                }
                else
                {
                    message.Status = false;
                    message.Msg = "保存失败！";
                    rs = Json(message);
                    rs.ContentType = "text/html";
                    return rs;
                }

            }
            catch (Exception e)
            {
                message.Status = false;
                message.Msg = e.ToString();
                message.Location_Url = "/Authority/Index";
                rs = Json(message);
                rs.ContentType = "text/html";
                return rs;
            }

        }

    }
}