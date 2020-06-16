using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RongKang_Entity;
using RongKang_ViewModel;
using RongKang_IDal;
using Repository;
using System.Data.SqlClient;
namespace RongKang_Dal
{
    public class UserModuleDal : BaseDal<UserModule>, IUserModuleDal<UserModule>
    {
        /// <summary>
        /// 保存用户权限数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual bool InsertUserModule(IList<int> Modules, int User_ID = 0)
        {
            using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
            {
                using (var dbContextTransaction = RKRepository.Database.BeginTransaction())
                {
                    try
                    {
                        var obj = RKRepository.Set<UserModule>();
                        //string sql = "";
                        List<int> List_RoleModule = new List<int>();//用户的角色权限
                        List<int> List_UserModule_Int = new List<int>();

                        RoleModuleDal roleModuleDal=new RoleModuleDal();

                        //获取用户所在角色的权限

                        //sql = "select Module_ID from RongKang_RoleModule where Role_ID in(select UsersInRole.Role_ID from RongKang_UserRole UsersInRole,RongKang_Role pages_Role where  UsersInRole.Role_ID=pages_Role.ID  and pages_Role.Switch_OnOff=1 and User_ID=@UserId)";
                        //SqlParameter[] para = new SqlParameter[] { new SqlParameter("@UserId", User_ID) };
                        //List_RoleModule = RKRepository.Database.SqlQuery<int>(sql, para).ToList();

                        List_RoleModule = roleModuleDal.GetUserRoleModule(User_ID).ToList();

                        foreach (int x in List_RoleModule)
                        {
                            List_UserModule_Int.Add((Int32)x);
                        }

                        Modules.Remove(0);
                        var qq = Modules.Except(List_UserModule_Int).ToList();


                        //删除用户已经勾选的权限
                        List<UserModule> list = obj.Where(x => x.User_ID == User_ID).ToList();
                        foreach (UserModule item in list)
                        {
                            obj.Remove(item);
                        }


                        foreach (int i in qq)
                        {
                            UserModule userModule = new UserModule();
                            userModule.Module_ID = i;
                            userModule.User_ID = User_ID;
                            obj.Add(userModule);
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





    }
}