using CondoBll;
using CondoDll.ADO;
using CondoEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace CondoDll.DataAccess
{
    public partial class SQLAdmin
    {

        private static bool ReturnBool(int Result)
        {
            return Result > 0;
        }
        public UserCondoData CondoData(Int64 Id)
        {
            using (AdoExecution exec = new AdoExecution(SQLAdmin.GetConnectionString()))
            {
                using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "getUserbyId",
                    new SqlParameter("@UserId", Id)
                    ))
                {
                    UserCondoData userCondo = new UserCondoData();


                    while (dr.Read())
                    {
                        userCondo.CondoId = Convert.ToInt64(dr["CondoId"]);
                        userCondo.CondoName = Convert.ToString(dr["CondoName"]);
                        userCondo.CondoPicture = Convert.ToString(dr["CondoPicture"]);
                        userCondo.CondoEmail = Convert.ToString(dr["CondoEmail"]);
                        userCondo.Address = Convert.ToString(dr["Address"]);
                        userCondo.Contact = Convert.ToString(dr["Contact"]);
                        userCondo.Name = Convert.ToString(dr["Name"]);
                        userCondo.Email = Convert.ToString(dr["Email"]);
                    }
                    return userCondo;
                }

            }
        }

        public string EncryptPassword(string password)
        {
            string EncrptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(password);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }


        public bool UpdateUserPassword(string username, string password, ChangePasswordDbModel obj)
        {
            using (AdoExecution exec = new AdoExecution(SQLAdmin.GetConnectionString()))
            {
                int Result = exec.ExecuteNonQuery(CommandType.StoredProcedure, "ChangeAdminPassword",
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", EncryptPassword(password)),
                    new SqlParameter("@NewPassword", EncryptPassword(obj.NewPassword)));

                return ReturnBool(Result);
            }

        }


        public bool EditUserDetails(Int64 userId,EditUserDataEntity obj)
        {
            
            using(AdoExecution exec=new AdoExecution(SQLAdmin.GetConnectionString()))
            {
                int Result = exec.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateUsersData",
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@Name", obj.Name),
                    new SqlParameter("@Email", obj.Email));
                return ReturnBool(Result);
            }
           
            
        }


        public EditUserCondoEntity EditCondoData(Int64 Id, HttpPostedFileBase CondoPicture,EditUserCondoEntity obj)
        {

            using (AdoExecution exec = new AdoExecution(SQLAdmin.GetConnectionString()))
            {
                using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "UpdateCondo",
                    new SqlParameter("@CondoId", Id),
                    new SqlParameter("@CondoName", obj.CondoName),
                    new SqlParameter("@CondoEmail", obj.CondoEmail),
                    new SqlParameter("@Contact", obj.Contact),
                    new SqlParameter("@Address", obj.Address),
                    new SqlParameter("@CondoPicture", obj.CondoPicture)
                    ))
                {

                }
            }
            return obj;
        }


        public List<LoginUserModel> GetAllEmployeeData()
        {
            List<LoginUserModel> adminObj = new List<LoginUserModel>();
            using (AdoExecution exec = new AdoExecution(SQLAdmin.GetConnectionString()))
            {
                using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "SignInUsers"))
                {
                    while (dr.Read())
                    {
                        var emp = new LoginUserModel();
                        emp.Username = Convert.ToString(dr["Username"]);
                        emp.Password = Convert.ToString(dr["Password"]);
                        emp.UserId = Convert.ToInt64(dr["UserId"]);
                        adminObj.Add(emp);
                    }
                }
                return adminObj;
            }
        }


        public UserDbModel GetUserData(Int64 id)
        {
            using(AdoExecution exec=new AdoExecution(SQLAdmin.GetConnectionString()))
            {
                using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "getAdminProfilebyId",
                    new SqlParameter("@UserId", id)
                    ))
                {
                    UserDbModel obj = new UserDbModel();
                    while(dr.Read())
                    {
                        obj.UserId = Convert.ToInt64(dr["UserId"]);
                        obj.Name = Convert.ToString(dr["Name"]);
                        obj.Email = Convert.ToString(dr["Email"]);

                    }
                    return obj;

                }
            }
        }



        public UserCondoData GetUserCondoData(Int64 id)
        {
            using (AdoExecution exec = new AdoExecution(SQLAdmin.GetConnectionString()))
            {
                using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "GetAllCondo",
                    new SqlParameter("@UserId", id)
                    ))
                {
                    UserCondoData userCondo = new UserCondoData();
                    while (dr.Read())
                    {
                        userCondo.UserId = Convert.ToInt64(dr["UserId"]);
                        userCondo.CondoName = Convert.ToString(dr["CondoName"]);
                        userCondo.CondoPicture = Convert.ToString(dr["CondoPicture"]);
                        userCondo.CondoEmail = Convert.ToString(dr["CondoEmail"]);
                        userCondo.Address = Convert.ToString(dr["Address"]);
                        userCondo.Contact = Convert.ToString(dr["Contact"]);
                        userCondo.Email = Convert.ToString(dr["Email"]);
                        userCondo.Name = Convert.ToString(dr["Name"]);
                        userCondo.Username = Convert.ToString(dr["Username"]);

                    }
                    return userCondo;
                }
            }
        }




        public EditUserCondoEntity GetAllCondoData(Int64 cId)
        {

            using (AdoExecution exec = new AdoExecution(SQLAdmin.GetConnectionString()))
            {
                using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "GetAllCondoDetailbyCondoId",
                    new SqlParameter("@CondoId",cId)
                    ))
                {
                    EditUserCondoEntity condoObj = new EditUserCondoEntity();
                    while (dr.Read())
                    {
                        condoObj.CondoId = Convert.ToInt64(dr["CondoId"]);
                        condoObj.CondoName = Convert.ToString(dr["CondoName"]);
                        condoObj.CondoPicture = Convert.ToString(dr["CondoPicture"]);
                        condoObj.CondoEmail = Convert.ToString(dr["CondoEmail"]);
                        condoObj.Address = Convert.ToString(dr["Address"]);
                        condoObj.Contact = Convert.ToString(dr["Contact"]);

                    }
                    return condoObj;
                }

            }
        }

        public LoginUserModel LoginCMS(string Username, string EnctypePassword)
        {
            LoginUserModel loginEntity = new LoginUserModel();
            using (AdoExecution exec = new AdoExecution(GetConnectionString()))
            {
                using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "SignInUsers",
                    new SqlParameter("@Username", Username),
                    new SqlParameter("@Password", EncryptPassword(EnctypePassword))
                    ))
                {
                    while (dr.Read())
                    {
                        loginEntity.UserId = Convert.ToInt64(dr["UserId"]);

                        loginEntity.Name = Convert.ToString(dr["Name"]);
                        loginEntity.Username = Convert.ToString(dr["Username"]);
                        loginEntity.Password = Convert.ToString(dr["Password"]);
                        loginEntity.EmailAddress = Convert.ToString(dr["Email"]);
                        loginEntity.RoleType = Convert.ToInt32(dr["RoleType"]);
                        
                    }
                    return loginEntity;
                }
            }
        }
       
        public UserCondoData GetUserCondoDataByUserId(Int64 Id)
        {
           UserCondoData user=new UserCondoData();  
            using(AdoExecution exec=new AdoExecution(SQLAdmin.GetConnectionString()))
            {
                using(IDataReader dr=exec.ExecuteReader(CommandType.StoredProcedure, "getAllCondoDataByUserId",
                    new SqlParameter("@UserId",Id)
                    ))
                {
                    while (dr.Read()) 
                    {
                        user.CondoId = Convert.ToInt64(dr["CondoId"]);
                        user.CondoName = Convert.ToString(dr["CondoName"]);
                        user.CondoPicture = Convert.ToString(dr["CondoPicture"]);
                        user.CondoEmail = Convert.ToString(dr["CondoEmail"]);
                        user.Address = Convert.ToString(dr["Address"]);
                        user.Contact = Convert.ToString(dr["Contact"]);
                        user.Name = Convert.ToString(dr["Name"]);
                        user.Email = Convert.ToString(dr["Email"]);
                    }
                }
            }
            return user;    
        }
    }
}


