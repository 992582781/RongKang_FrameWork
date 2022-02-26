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
    public class BranchOfficeYearBudgetController : Controller
    {

        private Message message = new Message();
        private JsonResult rs = null;
        private int User_ID = 0;

        IBranchOfficeYearBudgetBll<BranchOfficeYearBudget> BranchOfficeYearBudgetBll;
        IBranchOfficeBll<BranchOffice> BranchOfficeBll;
        IYearBudgetBll<YearBudget> YearBudgetBll;
        IProvincialRegionBll<ProvincialRegion> ProvincialRegionBll;
        IReimbursementRecordBll<ReimbursementRecord> ReimbursementRecordBll;

        public BranchOfficeYearBudgetController(IBranchOfficeYearBudgetBll<BranchOfficeYearBudget> BranchOfficeYearBudgetBll,
             IBranchOfficeBll<BranchOffice> BranchOfficeBll,
             IYearBudgetBll<YearBudget> YearBudgetBll,
             IReimbursementRecordBll<ReimbursementRecord> ReimbursementRecordBll,
             IProvincialRegionBll<ProvincialRegion> ProvincialRegionBll) //�������캯�����ж���ע�� 
        {
            this.BranchOfficeYearBudgetBll = BranchOfficeYearBudgetBll; //�ڹ��캯���г�ʼ�����������Bll����
            this.BranchOfficeBll = BranchOfficeBll; //�ڹ��캯���г�ʼ�����������Bll����
            this.YearBudgetBll = YearBudgetBll; //�ڹ��캯���г�ʼ�����������Bll����
            this.ProvincialRegionBll = ProvincialRegionBll; //�ڹ��캯���г�ʼ�����������Bll����
            this.ReimbursementRecordBll = ReimbursementRecordBll; //�ڹ��캯���г�ʼ�����������Bll����
            User_ID = Cookie_Operate.GetID();
        }



        #region ����޸�ɾ������
        public ActionResult Edit(int ID = 0)
        {
            try
            {
                BranchOfficeYearBudget model = new BranchOfficeYearBudget();

                if (ID == 0 || string.IsNullOrEmpty(ID.ToString().Trim()))
                {
                    ViewBag.model = model;
                }
                else
                {
                    model = BranchOfficeYearBudgetBll.GetEntity(x => x.ID == ID && x.UserID == User_ID);
                    if (model != null)
                    {
                        model.BudgetFunds_1 = model.BudgetFunds.ToString();
                        model.AvailableBudgetFunds_1 = model.AvailableBudgetFunds.ToString();
                        model.UsedBudgetFunds_1 = model.UsedBudgetFunds.ToString();

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
        public ActionResult UpdateInsert(BranchOfficeYearBudget branchOfficeYearBudget)
        {

            try
            {
                branchOfficeYearBudget.UserID = User_ID;
                branchOfficeYearBudget.InTime = DateTime.Now;

                branchOfficeYearBudget.BudgetFunds = Convert.ToDecimal(branchOfficeYearBudget.BudgetFunds_1?.Replace(",", ""));
                branchOfficeYearBudget.AvailableBudgetFunds = Convert.ToDecimal(branchOfficeYearBudget?.AvailableBudgetFunds_1?.Replace(",", ""));
                branchOfficeYearBudget.UsedBudgetFunds = Convert.ToDecimal(branchOfficeYearBudget?.UsedBudgetFunds_1?.Replace(",", ""));


                string messageStr = "";
                if (string.IsNullOrEmpty(branchOfficeYearBudget.ID.ToString().Trim()) || branchOfficeYearBudget.ID == 0)
                {
                    var branchOfficeYearBudgetOld = BranchOfficeYearBudgetBll.GetEntities(x => x.BranchOffice_ID == branchOfficeYearBudget.BranchOffice_ID
                    && x.Year == branchOfficeYearBudget.Year).FirstOrDefault();

                    var BranchOfficeList = BranchOfficeBll.GetEntities(x => x.ID > 0).ToList();
                    branchOfficeYearBudget.ProvincialRegion_ID = (BranchOfficeList.Where(x => x.ID == branchOfficeYearBudget.BranchOffice_ID).FirstOrDefault().ProvincialRegion_ID);

                    if (branchOfficeYearBudgetOld != null)
                    {
                        message.Status = false;
                        message.Msg = "�����Ѿ����ڣ����ظ���ӣ�";
                        rs = Json(message);
                        rs.ContentType = "text/html";
                        return rs;
                    }

                    var YearBudgetOld = YearBudgetBll.GetEntities(x => x.ProvincialRegion_ID == branchOfficeYearBudget.ProvincialRegion_ID
                     && x.Year == branchOfficeYearBudget.Year).FirstOrDefault();

                    if (branchOfficeYearBudget.Switch_ManageType == "��")
                    {
                        YearBudgetOld.AvailableBudgetFunds = YearBudgetOld.AvailableBudgetFunds - branchOfficeYearBudget.BudgetFunds;
                        YearBudgetOld.UsedBudgetFunds = YearBudgetOld.UsedBudgetFunds + branchOfficeYearBudget.BudgetFunds;
                        branchOfficeYearBudget.AvailableBudgetFunds = branchOfficeYearBudget.BudgetFunds;
                    }
                    else
                    {
                        YearBudgetOld.AvailableManagementFunds = YearBudgetOld.AvailableManagementFunds - branchOfficeYearBudget.BudgetFunds;
                        YearBudgetOld.UsedManagementFunds = YearBudgetOld.UsedManagementFunds + branchOfficeYearBudget.BudgetFunds;
                        branchOfficeYearBudget.AvailableBudgetFunds = branchOfficeYearBudget.BudgetFunds;
                    }


                    if (BranchOfficeYearBudgetBll.Insert(branchOfficeYearBudget, out messageStr, User_ID.ToString()))
                    {
                        YearBudgetBll.Update(YearBudgetOld, out messageStr, User_ID.ToString());
                        message.Status = true;
                        message.Msg = "��ӳɹ���";
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

                    var BranchOfficeYearBudgetOld = BranchOfficeYearBudgetBll.GetEntities(x => x.ID == branchOfficeYearBudget.ID
                    && x.Year == branchOfficeYearBudget.Year).FirstOrDefault();

                    var YearBudgetOld = YearBudgetBll.GetEntities(x => x.ProvincialRegion_ID == branchOfficeYearBudget.ProvincialRegion_ID
                     && x.Year == branchOfficeYearBudget.Year).FirstOrDefault();
                    if (branchOfficeYearBudget.Switch_ManageType == "��")
                    {
                        YearBudgetOld.AvailableBudgetFunds = YearBudgetOld.AvailableBudgetFunds +
                        (BranchOfficeYearBudgetOld.BudgetFunds - branchOfficeYearBudget.BudgetFunds);

                        YearBudgetOld.UsedBudgetFunds = YearBudgetOld.UsedBudgetFunds -
                            (BranchOfficeYearBudgetOld.BudgetFunds - branchOfficeYearBudget.BudgetFunds);

                        branchOfficeYearBudget.AvailableBudgetFunds = BranchOfficeYearBudgetOld.AvailableBudgetFunds
                            - (BranchOfficeYearBudgetOld.BudgetFunds - branchOfficeYearBudget.BudgetFunds);
                    }
                    else
                    {
                        YearBudgetOld.AvailableManagementFunds = YearBudgetOld.AvailableManagementFunds +
                                                (BranchOfficeYearBudgetOld.BudgetFunds - branchOfficeYearBudget.BudgetFunds);

                        YearBudgetOld.UsedManagementFunds = YearBudgetOld.UsedManagementFunds -
                            (BranchOfficeYearBudgetOld.BudgetFunds - branchOfficeYearBudget.BudgetFunds);

                        branchOfficeYearBudget.AvailableBudgetFunds = BranchOfficeYearBudgetOld.AvailableBudgetFunds
                            - (BranchOfficeYearBudgetOld.BudgetFunds - branchOfficeYearBudget.BudgetFunds);
                    }

                    if (BranchOfficeYearBudgetBll.Update(branchOfficeYearBudget, out messageStr, User_ID.ToString()))
                    {
                        YearBudgetBll.Update(YearBudgetOld, out messageStr, User_ID.ToString());
                        message.Status = true;
                        message.Msg = "�޸ĳɹ���";
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
                message.Msg = "ʧ�ܣ�" + e.ToString();
                rs = Json(message);
                rs.ContentType = "text/html";
                return rs;
            }
        }

        #endregion






        #region ��ʾ�б�
        /// </summary>
        /// <param name="page">��ǰҳ��</param>
        /// <param name="pageSize">��ҳ����</param>
        /// <param name="Searchtext">��ѯ����</param>
        /// <param name="Selecte_parameter">��ѯ�ֶ�</param>
        /// <returns></returns>
        public ActionResult List(int page = 1, int pageSize = 20)
        {
            try
            {

                //Func<ViewModule, bool> exp1;
                //exp1 = x => x.ID > 0;

                var orderName = "Year";
                var exp = "ID>0  and UserID=" + User_ID + "";
                Dictionary<string, FieldNameAttribute> Dic = CustomAttributeHelper.GetpropertyView<BranchOfficeYearBudget>();//�޸�model
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
                                exp = exp + " and   CONVERT(varchar(100), " + dic.Key + ", 23)" + " like '%" + Request.QueryString[dic.Key].ToString() + "%'";
                        }
                    }
                }

                var totalRecord = BranchOfficeYearBudgetBll.GetEntitiesCount(exp);
                var totalPage = (totalRecord + pageSize - 1) / pageSize;
                var List = BranchOfficeYearBudgetBll.GetEntitiesForPaging(page, pageSize, orderName, "desc", exp).ToList();

                var BranchOfficeList = BranchOfficeBll.GetEntities(x => x.ID > 0).ToList();
                var ProvincialRegionList = ProvincialRegionBll.GetEntities(x => x.ID > 0).ToList();

                foreach (var BranchOfficeYearBudget in List)
                {
                    BranchOfficeYearBudget.BranchOfficeName = BranchOfficeList?.Where(x => x.ID == BranchOfficeYearBudget.BranchOffice_ID)?.FirstOrDefault()?.BranchOfficeName;
                    BranchOfficeYearBudget.ProvinceName = ProvincialRegionList?.Where(x => x.ID == BranchOfficeYearBudget.ProvincialRegion_ID)?.FirstOrDefault()?.ProvinceName;

                    BranchOfficeYearBudget.BudgetFunds_1 = String.Format("{0:N2}", BranchOfficeYearBudget.BudgetFunds);
                    BranchOfficeYearBudget.AvailableBudgetFunds_1 = String.Format("{0:N2}", BranchOfficeYearBudget.AvailableBudgetFunds);
                    BranchOfficeYearBudget.UsedBudgetFunds_1 = String.Format("{0:N2}", BranchOfficeYearBudget.UsedBudgetFunds);

                    var reimbursementRecordList = ReimbursementRecordBll.GetEntities(x => x.BranchOffice_ID == BranchOfficeYearBudget.BranchOffice_ID &&
                   x.Year == BranchOfficeYearBudget.Year).ToList();

                    var GuangLeFundsList = reimbursementRecordList.Where(x => x.Project_ID == 6).GroupBy(g => g.Project_ID).
                Select(e => new { Project_ID = e.Key, GuangLeFunds = e.Sum(q => q.Funds) });
                    BranchOfficeYearBudget.GuangLeFunds_1 = string.Format("{0:N2}", GuangLeFundsList?.FirstOrDefault()?.GuangLeFunds);

                    var personFundsList = reimbursementRecordList.Where(x => x.Project_ID == 7).GroupBy(g => g.Project_ID).
                   Select(e => new { Project_ID = e.Key, PersonFunds = e.Sum(q => q.Funds) });
                    BranchOfficeYearBudget.PersonFunds_1 = string.Format("{0:N2}", personFundsList?.FirstOrDefault()?.PersonFunds);
                }

                ViewBag.List = List;
                ViewBag.totalPage = totalPage;
                ViewBag.totalRecord = totalRecord;
                return View();
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return Content("<script>alert('��ѯ�����쳣��������������');window.history.back();</script>");
            }
        }

        #endregion

        #region ��ǰ�˿��ŵ��������ݽӿ�
        public ActionResult Funds(int BranchOffice_ID, DateTime ReimbursementDate)
        {
            var View_Rental_VehicleS = BranchOfficeYearBudgetBll.GetEntities(x => x.ID > 0 && x.UserID == User_ID
            && x.BranchOffice_ID == BranchOffice_ID && x.Year == ReimbursementDate.Year
            ).ToList().Select(x => new SelectData { ID = x.ID.ToString(), Name = x.AvailableBudgetFunds.ToString() }).ToList();
            return Json(View_Rental_VehicleS, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}