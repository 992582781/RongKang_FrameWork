using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ModuleController : Controller
    {

        private Message message = new Message();
        private JsonResult rs = null;
        private int User_ID = 0;

        IModuleBll<Module> ModuleBll;
        IViewModuleBll<ViewModule> ViewModuleBll;

        public ModuleController(IModuleBll<Module> ModuleBll, IViewModuleBll<ViewModule> ViewModuleBll) //依赖构造函数进行对象注入 
        {
            this.ModuleBll = ModuleBll; //在构造函数中初始化控制器类的Bll属性
            this.ViewModuleBll = ViewModuleBll;
            User_ID = Cookie_Operate.GetID();
        }



        #region 添加修改删除代码
        public ActionResult Edit(int ID = 0)
        {
            try
            {
                Module model = new Module();

                if (ID == 0 || string.IsNullOrEmpty(ID.ToString().Trim()))
                {
                    ViewBag.model = model;
                }
                else
                {
                    model = ModuleBll.GetEntity(x => x.ID == ID);
                    if (model != null)
                    {
                        ViewBag.model = model;
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
        public ActionResult UpdateInsert(Module Model)
        {

            try
            {
                string messageStr = "";
                if (string.IsNullOrEmpty(Model.ID.ToString().Trim()) || Model.ID == 0)
                {
                    if (ModuleBll.Insert(Model, out messageStr, User_ID.ToString()))
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
                    if (ModuleBll.Update(Model, out messageStr, User_ID.ToString()))
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
        public ActionResult List(int page = 1, int pageSize = 10, string Searchtext = "", string Selecte_parameter = "")
        {
            try
            {

                //Func<ViewModule, bool> exp1;
                //exp1 = x => x.ID > 0;

                var orderName = "Module_Order";
                var exp = "ID>0  ";
                if (!string.IsNullOrEmpty(Selecte_parameter))
                {

                    var parameter = Selecte_parameter.Split(',');
                    var Count = parameter.Count();
                    orderName = "Module_Order";
                    exp = " CONVERT(varchar(100), " + parameter[0] + ", 23)" + " like '%" + Searchtext + "%'";
                    for (int i = 1; i < Count; i++)
                        exp = exp + "or " + " CONVERT(varchar(100), " + parameter[i] + ", 23)" + " like '%" + Searchtext + "%'";
                }

                var totalRecord = ViewModuleBll.GetEntitiesCount(exp);
                var totalPage = (totalRecord + pageSize - 1) / pageSize;
                var List = ViewModuleBll.GetEntitiesForPaging(page, pageSize, orderName, "asc", exp).ToList();

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








        #region 对前端开放的下拉数据接口
        /// <summary>
        /// 获取模块的下拉数据
        /// </summary>
        /// <returns></returns>
        public ActionResult ParentID()
        {
            var Model = ModuleBll.GetEntities(x => x.Module_ParentID == 0).ToList();
            List<SelectData> SelectItems = new List<SelectData>();

            foreach (Module module in Model)
            {
                SelectItems.Add(new SelectData { ID = module.ID.ToString(), Name = module.Module_Name.ToString() + "(" + module.Module_Order + ")" });

                List<Module> list_menu = ModuleBll.GetEntities(x => x.Module_ParentID == module.ID).ToList();

                int count = list_menu.Count;

                for (int i = 0; i < count; i++)
                {
                    if (i != count - 1)
                        SelectItems.Add(new SelectData { ID = list_menu[i].ID.ToString(), Name = "├" + list_menu[i].Module_Name.ToString() + "(" + list_menu[i].Module_Order + ")" });
                    else
                        SelectItems.Add(new SelectData { ID = list_menu[i].ID.ToString(), Name = "└" + list_menu[i].Module_Name.ToString() + "(" + list_menu[i].Module_Order + ")" });

                }
            }
            return Json(SelectItems, JsonRequestBehavior.AllowGet);
        }
        #endregion





    }
}