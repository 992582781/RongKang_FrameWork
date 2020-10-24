using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RongKang_IBll;
using RongKang_IDal;
using Web_Common;

namespace RongKang_Bll
{

    public class BaseBll<T> : IBaseBll<T> where T : class, new()//限制T的类型为class或者对象
    {
        private IBaseDal<T> dal;

        public BaseBll() { }

        public BaseBll(IBaseDal<T> dal)
        {
            this.dal = dal; //在构造函数中获取子类传递的值
        }


        #region 查询普通实现方案(基于Lambda表达式的Where查询)
        /// <summary>
        /// 获取所有Entity
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns>返回IEnumerable类型</returns>
        public virtual IEnumerable<T> GetEntities(Func<T, bool> exp)
        {

            return dal.GetEntities(exp);
        }
        /// <summary>
        /// 计算总个数(分页)
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual int GetEntitiesCount(Func<T, bool> exp)
        {
            return dal.GetEntitiesCount(exp);
        }

        /// <summary>
        /// sql计算总个数(分页)
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual int GetEntitiesCount(string exp)
        {
            return dal.GetEntitiesCount(exp);
        }


        /// <summary>
        /// 分页查询(Linq分页方式)
        /// </summary>
        /// <param name="pageNumber">当前页</param>
        /// <param name="pageSize">页码</param>
        /// <param name="orderName">lambda排序名称</param>
        /// <param name="sortOrder">排序(升序or降序)</param>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetEntitiesForPaging(int pageNumber, int pageSize, Func<T, string> orderName, string sortOrder, Func<T, bool> exp)
        {
            return dal.GetEntitiesForPaging(pageNumber, pageSize, orderName, sortOrder, exp);

        }

        /// <summary>
        /// 单表sql查询分页
        /// </summary>
        /// <param name="pageNumber">页数</param>
        /// <param name="pageSize">条数</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="sortOrder">排序方式 asc desc</param>
        /// <param name="exp">查询条件</param>
        /// <returns></returns>
        public virtual IEnumerable<T> SingleGetEntitiesForPaging(int pageNumber, int pageSize, string orderName, string sortOrder, string exp)
        {
            return dal.SingleGetEntitiesForPaging(pageNumber, pageSize, orderName, sortOrder, exp);
        }


        /// <summary>
        /// sql查询分页
        /// </summary>
        /// <param name="pageNumber">页数</param>
        /// <param name="pageSize">条数</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="sortOrder">排序方式 asc desc</param>
        /// <param name="exp">查询条件</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetEntitiesForPaging(int pageNumber, int pageSize, string orderName, string sortOrder, string exp)
        {
            return dal.GetEntitiesForPaging(pageNumber, pageSize, orderName, sortOrder, exp);
        }

 

        /// <summary>
        /// 根据条件查找满足条件的一个entites
        /// </summary>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        public virtual T GetEntity(Func<T, bool> exp)
        {
            return dal.GetEntity(exp);
        }

        /// <summary>
        /// 根据条件查找满足条件的第一个entites
        /// </summary>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        public virtual T GetFirstEntity(Func<T, bool> exp)
        {
            return dal.GetFirstEntity(exp);
        }

        /// <summary>
        /// 根据条件查找满足条件的最后一个entites
        /// </summary>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        public virtual T GetLastEntity(Func<T, bool> exp)
        {
            return dal.GetLastEntity(exp);
        }

 

        #endregion

        #region 增改删实现
        /// <summary>
        /// 插入Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool Insert(T entity, out string messageStr, string User_ID)
        {
            if (!string.IsNullOrEmpty(CustomAttributeHelper.ValidateString(entity, User_ID.ToString())))
            {
                messageStr = CustomAttributeHelper.ValidateString(entity, User_ID.ToString());
                return false;
            }
            else
            {
                messageStr = "";
                return dal.Insert(entity);
            }

        }
        /// <summary>
        /// 更新Entity(注意这里使用的傻瓜式更新,可能性能略低)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool Update(T entity, out string messageStr, string User_ID)
        {
            if (!string.IsNullOrEmpty(CustomAttributeHelper.ValidateString(entity, User_ID.ToString())))
            {
                messageStr = CustomAttributeHelper.ValidateString(entity, User_ID.ToString());
                return false;
            }
            else
            {
                messageStr = "";
                return dal.Update(entity);
            }

        }
        /// <summary>
        /// 删除Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Delete(T entity)
        {
            return dal.Delete(entity);

        }
        #endregion
    }
}