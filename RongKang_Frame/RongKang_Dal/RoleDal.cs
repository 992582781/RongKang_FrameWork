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
    public class RoleDal : BaseDal<Role>, IRoleDal<Role>
    {
        /// <summary>
        /// 获取用户所有角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual IEnumerable<Role> GetUserRole(int User_ID)
        {

            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    string sql = "SELECT  R.* FROM  RongKang_UserRole U left join RongKang_Role R on U.Role_ID=R.ID WHERE    User_ID =@User_ID";
                    SqlParameter[] para = new SqlParameter[] { new SqlParameter("@User_ID", User_ID) };
                    return RKRepository.Database.SqlQuery<Role>(sql, para).ToList();
                }
            }
            catch (Exception e)
            {
                Dal_Log.WriteBaseDal(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取所有用户和所有有角色的合集
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual IEnumerable<Role> GetUserAndRole()
        {
            try
            {
                using (RongKang_FrameRepository RKRepository = new RongKang_FrameRepository())
                {
                    List<Role> Authoritylist = new List<Role>();
                    //Role_Remark是1表示是用户 2 表示是角色
                    string sql = "SELECT  ID,User_Name as Role_Name,Switch_OnOff,Role_Remark='1' from RongKang_User UNION ALL SELECT ID, Role_Name+'('+(Role_Remark)+')' as Role_Name,Switch_OnOff,Role_Remark='2'from RongKang_Role";
                    return RKRepository.Database.SqlQuery<Role>(sql).ToList();
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
