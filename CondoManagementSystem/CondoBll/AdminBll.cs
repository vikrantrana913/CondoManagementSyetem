using CondoDll.DataAccess;
using CondoDll.Interface;
using CondoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CondoBll
{
    public class AdminBll
    {
        IAdmin iAdmin;

        public AdminBll()
        {
            iAdmin = new SQLAdmin();
        }
        public UserCondoData CondoData(Int64 Id)
        {
            return iAdmin.CondoData(Id);
        }
        public bool EditUserDetails(Int64 userId,EditUserDataEntity obj)
        {
         return iAdmin.EditUserDetails(userId,obj);   
        }
        public bool UpdateUserPassword(string username,string password, ChangePasswordDbModel obj)
        {
            return iAdmin.UpdateUserPassword(username, password,obj);
        }

        public EditUserCondoEntity EditCondoData(Int64 Id,HttpPostedFileBase CondoPicture,EditUserCondoEntity obj)
        {
            return iAdmin.EditCondoData(Id,CondoPicture,obj);
        }

        public List<LoginUserModel> GetAllEmployeeData() 
        {
        return iAdmin.GetAllEmployeeData();
        }

        public UserCondoData GetUserCondoDataByUserId(Int64 Id)
        {
          return  iAdmin.GetUserCondoDataByUserId(Id);
        }

        public EditUserCondoEntity GetAllCondoData(Int64 cId)
        {
            return iAdmin.GetAllCondoData(cId);
        }

        public UserCondoData GetUserCondoData(Int64 UserId)
        {
            return iAdmin.GetUserCondoData(UserId);
        }

        public UserDbModel GetUserData(Int64 id)
        {
            return iAdmin.GetUserData(id);   
        }

        public LoginUserModel LoginCMS(string Username, string EnctypePassword)
        {
            return iAdmin.LoginCMS(Username,EnctypePassword);
        }

    }
}
