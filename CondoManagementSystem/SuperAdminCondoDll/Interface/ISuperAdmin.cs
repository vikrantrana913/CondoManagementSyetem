using CondoBll;
using CondoEntity;
using SuperCondoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SuperAdminCondoDll.Interface
{
    public interface ISuperAdmin
    {
        LoginSuperAdminDbModel LoginSuperAdminCMS(string Username, string EncrytypePassword);

        bool AddCondoData(CondoDbModel condoDb);

        List<UserCondoData> GetAllCondo();

        List<UserCondoData> UserCondoList();

        List<UserCondoData> GetAllUserCondoList();

        bool DeleteUserCondoData(Int64 Id);

        bool UpdateUserCondoData(Int64 Id,UpdateUserCondoDbModel obj);

        UserCondoData GetUserCondoDataById(Int64 Id);

        bool AddUserData(UserDbModel userDb);

        List<UserCondoData> GetAllUsers();

        bool EditUserDetail(Int64 Id, UpdateAdminData obj);

        UserDbModel GetUserDataById(Int64 id);

        bool EditCondo(Int64 Id, CondoDbModel obj);

        CondoDbModel GetCondoDataById(Int64 id);

        AssignUserCondoData GetCondo(Int64 id);

        AssignUserCondoData getUserDataByid(Int64 id);

        UserDbModel GetUserProfileById(Int64 id);


        bool DeleteCondo(Int64 id);

        bool DeleteUsers(Int64 id);

        bool UpdateUserPassword(string username, string password, ChangePasswordDbModel obj);

        List<AssignUserCondoData> GetUserId();

        List<AssignUserCondoData> GetCondoId();

        bool AssignCondoToUsers(AssignUserCondoData obj);

    }

}
