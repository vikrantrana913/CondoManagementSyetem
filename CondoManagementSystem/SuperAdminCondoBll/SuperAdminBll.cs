using CondoBll;
using CondoEntity;
using SuperAdminCondoDll.DataAccess;
using SuperAdminCondoDll.Interface;
using SuperCondoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SuperAdminCondoBll
{
    public class SuperAdminBll
    {
        ISuperAdmin iAdmin;

        public SuperAdminBll()
        {
            iAdmin= new SQLSuperAdmin();
        }

        public List<UserCondoData> UserCondoList()
        {
            return iAdmin.UserCondoList();
        }

        public List<UserCondoData> GetAllUserCondoList()
        {
            return iAdmin.GetAllUserCondoList();
        }

        public bool DeleteUserCondoData(Int64 Id)
        {
            return iAdmin.DeleteUserCondoData(Id);
        }

        public bool UpdateUserCondoData(Int64 Id,UpdateUserCondoDbModel obj)
        {
            return iAdmin.UpdateUserCondoData(Id,obj);
        }

        public UserCondoData GetUserCondoDataById(Int64 Id)
        {
            return iAdmin.GetUserCondoDataById(Id);
        }

        public List<UserCondoData> GetAllUsers()
        {
            return iAdmin.GetAllUsers();    
        }
        public List<UserCondoData> GetAllCondo()
        {
            return iAdmin.GetAllCondo();    
        }
        public bool EditCondo(Int64 Id, CondoDbModel obj)
        {
            return iAdmin.EditCondo(Id,obj);
        }
        public bool EditUserDetail(Int64 Id, UpdateAdminData obj)
        {
            return iAdmin.EditUserDetail(Id, obj);
        }

        public UserDbModel GetUserProfileById(Int64 id)
        {
            return iAdmin.GetUserProfileById(id);
        }

        public UserDbModel GetUserDataById(Int64 id)
        {
            return iAdmin.GetUserDataById(id);
        }

        public AssignUserCondoData getUserDataByid(Int64 id)
        {
            return iAdmin.getUserDataByid(id);
        }

        public AssignUserCondoData GetCondo(Int64 id)
        {
            return iAdmin.GetCondo(id);
        }

        public CondoDbModel GetCondoDataById(Int64 id)
        {
            return iAdmin.GetCondoDataById(id);
        }

        public bool AssignCondoToUsers(AssignUserCondoData obj)
        {
            return iAdmin.AssignCondoToUsers(obj);
        }

        public bool AddUserData(UserDbModel userDb)
        {
            return iAdmin.AddUserData(userDb);
        }

        public LoginSuperAdminDbModel LoginSuperAdminCMS(string Username,string EncrytypePassword)
        {
            return iAdmin.LoginSuperAdminCMS(Username,EncrytypePassword);
        }

        public List<AssignUserCondoData> GetUserId()
        {
            return iAdmin.GetUserId(); 
        }

        public List<AssignUserCondoData> GetCondoId()
        {
            return iAdmin.GetCondoId();
        }

        public bool DeleteCondo(Int64 id)
        {
            return iAdmin.DeleteCondo(id);
        }

        public bool AddCondoData(CondoDbModel condoDb)
        {
            return iAdmin.AddCondoData(condoDb);
        }

        public bool UpdateUserPassword(string username, string password, ChangePasswordDbModel obj)
        {
            return iAdmin.UpdateUserPassword(username,password, obj);    
        }

        public bool DeleteUsers(Int64 id)
        {
            return iAdmin.DeleteUsers(id);
        }



    }
}
