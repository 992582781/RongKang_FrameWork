using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RongKang_Entity;
using RongKang_ViewModel;
using RongKang_IBll;
using RongKang_IDal;
using Web_Common;
namespace RongKang_Bll
{
    public class UserBll : BaseBll<User>, IUserBll<User>
    {
        private IUserDal<User> dal;
        public UserBll(IUserDal<User> dal)
            : base(dal)
        {
            this.dal = dal;//在构造函数中初始化类的dal属性
        }
        /// <summary>
        /// 用户和角色数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual bool InsertUserRole(User entity,IList<int> RoleID, out string messageStr, string User_ID)
        {
            if (!string.IsNullOrEmpty(CustomAttributeHelper.ValidateString(entity, User_ID.ToString())))
            {
                messageStr = CustomAttributeHelper.ValidateString(entity, User_ID.ToString());
                return false;
            }
            else
            {
               
                if(dal.InsertUserRole(entity,RoleID))
                {
                    messageStr = "";
                    return true;
                }
                else
                {
                    messageStr = "用户名已经使用";
                    return false;
                }
            }
        }

        /// <summary>
        /// 用户和角色数据更新事务代码
        /// </summary>
        /// <param name="exp">Lambda条件的where</param>
        /// <returns></returns>
        public virtual bool UpdateUserRole(User entity, IList<int> RoleID, out string messageStr, string User_ID)
        {
            if (!string.IsNullOrEmpty(CustomAttributeHelper.ValidateString(entity, User_ID.ToString())))
            {
                messageStr = CustomAttributeHelper.ValidateString(entity, User_ID.ToString());
                return false;
            }
            else
            {
                if (dal.UpdateUserRole(entity, RoleID))
                {
                    messageStr = "";
                    return true;
                }
                else
                {
                    messageStr = "用户名已经使用";
                    return false;
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

            return dal.GetRoleUser(Role_ID);
        }
    }
}
