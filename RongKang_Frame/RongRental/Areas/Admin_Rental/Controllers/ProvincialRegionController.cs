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
    public class ProvincialRegionController : Controller
    {

        private Message message = new Message();
        private JsonResult rs = null;
        private int User_ID = 0;

        IProvincialRegionBll<ProvincialRegion> ProvincialRegionBll;
        IYearBudgetBll<YearBudget> YearBudgetBll;
        IBranchOfficeYearBudgetBll<BranchOfficeYearBudget> BranchOfficeYearBudgetBll;
        IReimbursementRecordBll<ReimbursementRecord> ReimbursementRecordBll;

        public ProvincialRegionController(IProvincialRegionBll<ProvincialRegion> ProvincialRegionBll,
            IYearBudgetBll<YearBudget> YearBudgetBll,
            IBranchOfficeYearBudgetBll<BranchOfficeYearBudget> BranchOfficeYearBudgetBll,
            IReimbursementRecordBll<ReimbursementRecord> ReimbursementRecordBll) //依赖构造函数进行对象注入 
        {
            this.ProvincialRegionBll = ProvincialRegionBll; //在构造函数中初始化控制器类的Bll属性
            this.YearBudgetBll = YearBudgetBll; //在构造函数中初始化控制器类的Bll属性
            this.BranchOfficeYearBudgetBll = BranchOfficeYearBudgetBll; //在构造函数中初始化控制器类的Bll属性
            this.ReimbursementRecordBll = ReimbursementRecordBll; //在构造函数中初始化控制器类的Bll属性

            User_ID = Cookie_Operate.GetID();
        }



        #region 添加修改删除代码
        public ActionResult Edit(int ID = 0)
        {
            try
            {
                ProvincialRegion model = new ProvincialRegion();

                if (ID == 0 || string.IsNullOrEmpty(ID.ToString().Trim()))
                {
                    ViewBag.model = model;
                }
                else
                {
                    model = ProvincialRegionBll.GetEntity(x => x.ID == ID && x.UserID == User_ID);
                    if (model != null)
                    {
                        ViewBag.model = model;
                    }
                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
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
        public ActionResult UpdateInsert(ProvincialRegion Model)
        {

            try
            {
                Model.UserID = User_ID;
                Model.InTime = DateTime.Now;
                string messageStr = "";
                if (string.IsNullOrEmpty(Model.ID.ToString().Trim()) || Model.ID == 0)
                {
                    var ProvincialRegionOld = ProvincialRegionBll.GetEntities(x => x.ProvinceName == Model.ProvinceName.Trim()
                    && x.Leader == Model.Leader).FirstOrDefault();
                    if (ProvincialRegionOld != null)
                    {
                        message.Status = false;
                        message.Msg = "数据已经存在，务重复添加！";
                        rs = Json(message);
                        rs.ContentType = "text/html";
                        return rs;
                    }
                    if (ProvincialRegionBll.Insert(Model, out messageStr, User_ID.ToString()))
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
                    if (ProvincialRegionBll.Update(Model, out messageStr, User_ID.ToString()))
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
                Dal_Log.WriteBaseDal(e.ToString());
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
        public ActionResult List(int page = 1, int pageSize = 20, string Searchtext = "", string Selecte_parameter = "")
        {
            try
            {

                //Func<ViewModule, bool> exp1;
                //exp1 = x => x.ID > 0;

                var orderName = "ID";
                var exp = "ID>0  and UserID=" + User_ID + "";
                if (!string.IsNullOrEmpty(Selecte_parameter))
                {

                    var parameter = Selecte_parameter.Split(',');
                    var Count = parameter.Count();
                    orderName = parameter[0];
                    exp = " CONVERT(varchar(100), " + parameter[0] + ", 23)" + " like '%" + Searchtext + "%'";
                    for (int i = 1; i < Count; i++)
                        exp = exp + "or " + " CONVERT(varchar(100), " + parameter[i] + ", 23)" + " like '%" + Searchtext + "%'";
                }

                var totalRecord = ProvincialRegionBll.GetEntitiesCount(exp);
                var totalPage = (totalRecord + pageSize - 1) / pageSize;
                var List = ProvincialRegionBll.GetEntitiesForPaging(page, pageSize, orderName, "asc", exp).ToList();
                foreach (var provincialRegion in List)
                {
                    var yearBudgets = YearBudgetBll.GetEntities(x => x.ProvincialRegion_ID == provincialRegion.ID &&
                    x.Year == DateTime.Now.Year).FirstOrDefault();
                    provincialRegion.BudgetFunds_1 = String.Format("{0:N2}", yearBudgets?.BudgetFunds);
                    provincialRegion.AvailableBudgetFunds_1 = String.Format("{0:N2}", yearBudgets?.AvailableBudgetFunds);
                    provincialRegion.UsedBudgetFunds_1 = String.Format("{0:N2}", yearBudgets?.UsedBudgetFunds);

                    provincialRegion.ManagementFunds_1 = string.Format("{0:N2}", yearBudgets?.ManagementFunds);
                    provincialRegion.AvailableManagementFunds_1 = string.Format("{0:N2}", yearBudgets?.AvailableManagementFunds);
                    provincialRegion.UsedManagementFunds_1 = string.Format("{0:N2}", yearBudgets?.UsedManagementFunds);

                    var branchOfficeYearBudgetList = BranchOfficeYearBudgetBll.GetEntities(x => x.ProvincialRegion_ID == provincialRegion.ID &&
                     x.Switch_ManageType == "否" &&
                     x.Year == DateTime.Now.Year).GroupBy(g => g.ProvincialRegion_ID).
                     Select(e => new { ProvincialRegion_ID = e.Key, UsedBudgetFunds = e.Sum(q => q.UsedBudgetFunds) });
                    provincialRegion.RealUsedBudgetFunds = string.Format("{0:N2}", branchOfficeYearBudgetList?.FirstOrDefault()?.UsedBudgetFunds);


                    var branchOfficeYearBudgetList2 = BranchOfficeYearBudgetBll.GetEntities(x => x.ProvincialRegion_ID == provincialRegion.ID &&
                    x.Switch_ManageType == "是" &&
                    x.Year == DateTime.Now.Year).GroupBy(g => g.ProvincialRegion_ID).
                    Select(e => new { ProvincialRegion_ID = e.Key, UsedBudgetFunds = e.Sum(q => q.UsedBudgetFunds) });
                    provincialRegion.RealUsedManagementFunds = string.Format("{0:N2}", branchOfficeYearBudgetList2?.FirstOrDefault()?.UsedBudgetFunds);

                    var reimbursementRecordList = ReimbursementRecordBll.GetEntities(x => x.ProvincialRegion_ID == provincialRegion.ID &&
                     x.Year == DateTime.Now.Year).ToList();

                    var FollowUpFundsList = reimbursementRecordList.Where(x => x.Project_ID == 2).GroupBy(g => g.Project_ID).
                    Select(e => new { Project_ID = e.Key, FollowUpFunds = e.Sum(q => q.Funds) });
                    provincialRegion.FollowUpFunds = string.Format("{0:N2}", FollowUpFundsList?.FirstOrDefault()?.FollowUpFunds);
                    provincialRegion.PercentFollowUpFunds = Convert.ToDecimal(FollowUpFundsList?.FirstOrDefault()?.FollowUpFunds 
                        / branchOfficeYearBudgetList?.FirstOrDefault()?.UsedBudgetFunds).ToString("0.00%");
                  

                    var AcademicFundsList = reimbursementRecordList.Where(x => x.Project_ID == 3).GroupBy(g => g.Project_ID).
                    Select(e => new { Project_ID = e.Key, AcademicFunds = e.Sum(q => q.Funds) });
                    provincialRegion.AcademicFunds = string.Format("{0:N2}", AcademicFundsList?.FirstOrDefault()?.AcademicFunds);
                    provincialRegion.PercentAcademicFunds = Convert.ToDecimal(AcademicFundsList?.FirstOrDefault()?.AcademicFunds
                       / branchOfficeYearBudgetList?.FirstOrDefault()?.UsedBudgetFunds).ToString("0.00%");


                    var BusinessFundsList = reimbursementRecordList.Where(x => x.Project_ID == 4).GroupBy(g => g.Project_ID).
                    Select(e => new { Project_ID = e.Key, BusinessFunds = e.Sum(q => q.Funds) });
                    provincialRegion.BusinessFunds = string.Format("{0:N2}", BusinessFundsList?.FirstOrDefault()?.BusinessFunds);
                    provincialRegion.PercentBusinessFunds = Convert.ToDecimal(BusinessFundsList?.FirstOrDefault()?.BusinessFunds
                      / branchOfficeYearBudgetList?.FirstOrDefault()?.UsedBudgetFunds).ToString("0.00%");

                    var InformationFundsList = reimbursementRecordList.Where(x => x.Project_ID == 5).GroupBy(g => g.Project_ID).
                    Select(e => new { Project_ID = e.Key, InformationFunds = e.Sum(q => q.Funds) });
                    provincialRegion.InformationFunds = string.Format("{0:N2}", InformationFundsList?.FirstOrDefault()?.InformationFunds);
                    provincialRegion.PercentInformationFunds = Convert.ToDecimal(InformationFundsList?.FirstOrDefault()?.InformationFunds
                     / branchOfficeYearBudgetList?.FirstOrDefault()?.UsedBudgetFunds).ToString("0.00%");
                }
                ViewBag.List = List;
                ViewBag.totalPage = totalPage;
                return View();
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return Content("<script>alert('查询数据异常，请吴恶意操作！');window.history.back();</script>");
            }
        }

        #endregion



        #region 对前端开放的下拉数据接口
        public ActionResult ID()
        {
            var View_Rental_VehicleS = ProvincialRegionBll.GetEntities(x => x.ID > 0 && x.UserID==User_ID).ToList().Select(x => new SelectData { ID = x.ID.ToString(), Name = x.ProvinceName }).ToList();
            return Json(View_Rental_VehicleS, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 对前端开放的下拉数据接口
        public ActionResult Leader(int ProvincialRegion_ID)
        {
            var View_Rental_VehicleS = ProvincialRegionBll.GetEntities(x => x.ID > 0 && x.UserID == User_ID
            && x.ID==ProvincialRegion_ID
            ).ToList().Select(x => new SelectData { ID = x.Leader.ToString(), Name = x.Leader }).ToList();
            return Json(View_Rental_VehicleS, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}