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
    public class RoleModuleDal : BaseDal<RoleModule>, IRoleModuleDal<RoleModule>
    {
        /// <summary>
        /// 保存角色权限数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual bool InsertRoleModule(IList<int> Modules, int Role_ID = 0)
        {
            using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
            {
                using (var dbContextTransaction = RKRepository.Database.BeginTransaction())
                {
                    try
                    {
                        var obj = RKRepository.Set<RoleModule>();
                        List<RoleModule> list = obj.Where(x => x.Role_ID == Role_ID).ToList();
                        foreach (RoleModule item in list)
                        {
                            obj.Remove(item);
                        }
                        Modules.Remove(0);
                        foreach (int i in Modules)
                        {
                            RoleModule roleModule = new RoleModule();
                            roleModule.Role_ID = Role_ID;
                            roleModule.Module_ID = i;

                            obj.Add(roleModule);
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
        /// 获取用户所有角色的权限Module_ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual IEnumerable<int> GetUserRoleModule(int User_ID)
        {

            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    string sql = "select Module_ID from RongKang_RoleModule where Role_ID in(select UsersInRole.Role_ID from RongKang_UserRole UsersInRole,RongKang_Role pages_Role where  UsersInRole.Role_ID=pages_Role.ID  and pages_Role.Switch_OnOff=1 and User_ID=@UserId)";
                    SqlParameter[] para = new SqlParameter[] { new SqlParameter("@UserId", User_ID) };
                    return RKRepository.Database.SqlQuery<int>(sql, para).ToList();
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
