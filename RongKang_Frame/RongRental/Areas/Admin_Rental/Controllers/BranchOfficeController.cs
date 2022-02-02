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
    public class BranchOfficeController : Controller
    {

        private Message message = new Message();
        private JsonResult rs = null;
        private int User_ID = 0;

        IBranchOfficeBll<BranchOffice> BranchOfficeBll;
        IProvincialRegionBll<ProvincialRegion> ProvincialRegionBll;
        IBranchOfficeYearBudgetBll<BranchOfficeYearBudget> BranchOfficeYearBudgetBll;
        public BranchOfficeController(IBranchOfficeYearBudgetBll<BranchOfficeYearBudget> BranchOfficeYearBudgetBll,
            IBranchOfficeBll<BranchOffice> BranchOfficeBll,
            IProvincialRegionBll<ProvincialRegion> ProvincialRegionBll) //依赖构造函数进行对象注入 
        {
            this.BranchOfficeYearBudgetBll = BranchOfficeYearBudgetBll; //在构造函数中初始化控制器类的Bll属性
            this.BranchOfficeBll = BranchOfficeBll; //在构造函数中初始化控制器类的Bll属性
            this.ProvincialRegionBll = ProvincialRegionBll; //在构造函数中初始化控制器类的Bll属性
            User_ID = Cookie_Operate.GetID();
        }



        #region 添加修改删除代码
        public ActionResult Edit(int ID = 0)
        {
            try
            {
                BranchOffice model = new BranchOffice();

                if (ID == 0 || string.IsNullOrEmpty(ID.ToString().Trim()))
                {
                    ViewBag.model = model;
                }
                else
                {
                    model = BranchOfficeBll.GetEntity(x => x.ID == ID && x.UserID == User_ID);
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
        public ActionResult UpdateInsert(BranchOffice  branchOffice)
        {

            try
            {
                branchOffice.UserID = User_ID;
                branchOffice.InTime = DateTime.Now;
  
                string messageStr = "";
                if (string.IsNullOrEmpty(branchOffice.ID.ToString().Trim()) || branchOffice.ID == 0)
                {
                    var branchOfficeOld = BranchOfficeBll.GetEntities(x => x.ProvincialRegion_ID == branchOffice.ProvincialRegion_ID 
                    && x.BranchOfficeName == branchOffice.BranchOfficeName).FirstOrDefault();
                    if (branchOfficeOld != null)
                    {
                        message.Status = false;
                        message.Msg = "数据已经存在，务重复添加！";
                        rs = Json(message);
                        rs.ContentType = "text/html";
                        return rs;
                    }
                    if (BranchOfficeBll.Insert(branchOffice, out messageStr, User_ID.ToString()))
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
                    if (BranchOfficeBll.Update(branchOffice, out messageStr, User_ID.ToString()))
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
        public ActionResult List(int page = 1, int pageSize = 20)
        {
            try
            {

                //Func<ViewModule, bool> exp1;
                //exp1 = x => x.ID > 0;

                var orderName = "ID";
                var exp = "ID>0  and UserID=" + User_ID + "";
                Dictionary<string, FieldNameAttribute> Dic = CustomAttributeHelper.GetpropertyView<BranchOffice>();//修改model
                foreach (var dic in Dic)
                {
                    if (dic.Value.View_Flag != 0)
                    {
                        if (dic.Value.Control_Type.ToString() == Control_Type.SelectText.ToString())
                        {
                            if (!string.IsNullOrWhiteSpace(Request.QueryString[dic.Key]))
                                exp = exp + "and  " + dic.Key + "= " + Request.QueryString[dic.Key].ToString() + "";
                        }
                        else if (dic.Value.Control_Type.ToString() == Control_Type.Text.ToString())
                        {
                            if (!string.IsNullOrWhiteSpace(Request.QueryString[dic.Key]))
                                exp = exp + "and   CONVERT(varchar(100), " + dic.Key + ", 23)" + " like '%" + Request.QueryString[dic.Key].ToString() + "%'";
                        }
                    }
                }

                var totalRecord = BranchOfficeBll.GetEntitiesCount(exp);
                var totalPage = (totalRecord + pageSize - 1) / pageSize;
                var List = BranchOfficeBll.GetEntitiesForPaging(page, pageSize, orderName, "desc", exp).ToList();

                var ProvincialRegionList = ProvincialRegionBll.GetEntities(x => x.ID > 0).ToList();
                var BranchOfficeYearBudgetList = BranchOfficeYearBudgetBll.GetEntities(x => x.ID > 0).ToList();

                foreach (var BranchOffice in List)
                {
                    BranchOffice.ProvinceName = ProvincialRegionList?.Where(x => x.ID == BranchOffice.ProvincialRegion_ID)?.FirstOrDefault()?.ProvinceName;
                    var BranchOfficeYearBudget = BranchOfficeYearBudgetList.Where(x => x.BranchOffice_ID == BranchOffice.ID)?.FirstOrDefault();
                    BranchOffice.Switch_ManageType = BranchOfficeYearBudget?.Switch_ManageType;
                    BranchOffice.BudgetFunds_1 = String.Format("{0:N2}", BranchOfficeYearBudget?.BudgetFunds);
                    BranchOffice.AvailableBudgetFunds_1 = String.Format("{0:N2}", BranchOfficeYearBudget?.AvailableBudgetFunds);
                    BranchOffice.UsedBudgetFunds_1 = String.Format("{0:N2}", BranchOfficeYearBudget?.UsedBudgetFunds);
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


        #region 对前端开放的下拉数据接口
        public ActionResult ID(int ProvincialRegion_ID)
        {
            var View_Rental_VehicleS = BranchOfficeBll.GetEntities(x => x.ID > 0 && x.UserID==User_ID && x.ProvincialRegion_ID==ProvincialRegion_ID
            ).ToList().Select(x => new SelectData { ID = x.ID.ToString(), Name = x.BranchOfficeName }).ToList();
            return Json(View_Rental_VehicleS, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}