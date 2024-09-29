using CondoBll;
using CondoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CondoDll.Interface
{
    public interface IAdmin
    {
        List<LoginUserModel> GetAllEmployeeData();

        EditUserCondoEntity GetAllCondoData(Int64 cId);

        UserCondoData GetUserCondoData(Int64 UserId);

        UserCondoData CondoData(Int64 Id);

        UserDbModel  GetUserData(Int64 id);

        bool EditUserDetails(Int64 userId,EditUserDataEntity obj);

        EditUserCondoEntity EditCondoData(Int64 Id,HttpPostedFileBase CondoPicture,EditUserCondoEntity obj);
       
        bool UpdateUserPassword(string username,string password, ChangePasswordDbModel obj);
        
        LoginUserModel LoginCMS(string Username, string EnctypePassword);
       
        UserCondoData GetUserCondoDataByUserId(Int64 Id);
    }
}
