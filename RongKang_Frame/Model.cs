using System;
using System.Collections.Generic;
using System.Text;

namespace Agile.Config.Protocol.Model
{
    /// <summary>
    /// 省份+负责人(基础数据)
    /// </summary>
    public class ProvincialRegion
    {
        public int ID { get; set; }
        /// <summary>
        /// 省份 如：江苏省
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 负责人 如：李明
        /// </summary>
        public string Leader { get; set; }
       public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }

    /// <summary>
    /// 省份+负责人年度预算
    /// </summary>
    public class YearBudget
    {
        public int ID { get; set; }
        /// <summary>
        /// 省份+负责人(基础数据)
        /// </summary>
        public int ProvincialRegionID { get; set; }
        /// <summary>
        /// 所属年度
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 预算资金 修改不能小于 BudgetFunds-AvailableBudgetFunds（已分配的）
        /// 或者UsedBudgetFunds
        /// </summary>
        public decimal BudgetFunds { get; set; }
        /// <summary>
        /// 可用预算资金
        /// </summary>
        public decimal AvailableBudgetFunds { get; set; }
        /// <summary>
        /// 已用预算资金
        /// </summary>
        public decimal UsedBudgetFunds { get; set; }
		
		 /// <summary>
        /// 管理费预算资金 修改不能小于 ManagementFunds-AvailableManagementFunds（已分配的）
        /// 或者UsedManagementFunds
        /// </summary>
        public decimal ManagementFunds { get; set; }
        /// <summary>
        /// 可用管理费预算资金
        /// </summary>
        public decimal AvailableManagementFunds { get; set; }
        /// <summary>
        /// 已用管理费预算资金
        /// </summary>
        public decimal UsedManagementFunds { get; set; }
		
        public int UserID { get; set; }
        public DateTime InTime { get; set; }  getdate()
    }
	
	 /// <summary>
    /// 省份+负责人管理费年度预算，不使用了
    /// </summary>
	 public class ManagementBudget
	 {
		  public int ID { get; set; }
        /// <summary>
        /// 省份+负责人(基础数据)
        /// </summary>
        public int ProvincialRegionID { get; set; }
        /// <summary>
        /// 所属年度
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 管理费预算资金 修改不能小于 ManagementFunds-AvailableManagementFunds（已分配的）
        /// 或者UsedManagementFunds
        /// </summary>
        public decimal ManagementFunds { get; set; }
        /// <summary>
        /// 可用管理费预算资金
        /// </summary>
        public decimal AvailableManagementFunds { get; set; }
        /// <summary>
        /// 已用管理费预算资金
        /// </summary>
        public decimal UsedManagementFunds { get; set; }
        public int UserID { get; set; }
        public DateTime InTime { get; set; }  getdate()
	 }
	
	

    /// <summary>
    /// 报销商业 (基础数据)
    /// </summary>
    public class BranchOffice
    {
        public int ID { get; set; }
        /// <summary>
        /// 省份+负责人(基础数据)
        /// </summary>
        public int ProvincialRegion_ID { get; set; }
        /// <summary>
        /// 商业名称
        /// </summary>
        public string BranchOfficeName { get; set; }
       public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }

    /// <summary>
    /// 报销商业年度预算
    /// </summary>
    public class BranchOfficeYearBudget
    {
        public int ID { get; set; }
        /// <summary>
        /// 省份+负责人(基础数据)
        /// </summary>
        public int ProvincialRegionID { get; set; }
        /// <summary>
        /// 报销商业 (基础数据)
        /// </summary>
        public int BranchOfficeID { get; set; }

        /// <summary>
        /// 预算年度信息
        /// </summary>
        public int YearBudgetID { get; set; }
        /// <summary>
        /// 所属年度
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 预算资金 修改不能小于 BudgetFunds-AvailableBudgetFunds（已分配的）
        /// 或者UsedBudgetFunds
        /// </summary>
        public decimal BudgetFunds { get; set; }
        /// <summary>
        /// 可用预算资金
        /// </summary>
        public decimal AvailableBudgetFunds { get; set; }
        /// <summary>
        /// 已用预算资金
        /// </summary>
        public decimal UsedBudgetFunds { get; set; }
       public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }

    /// <summary>
    /// 3大科目(基础数据)
    /// </summary>
    public class Subject
    {
        public int ID { get; set; }
        /// <summary>
        /// 科目名称
        /// </summary>
        public string SubjectName { get; set; }
       public int UserID { get; set; }
        public DateTime InTime { get; set; } getdate()
    }

    /// <summary>
    /// 5个项目
    /// </summary>
    public class Project
    {
        public int ID { get; set; }
        /// <summary>
        /// 所属区域（省份+负责人(基础数据)）
        /// </summary>
        public int ProvincialRegionID { get; set; }
        /// <summary>
        /// 科目ID
        /// </summary>
        public string SubjectID { get; set; }
        //项目名称
        public string ProjectName { get; set; }
        /// <summary>
        /// 报销比例
        /// </summary>
        public decimal Ratio { get; set; }
       public int UserID { get; set; }
        public DateTime InTime { get; set; } getdate()
    }

    /// <summary>
    /// 产品名称
    /// </summary>
    public class Product
    {
        public int ID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
       public int UserID { get; set; }
        public DateTime InTime { get; set; }
    }

    /// <summary>
    /// 报销记录
    /// </summary>
    public class ReimbursementRecord
    {
        public int ID { get; set; }
        /// <summary>
        /// 报销日期，需要判断 年度预算是否存在；
        /// </summary>
        public DateTime ReimbursementDate { get; set; }
        /// <summary>
        /// 报销的年
        /// </summary>
        public DateTime Year { get; set; }
        /// <summary>
        /// 报销的月
        /// </summary>
        public DateTime Month { get; set; }
    }
}

SELECT ProvincialRegions.ProvinceName,ProvincialRegions.Leader,ProvincialRegions.Remark,
users.User_Name, 
YearBudgets.BudgetFunds,YearBudgets.AvailableBudgetFunds,YearBudgets.UsedBudgetFunds,
ManagementBudgets.ManagementFunds,ManagementBudgets.AvailableManagementFunds,ManagementBudgets.UsedManagementFunds
FROM dbo.RongKang_ProvincialRegion ProvincialRegions
LEFT JOIN dbo.RongKang_User users ON users.ID = ProvincialRegions.UserID
LEFT JOIN dbo.RongKang_YearBudget YearBudgets  ON YearBudgets.ProvincialRegionID = ProvincialRegions.ID
LEFT JOIN RongKang_ManagementBudget ManagementBudgets ON ManagementBudgets.ProvincialRegionID = ProvincialRegions.ID

     
