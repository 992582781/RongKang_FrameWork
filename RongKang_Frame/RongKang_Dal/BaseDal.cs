using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using RongKang_IDal;

namespace RongKang_Dal
{
    public class BaseDal<T> : IBaseDal<T> where T : class//限制T的类型为class或者对象
    {


        #region 查询普通实现方案(基于Lambda表达式的Where查询)
        /// <summary>
        /// 获取所有Entity
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns>返回IEnumerable类型</returns>
        public virtual IEnumerable<T> GetEntities(Func<T, bool> exp)
        {
            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    return RKRepository.Set<T>().Where(exp).ToList<T>();
                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return null;
            }

        }
        /// <summary>
        /// 计算总个数(分页)
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual int GetEntitiesCount(Func<T, bool> exp)
        {
            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    return RKRepository.Set<T>().Where(exp).ToList().Count();

                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return -1;
            }
        }

        /// <summary>
        /// sql计算总个数(分页)
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual int GetEntitiesCount(string exp)
        {
            try
            {
                var ClassName = typeof(T).Name.ToString();
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    string sql = @"select count(ID) from  RongKang_" + ClassName + " where  " + exp + " ";
                    return RKRepository.Database.SqlQuery<int>(sql).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return -1;
            }
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
            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    if (sortOrder == "asc") //升序排列
                    {
                        return RKRepository.Set<T>().Where(exp).OrderBy(orderName).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    }
                    else
                    {
                        return RKRepository.Set<T>().Where(exp).OrderByDescending(orderName).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return null;
            }

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
        public virtual IEnumerable<T> GetEntitiesForPaging(int pageNumber, int pageSize,  string  orderName, string sortOrder, string exp)
        {
            try
            {
                var ClassName = typeof(T).Name.ToString();
                string sql = "";
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    if (string.IsNullOrEmpty(orderName))
                    {
                        sql = @"select * from (select row_number()over(order by ID)rownumber,* from  RongKang_" + ClassName + " where  " + exp + " )a where rownumber>({0}-1)* {1} AND rownumber <= {0} * {1}";
                    }
                    else if (orderName == "Module_Order")
                    {
                        sql = @"select * from (select row_number()over(order by LEFT(Module_Order, 3))rownumber,* from  RongKang_" + ClassName + " where  " + exp + " )a where rownumber>({0}-1)* {1} AND rownumber <= {0} * {1} ";
                    }
                    else
                    {
                        sql = @"select * from (select row_number()over(order by ID)rownumber,* from  RongKang_" + ClassName + " where  " + exp + " )a where rownumber>({0}-1)* {1} AND rownumber <= {0} * {1}   order by " + orderName + " " + sortOrder + " ";
                    }

                    sql = string.Format(sql, pageNumber, pageSize);


                    return RKRepository.Database.SqlQuery<T>(sql).ToList();
                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return null;
            }
        }




        /// <summary>
        /// 根据条件查找满足条件的一个entites
        /// </summary>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        public virtual T GetEntity(Func<T, bool> exp)
        {
            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    return RKRepository.Set<T>().Where(exp).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 根据条件查找满足条件的第一个entites
        /// </summary>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        public virtual T GetFirstEntity(Func<T, bool> exp)
        {
            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    return RKRepository.Set<T>().Where(exp).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 根据条件查找满足条件的最后一个entites
        /// </summary>
        /// <param name="exp">lambda查询条件where</param>
        /// <returns></returns>
        public virtual T GetLastEntity(Func<T, bool> exp)
        {
            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    return RKRepository.Set<T>().Where(exp).LastOrDefault();
                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return null;
            }
        }

 

        #endregion

        #region 增改删实现
        /// <summary>
        /// 插入Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool Insert(T entity)
        {
            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    var obj = RKRepository.Set<T>();
                    obj.Add(entity);
                    return RKRepository.SaveChanges() > 0;

                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return false;
            }

        }
        /// <summary>
        /// 更新Entity(注意这里使用的傻瓜式更新,可能性能略低)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool Update(T entity)
        {
            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    var obj = RKRepository.Set<T>();
                    obj.Attach(entity);
                    RKRepository.Entry(entity).State = EntityState.Modified;
                    return RKRepository.SaveChanges() > 0;
                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// 删除Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Delete(T entity)
        {
            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    var obj = RKRepository.Set<T>();
                    if (entity != null)
                    {
                        obj.Attach(entity);
                        RKRepository.Entry(entity).State = EntityState.Deleted;
                        obj.Remove(entity);
                        return RKRepository.SaveChanges() > 0;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return false;
            }

        }
        #endregion
    }
}

 
