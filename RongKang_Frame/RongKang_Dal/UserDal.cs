using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RongKang_Entity;
using RongKang_ViewModel;
using RongKang_IDal;
using Repository;
using System.Data.Entity;
using System.Data.SqlClient;
namespace RongKang_Dal
{
    public class UserDal : BaseDal<User>, IUserDal<User>
    {
        /// <summary>
        /// 用户和角色数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual bool InsertUserRole(User entity, IList<int> RoleIDS)
        {
            using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
            {
                using (var dbContextTransaction = RKRepository.Database.BeginTransaction())
                {
                    try
                    {
                        var obj = RKRepository.Set<User>();
                        obj.Add(entity);
                        RKRepository.SaveChanges();
                        var User_ID = RKRepository.Users.Where(x => x.User_Name == entity.User_Name).FirstOrDefault().ID;

                        var obj1 = RKRepository.Set<UserRole>();
                        foreach (int RoleID in RoleIDS)
                        {
                            UserRole UsersInRole = new UserRole();
                            UsersInRole.Role_ID = RoleID;
                            UsersInRole.User_ID = User_ID;
                            obj1.Add(UsersInRole);
                        }

                        RKRepository.SaveChanges();
                        dbContextTransaction.Commit();

                        return true;
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        Dal_Log.WriteBaseDal(e.ToString());
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 用户和角色数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual bool UpdateUserRole(User entity, IList<int> RoleIDS)
        {
            using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
            {
                using (var dbContextTransaction = RKRepository.Database.BeginTransaction())
                {
                    try
                    {
                        var obj = RKRepository.Set<User>();
                        obj.Attach(entity);
                        RKRepository.Entry(entity).State = EntityState.Modified;



                        var obj1 = RKRepository.Set<UserRole>();
                        List<UserRole> list_UsersInRole = obj1.Where(x => x.User_ID == entity.ID).ToList();
                        foreach (UserRole item in list_UsersInRole)
                        {
                            obj1.Remove(item);
                        }

                        foreach (int RoleID in RoleIDS)
                        {
                            UserRole UsersInRole = new UserRole();
                            UsersInRole.Role_ID = RoleID;
                            UsersInRole.User_ID = entity.ID;
                            obj1.Add(UsersInRole);
                        }

                        RKRepository.SaveChanges();
                        dbContextTransaction.Commit();

                        return true;
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        Dal_Log.WriteBaseDal(e.ToString());
                        return false;
                    }
                }
            }
        }



        /// <summary>
        /// 获取角色的所有用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual IEnumerable<User> GetRoleUser(int Role_ID)
        {
            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    string sql = "SELECT  R.* FROM  RongKang_UserRole U left join RongKang_User R on U.User_ID=R.ID WHERE  Role_ID =@Role_ID";
                    SqlParameter[] para = new SqlParameter[] { new SqlParameter("@Role_ID", Role_ID) };
                    return RKRepository.Database.SqlQuery<User>(sql, para).ToList();
                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return null;
            }
        }
    }
}
