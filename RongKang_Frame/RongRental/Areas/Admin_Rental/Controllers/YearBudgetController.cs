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
    public class YearBudgetController : Controller
    {

        private Message message = new Message();
        private JsonResult rs = null;
        private int User_ID = 0;

        IYearBudgetBll<YearBudget> YearBudgetBll;
        IProvincialRegionBll<ProvincialRegion> ProvincialRegionBll;


        public YearBudgetController(IYearBudgetBll<YearBudget> YearBudgetBll, IProvincialRegionBll<ProvincialRegion> ProvincialRegionBll) //依赖构造函数进行对象注入 
        {
            this.YearBudgetBll = YearBudgetBll; //在构造函数中初始化控制器类的Bll属性
            this.ProvincialRegionBll = ProvincialRegionBll; //在构造函数中初始化控制器类的Bll属性
            User_ID = Cookie_Operate.GetID();
        }



        #region 添加修改删除代码
        public ActionResult Edit(int ID = 0)
        {
            try
            {
                YearBudget model = new YearBudget();

                if (ID == 0 || string.IsNullOrEmpty(ID.ToString().Trim()))
                {
                    ViewBag.model = model;
                }
                else
                {
                    model = YearBudgetBll.GetEntity(x => x.ID == ID);
                    if (model != null)
                    {
                        model.BudgetFunds_1 = model.BudgetFunds.ToString();
                        model.AvailableBudgetFunds_1 = model.AvailableBudgetFunds.ToString();
                        model.UsedBudgetFunds_1 = model.UsedBudgetFunds.ToString();

                        model.ManagementFunds_1 = model.ManagementFunds.ToString();
                        model.AvailableManagementFunds_1 = model.AvailableManagementFunds.ToString();
                        model.UsedManagementFunds_1 = model.UsedManagementFunds.ToString();

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
        public ActionResult UpdateInsert(YearBudget Model)
        {

            try
            {
                Model.UserID = User_ID;
                Model.InTime = DateTime.Now;
                string messageStr = "";
                if (string.IsNullOrEmpty(Model.ID.ToString().Trim()) || Model.ID == 0)
                {
                    if (YearBudgetBll.Insert(Model, out messageStr, User_ID.ToString()))
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
                    if (YearBudgetBll.Update(Model, out messageStr, User_ID.ToString()))
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

                var totalRecord = YearBudgetBll.GetEntitiesCount(exp);
                var totalPage = (totalRecord + pageSize - 1) / pageSize;
                var List = YearBudgetBll.GetEntitiesForPaging(page, pageSize, orderName, "asc", exp).ToList();

                var ProvincialRegionList = ProvincialRegionBll.GetEntities(x => x.ID > 0).ToList();

                foreach (var YearBudget in List)
                {
                    YearBudget.ProvinceName = ProvincialRegionList?.Where(x => x.ID == YearBudget.ProvincialRegion_ID)?.FirstOrDefault()?.ProvinceName;
                    YearBudget.BudgetFunds_1 = String.Format("{0:N2}", YearBudget.BudgetFunds);
                    YearBudget.AvailableBudgetFunds_1 = String.Format("{0:N2}", YearBudget.AvailableBudgetFunds);
                    YearBudget.UsedBudgetFunds_1 = String.Format("{0:N2}", YearBudget.UsedBudgetFunds);

                    YearBudget.ManagementFunds_1 = string.Format("{0:N2}", YearBudget.ManagementFunds);
                    YearBudget.AvailableManagementFunds_1 = string.Format("{0:N2}", YearBudget.AvailableManagementFunds);
                    YearBudget.UsedManagementFunds_1 = string.Format("{0:N2}", YearBudget.UsedManagementFunds);
                }

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





    }
}