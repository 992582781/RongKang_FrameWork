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


        public ProvincialRegionController(IProvincialRegionBll<ProvincialRegion> ProvincialRegionBll) //�������캯�����ж���ע�� 
        {
            this.ProvincialRegionBll = ProvincialRegionBll; //�ڹ��캯���г�ʼ�����������Bll����

            User_ID = Cookie_Operate.GetID();
        }



        #region ����޸�ɾ������
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
                    model = ProvincialRegionBll.GetEntity(x => x.ID == ID);
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
        public ActionResult UpdateInsert(ProvincialRegion Model)
        {

            try
            {
                Model.UserID = User_ID;
                Model.InTime = DateTime.Now;
                string messageStr = "";
                if (string.IsNullOrEmpty(Model.ID.ToString().Trim()) || Model.ID == 0)
                {
                    if (ProvincialRegionBll.Insert(Model, out messageStr, User_ID.ToString()))
                    {
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
                    if (ProvincialRegionBll.Update(Model, out messageStr, User_ID.ToString()))
                    {
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

                var totalRecord = ProvincialRegionBll.GetEntitiesCount(exp);
                var totalPage = (totalRecord + pageSize - 1) / pageSize;
                var List = ProvincialRegionBll.GetEntitiesForPaging(page, pageSize, orderName, "asc", exp).ToList();

                ViewBag.List = List;
                ViewBag.totalPage = totalPage;
                return View();
            }
            catch
            {
                return Content("<script>alert('��ѯ�����쳣��������������');window.history.back();</script>");
            }
        }

        #endregion



        #region ��ǰ�˿��ŵ��������ݽӿ�
        public ActionResult ID()
        {
            var View_Rental_VehicleS = ProvincialRegionBll.GetEntities(x => x.ID > 0).ToList().Select(x => new SelectData { ID = x.ID.ToString(), Name = x.ProvinceName }).ToList();
            return Json(View_Rental_VehicleS, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}