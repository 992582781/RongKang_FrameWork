using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RongKang_Entity;
using RongKang_IDal;
using Repository;
using System.Data.SqlClient;
namespace RongKang_Dal
{
    public class ModuleDal : BaseDal<Module>, IModuleDal<Module>
    {
        /// <summary>
        /// 获取用户所有权限Module
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual IEnumerable<Module> GetUserRoleModuleModel(int User_ID)
        {
            List<Module> List = new List<Module>();
            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    string sql = "select *  from RongKang_Module where  ID in(select Module_ID from RongKang_RoleModule where Role_ID in(select UsersInRole.Role_ID from RongKang_UserRole UsersInRole,RongKang_Role pages_Role where UsersInRole.Role_ID=pages_Role.ID  and pages_Role.Switch_OnOff=1 and User_ID=@User_ID))  and  Switch_OnOff=1  UNION  ";
                    sql = sql + "select *  from RongKang_Module where ID in(select Module_ID from RongKang_UserModule  where User_ID=@User_ID) and Switch_OnOff=1 ";
                    SqlParameter[] para1 = new SqlParameter[] { new SqlParameter("@User_ID", User_ID) };
                    return RKRepository.Database.SqlQuery<Module>(sql, para1).ToList();

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
