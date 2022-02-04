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
    public class ReimbursementRecordController : Controller
    {

        private Message message = new Message();
        private JsonResult rs = null;
        private int User_ID = 0;

        IReimbursementRecordBll<ReimbursementRecord> ReimbursementRecordBll;
        IBranchOfficeYearBudgetBll<BranchOfficeYearBudget> BranchOfficeYearBudgetBll;
        IBranchOfficeBll<BranchOffice> BranchOfficeBll;
        IYearBudgetBll<YearBudget> YearBudgetBll;
        IProvincialRegionBll<ProvincialRegion> ProvincialRegionBll;
        IProductBll<Product> ProductBll;
        IProjectBll<Project> ProjectBll;

        public ReimbursementRecordController(IReimbursementRecordBll<ReimbursementRecord> ReimbursementRecordBll,
             IBranchOfficeYearBudgetBll<BranchOfficeYearBudget> BranchOfficeYearBudgetBll,
             IBranchOfficeBll<BranchOffice> BranchOfficeBll,
             IYearBudgetBll<YearBudget> YearBudgetBll,
             IProvincialRegionBll<ProvincialRegion> ProvincialRegionBll,
             IProductBll<Product> ProductBll,
             IProjectBll<Project> ProjectBll
            ) //依赖构造函数进行对象注入 
        {
            this.ReimbursementRecordBll = ReimbursementRecordBll; //在构造函数中初始化控制器类的Bll属性
            this.BranchOfficeYearBudgetBll = BranchOfficeYearBudgetBll; //在构造函数中初始化控制器类的Bll属性
            this.BranchOfficeBll = BranchOfficeBll; //在构造函数中初始化控制器类的Bll属性
            this.YearBudgetBll = YearBudgetBll; //在构造函数中初始化控制器类的Bll属性
            this.ProvincialRegionBll = ProvincialRegionBll; //在构造函数中初始化控制器类的Bll属性
            this.ProductBll = ProductBll; //在构造函数中初始化控制器类的Bll属性
            this.ProjectBll = ProjectBll; //在构造函数中初始化控制器类的Bll属性
            User_ID = Cookie_Operate.GetID();
        }



        #region 添加修改删除代码
        public ActionResult Edit(int ID = 0)
        {
            try
            {
                ReimbursementRecord model = new ReimbursementRecord();

                if (ID == 0 || string.IsNullOrEmpty(ID.ToString().Trim()))
                {
                    //model.ReimbursementDate
                    ViewBag.model = model;
                }
                else
                {
                    model = ReimbursementRecordBll.GetEntity(x => x.ID == ID && x.UserID == User_ID);
                    if (model != null)
                    {
                        model.Funds_1 = model.Funds.ToString();
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
        public ActionResult UpdateInsert(ReimbursementRecord  reimbursementRecord)
        {

            try
            {
                reimbursementRecord.UserID = User_ID;
                reimbursementRecord.InTime = DateTime.Now;
                reimbursementRecord.Year=reimbursementRecord.ReimbursementDate.Year;
                reimbursementRecord.Month = reimbursementRecord.ReimbursementDate.Month;
                reimbursementRecord.Funds = Convert.ToDecimal(reimbursementRecord.Funds_1.Replace(",", ""));

                

                string messageStr = "";
                if (string.IsNullOrEmpty(reimbursementRecord.ID.ToString().Trim()) || reimbursementRecord.ID == 0)
                {
                    var branchOfficeYearBudget = BranchOfficeYearBudgetBll.GetEntities(x => x.BranchOffice_ID == reimbursementRecord.BranchOffice_ID
                              && x.Year == reimbursementRecord.Year).FirstOrDefault();
                    branchOfficeYearBudget.UsedBudgetFunds = branchOfficeYearBudget.UsedBudgetFunds + reimbursementRecord.Funds;
                    branchOfficeYearBudget.AvailableBudgetFunds = branchOfficeYearBudget.BudgetFunds - branchOfficeYearBudget.UsedBudgetFunds;

                    if (ReimbursementRecordBll.Insert(reimbursementRecord, out messageStr, User_ID.ToString()))
                    {
                        BranchOfficeYearBudgetBll.Update(branchOfficeYearBudget, out messageStr, User_ID.ToString());
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
                    var reimbursementRecordOld = ReimbursementRecordBll.GetEntities(x => x.ID == reimbursementRecord.ID).FirstOrDefault();
                    var branchOfficeYearBudget = BranchOfficeYearBudgetBll.GetEntities(x => x.BranchOffice_ID == reimbursementRecordOld.BranchOffice_ID
                              && x.Year == reimbursementRecordOld.Year).FirstOrDefault();

                    branchOfficeYearBudget.UsedBudgetFunds = branchOfficeYearBudget.UsedBudgetFunds - (reimbursementRecordOld.Funds - reimbursementRecord.Funds);
                    branchOfficeYearBudget.AvailableBudgetFunds = branchOfficeYearBudget.BudgetFunds - branchOfficeYearBudget.UsedBudgetFunds;
                    reimbursementRecordOld.Funds = reimbursementRecord.Funds;

                    if (ReimbursementRecordBll.Update(reimbursementRecordOld, out messageStr, User_ID.ToString()))
                    {
                        BranchOfficeYearBudgetBll.Update(branchOfficeYearBudget, out messageStr, User_ID.ToString());
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
        public ActionResult List(int page = 1, int pageSize = 20)
        {
            try
            {

                //Func<ViewModule, bool> exp1;
                //exp1 = x => x.ID > 0;

                var orderName = "Year";
                var exp = "ID>0  and UserID=" + User_ID + "";
                Dictionary<string, FieldNameAttribute> Dic = CustomAttributeHelper.GetpropertyView<ReimbursementRecord>();//修改model
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

                var totalRecord = ReimbursementRecordBll.GetEntitiesCount(exp);
                var totalPage = (totalRecord + pageSize - 1) / pageSize;
                var List = ReimbursementRecordBll.GetEntitiesForPaging(page, pageSize, orderName, "desc", exp).ToList();

               
                var ProvincialRegionList = ProvincialRegionBll.GetEntities(x => x.ID > 0).ToList();
                var BranchOfficeList = BranchOfficeBll.GetEntities(x => x.ID > 0).ToList();
                var ProjectList= ProjectBll.GetEntities(x => x.ID > 0).ToList();
                var ProductList = ProductBll.GetEntities(x => x.ID > 0).ToList();

                foreach (var ReimbursementRecor in List)
                {
                    ReimbursementRecor.BranchOfficeName = BranchOfficeList?.Where(x => x.ID == ReimbursementRecor.BranchOffice_ID)?.FirstOrDefault()?.BranchOfficeName;
                    ReimbursementRecor.ProvinceName = ProvincialRegionList?.Where(x => x.ID == ReimbursementRecor.ProvincialRegion_ID)?.FirstOrDefault()?.ProvinceName;
                    ReimbursementRecor.ProductName = ProductList?.Where(x => x.ID == ReimbursementRecor.Product_ID)?.FirstOrDefault()?.ProductName;

                    ReimbursementRecor.ProjectName = ProjectList?.Where(x => x.ID == ReimbursementRecor.Project_ID)?.FirstOrDefault().ProjectName;
                    ReimbursementRecor.Funds_1 = String.Format("{0:N2}", ReimbursementRecor.Funds);
                    ReimbursementRecor.ReimbursementDateStr = PageValidate.DateFormatReturnString(ReimbursementRecor.ReimbursementDate.ToString(), 3);
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

    }
}