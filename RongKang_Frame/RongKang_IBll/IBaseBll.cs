using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RongKang_IBll
{
   public interface IBaseBll<T>
    {
        #region 查询普通实现方案(基于Lambda表达式的Where查询)
        /// <summary>
        /// 获取所有Entity
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        IEnumerable<T> GetEntities(Func<T, bool> exp);

        /// <summary>
        /// 计算总个数(分页)
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        int GetEntitiesCount(Func<T, bool> exp);

        /// <summary>
        /// sql计算总个数(分页)
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        int GetEntitiesCount(string exp);
         

        /// <summary>
        /// 分页查询(Linq分页方式)
        /// </summary>
        /// <param name="pageNumber">当前页</param>
        /// <param name="pageSize">页码</param>
        /// <param name="orderName">lambda排序名称</param>
        /// <param name="sortOrder">排序(升序or降序)</param>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        IEnumerable<T> GetEntitiesForPaging(int pageNumber, int pageSize, Func<T, string> orderName, string sortOrder, Func<T, bool> exp);

        /// <summary>
        /// 单表sql查询分页
        /// </summary>
        /// <param name="pageNumber">页数</param>
        /// <param name="pageSize">条数</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="sortOrder">排序方式 asc desc</param>
        /// <param name="exp">查询条件</param>
        /// <returns></returns>
        IEnumerable<T> SingleGetEntitiesForPaging(int pageNumber, int pageSize, string orderName, string sortOrder, string exp);


        /// <summary>
        /// sql查询分页
        /// </summary>
        /// <param name="pageNumber">页数</param>
        /// <param name="pageSize">条数</param>
        /// <param name="orderName">排序字段</param>
        /// <param name="sortOrder">排序方式 asc desc</param>
        /// <param name="exp">查询条件</param>
        /// <returns></returns>
        IEnumerable<T> GetEntitiesForPaging(int pageNumber, int pageSize, string orderName, string sortOrder, string exp);
         

        /// <summary>
        /// 根据条件查找满足条件的一个entites
        /// </summary>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        T GetEntity(Func<T, bool> exp);

        /// <summary>
        /// 根据条件查找满足条件的第一个entites
        /// </summary>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        T GetFirstEntity(Func<T, bool> exp);

        /// <summary>
        /// 根据条件查找满足条件的最后一个entites
        /// </summary>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        T GetLastEntity(Func<T, bool> exp);
 
        #endregion

        //#endregion
        /// <summary>
        /// 插入Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Insert(T entity, out string messageStr, string User_ID);
        /// <summary>
        /// 更新Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(T entity, out string messageStr, string User_ID);
        /// <summary>
        /// 删除Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(T entity);
        /// <summary>
        /// 删除实现 存储过程实现方式(调用spDelete+表名+ 主键ID)
        /// </summary>
        /// <param name="ID">删除的主键</param>
        /// <returns></returns>
        //bool Delete(object ID);
    }
}