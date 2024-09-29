using CondoBll;
using CondoEntity;
using SuperAdminCondoDll.ADO;
using SuperCondoEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Role = CondoBll.Role;

namespace SuperAdminCondoDll.DataAccess
{
    public partial class SQLSuperAdmin
    {
        public bool UpdateUserPassword(string username, string password, ChangePasswordDbModel obj)
        {
            using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
            {
                int Result = exec.ExecuteNonQuery(CommandType.StoredProcedure, "ChangeAdminPassword",
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", EncryptPassword(password)),
                    new SqlParameter("@NewPassword", EncryptPassword(obj.NewPassword)));

                return ReturnBool(Result);
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

        public string DecryptPassword(string encryptpass)
        {
            try
            {
                encryptpass = encryptpass.Replace(" ", "+");
                string DecryptKey = "2013;[pnuLIT)WebCodeExpert";
                byte[] byKey = { };
                byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
                byte[] inputByteArray = new byte[encryptpass.Length];
                byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(encryptpass.Replace(" ", "+"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool DeleteUserCondoData(Int64 Id)
        {
            using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
            {
                int Result = exec.ExecuteNonQuery(CommandType.StoredProcedure, "DeleteUserCondo",
                    new SqlParameter("@UserCondoId", Id));
                return ReturnBool(Result);
            }

        }

        public List<UserCondoData> UserCondoList()
        {
            List<UserCondoData> userCondos = new List<UserCondoData>();
            using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
            {
                using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "getUserCondoData"))
                {
                    while (dr.Read())
                    {
                        userCondos.Add(new UserCondoData
                        {
                            UserCondoId = (dr["UserCondoId"] is DBNull) ? 0 : Convert.ToInt64(dr["UserCondoId"]),
                            UserId = Convert.ToInt64(dr["UserId"]),
                            Username = Convert.ToString(dr["Username"]),
                            Name = Convert.ToString(dr["Name"]),
                            Email = Convert.ToString(dr["Email"]),
                            RoleType = (Role)Convert.ToInt64(dr["RoleType"]),
                            CondoId = (dr["CondoId"] is DBNull) ? 0 : Convert.ToInt64(dr["CondoId"]),
                            CondoName = Convert.ToString(dr["CondoName"]),
                            CondoEmail = Convert.ToString(dr["CondoEmail"]),
                            Contact = Convert.ToString(dr["Contact"]),
                            Address = Convert.ToString(dr["Address"]),
                            CondoPicture = Convert.ToString(dr["CondoPicture"])
                        });


                }
            }
        }
            return userCondos;  
        }

    public UserCondoData GetUserCondoDataById(Int64 Id)
    {
        UserCondoData obj = new UserCondoData();

        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "getCondoUserDatabyId",
                new SqlParameter("@UserCondoId", Id)))
            {
                while (dr.Read())
                {
                    obj.UserId = Convert.ToInt64(dr["UserId"]);
                    obj.UserCondoId = Convert.ToInt64(dr["UserCondoId"]);
                    obj.Name = Convert.ToString(dr["Name"]);
                    obj.Email = Convert.ToString(dr["Email"]);
                    obj.Username = Convert.ToString(dr["Username"]);
                    obj.RoleType = (Role)Convert.ToInt64(dr["RoleType"]);
                    obj.CondoId = Convert.ToInt64(dr["CondoId"]);
                    obj.CondoName = Convert.ToString(dr["CondoName"]);
                    obj.CondoEmail = Convert.ToString(dr["CondoEmail"]);
                    obj.Address = Convert.ToString(dr["Address"]);
                    obj.Contact = Convert.ToString(dr["Contact"]);
                    obj.CondoPicture = Convert.ToString(dr["CondoPicture"]);

                }

            }
        }
        return obj;
    }
    public bool UpdateUserCondoData(Int64 Id, UpdateUserCondoDbModel obj)
    {

        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            int Result = exec.ExecuteNonQuery(CommandType.StoredProcedure, "EditUserCondoData",
                new SqlParameter("@UserCondoId", Id),
                new SqlParameter("@UserId", obj.UserId),
                new SqlParameter("@CondoId", obj.CondoId)
                );
            return ReturnBool(Result);
        }

    }
    public List<AssignUserCondoData> GetUserId()
    {
        List<AssignUserCondoData> userCondo = new List<AssignUserCondoData>();

        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "getUserIdfromDb"))
            {
                while (dr.Read())
                {
                    userCondo.Add(new AssignUserCondoData
                    {
                        UserId = Convert.ToInt64(dr["UserId"]),
                        Name = Convert.ToString(dr["Name"]),

                    });
                }
            }
        }

        return userCondo;
    }

    public List<AssignUserCondoData> GetCondoId()
    {
        List<AssignUserCondoData> userCondo = new List<AssignUserCondoData>();

        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "getCondoIdfromDb"))
            {
                while (dr.Read())
                {
                    userCondo.Add(new AssignUserCondoData
                    {
                        CondoId = Convert.ToInt64(dr["CondoId"]),
                        CondoName = Convert.ToString(dr["CondoName"]),

                    });
                }
            }
        }

        return userCondo;
    }
    public bool AssignCondoToUsers(AssignUserCondoData obj)
    {

        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            int Result = exec.ExecuteNonQuery(CommandType.StoredProcedure, "AssignCondoToUser",
                new SqlParameter("@UserId", obj.UserId),
                new SqlParameter("@CondoId", obj.CondoId));
            return ReturnBool(Result);
        }


    }

    public CondoDbModel GetCondoDataById(Int64 id)
    {
        CondoDbModel condoDb = new CondoDbModel();
        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "GetAllCondoDetailById",
                new SqlParameter("@CondoId", id)
                ))
            {
                while (dr.Read())
                {
                    condoDb.CondoId = Convert.ToInt64(dr["CondoId"]);
                    condoDb.CondoName = Convert.ToString(dr["CondoName"]);
                    condoDb.CondoEmail = Convert.ToString(dr["CondoEmail"]);
                    condoDb.Contact = Convert.ToString(dr["Contact"]);
                    condoDb.Address = Convert.ToString(dr["Address"]);
                    condoDb.CondoPicture = Convert.ToString(dr["CondoPicture"]);


                }
            }
        }

        return condoDb;
    }

        public AssignUserCondoData GetCondo(Int64 id)
        {
            AssignUserCondoData condoDb = new AssignUserCondoData();
            using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
            {
                using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "GetAllCondoDetailById",
                    new SqlParameter("@CondoId", id)
                    ))
                {
                    while (dr.Read())
                    {
                        condoDb.CondoId = Convert.ToInt64(dr["CondoId"]);
                        condoDb.CondoName = Convert.ToString(dr["CondoName"]);
                        


                    }
                }
            }

            return condoDb;
        }
        public AssignUserCondoData getUserDataByid(Int64 id)
        {
            AssignUserCondoData assignUser=new AssignUserCondoData();   
            using(AdoExecution exec=new AdoExecution(SQLSuperAdmin.GetConnectionString()))
            {
                using(IDataReader dr=exec.ExecuteReader(CommandType.StoredProcedure, "getUserByidVikrant",
                    new SqlParameter("@UserId",id)
                    ))
                {
                    while(dr.Read())
                    {
                        assignUser.UserId = Convert.ToInt64(dr["UserId"]);
                        assignUser.Name= Convert.ToString(dr["Name"]);  

                    }
                }
            }
            return assignUser;  
        }
 
    public UserDbModel GetUserDataById(Int64 id)
    {
        UserDbModel userDbModel = new UserDbModel();
        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "GetUserCondoDataByIdToDetails",
                new SqlParameter("@UserId", id)))
            {
                while (dr.Read())
                {

                    userDbModel.UserId = Convert.ToInt64(dr["UserId"]);
                    userDbModel.Username = Convert.ToString(dr["Username"]);
                    userDbModel.Password = DecryptPassword(Convert.ToString(dr["Password"]));
                    userDbModel.ConfirmPassword = DecryptPassword(Convert.ToString(dr["Password"]));
                    userDbModel.Name = Convert.ToString(dr["Name"]);
                    userDbModel.Email = Convert.ToString(dr["Email"]);
                    userDbModel.RoleType = (CondoEntity.Role)Convert.ToInt64(dr["RoleType"]);
                    userDbModel.CondoName = Convert.ToString(dr["CondoName"]);
                    userDbModel.CondoPicture = Convert.ToString(dr["CondoPicture"]);
                }
            }


        }
        return userDbModel;
    }

    public UserDbModel GetUserProfileById(Int64 id)
    {
        UserDbModel userDbModel = new UserDbModel();
        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "getUserAdminProfile",
                new SqlParameter("@UserId", id)))
            {
                while (dr.Read())
                {

                    userDbModel.UserId = Convert.ToInt64(dr["UserId"]);
                    userDbModel.Username = Convert.ToString(dr["Username"]);
                        userDbModel.ConfirmPassword = DecryptPassword(Convert.ToString(dr["Password"]));
                    userDbModel.Password = DecryptPassword(Convert.ToString(dr["Password"]));
                    userDbModel.Name = Convert.ToString(dr["Name"]);
                    userDbModel.Email = Convert.ToString(dr["Email"]);
                    userDbModel.RoleType = (CondoEntity.Role)(Role)Convert.ToInt64(dr["RoleType"]);

                }
            }

        }
        return userDbModel;
    }


    public bool AddCondoData(CondoDbModel condoDb)
    {
        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            int Result = exec.ExecuteNonQuery(CommandType.StoredProcedure, "AddCondo",
               new SqlParameter("@CondoName", condoDb.CondoName),
               new SqlParameter("@CondoPicture", condoDb.CondoPicture),
               new SqlParameter("@CondoEmail", condoDb.CondoEmail),
               new SqlParameter("@Address", condoDb.Address),
               new SqlParameter("@Contact", condoDb.Contact));

            return ReturnBool(Result);

        }
    }
   
        public List<UserCondoData> GetAllUserCondoList()
        {
            List<UserCondoData> userCondoDatas= new List<UserCondoData>();  
            using(AdoExecution exec=new AdoExecution(SQLSuperAdmin.GetConnectionString())) 
            {
                using(IDataReader dr=exec.ExecuteReader(CommandType.StoredProcedure, "getCondoUserData"))
                {
                    while (dr.Read()) 
                    {
                        userCondoDatas.Add(new UserCondoData
                        {
                           
                            UserId = (dr["UserId"] is DBNull) ? 0 : Convert.ToInt64(dr["UserId"]),
                            Username = Convert.ToString(dr["Username"]),
                            Name = Convert.ToString(dr["Name"]),
                            Email = Convert.ToString(dr["Email"]),
                            
                            CondoId = Convert.ToInt64(dr["CondoId"]),
                            CondoName = Convert.ToString(dr["CondoName"]),
                            CondoEmail = Convert.ToString(dr["CondoEmail"]),
                            Contact = Convert.ToString(dr["Contact"]),
                            Address = Convert.ToString(dr["Address"]),
                            CondoPicture = Convert.ToString(dr["CondoPicture"])

                        });
                    }
                }
            }
           return userCondoDatas;
        }
        
   public List<UserCondoData> GetAllUsers()
    {
        List<UserCondoData> userDbModels = new List<UserCondoData>();
        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "GetAllUsers"))
            {
                while (dr.Read())
                {
                    userDbModels.Add(new UserCondoData
                    {
                        UserId = Convert.ToInt64(dr["UserId"]),
                        Name = Convert.ToString(dr["Name"]),
                        Email = Convert.ToString(dr["Email"]),
                        Username = Convert.ToString(dr["Username"]),
                        RoleType = (Role)Convert.ToInt64(dr["RoleType"]),

                    });
                }
            }
        }
        return userDbModels;
    }
    public List<UserCondoData> GetAllCondo()
    {
        List<UserCondoData> condoDbModels = new List<UserCondoData>();
        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "GetAllCondo"))
            {
                while (dr.Read())
                {
                    condoDbModels.Add(new UserCondoData
                    {
                        CondoId = Convert.ToInt64(dr["CondoId"]),
                        CondoName = Convert.ToString(dr["CondoName"]),
                        Name = Convert.ToString(dr["Name"]),
                        CondoEmail = Convert.ToString(dr["CondoEmail"]),
                        Contact = Convert.ToString(dr["Contact"]),
                        Address = Convert.ToString(dr["Address"]),
                        CondoPicture = Convert.ToString(dr["CondoPicture"])

                    });

                }
            }
        }
        return condoDbModels;
    }

    public bool AddUserData(UserDbModel userDb)
    {

        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            int Result = exec.ExecuteNonQuery(CommandType.StoredProcedure, "AddUsers",
                new SqlParameter("@Name", userDb.Name),
                new SqlParameter("@Email", userDb.Email),
                new SqlParameter("@Username", userDb.Username),
                new SqlParameter("@Password", EncryptPassword(userDb.ConfirmPassword)),
                new SqlParameter("@RoleType",2)

                );

            return ReturnBool(Result);
        }
    }



    public bool DeleteUsers(Int64 id)
    {
        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            int Result = exec.ExecuteNonQuery(CommandType.StoredProcedure, "DeleteUserById",
                new SqlParameter("@UserId", id));
            return ReturnBool(Result);
        }
    }
    public bool DeleteCondo(Int64 id)
    {
        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            int Result = exec.ExecuteNonQuery(CommandType.StoredProcedure, "DeleteCondo",
                new SqlParameter("@CondoId", id));
            return ReturnBool(Result);
        }

    }

    public bool EditCondo(Int64 Id, CondoDbModel obj)
    {
        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            int Result = exec.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateCondoBySuperAdmin",
                new SqlParameter("@CondoId", Id),
                new SqlParameter("@CondoName", obj.CondoName),
                new SqlParameter("@CondoEmail", obj.CondoEmail),
                new SqlParameter("@Contact", obj.Contact),
                new SqlParameter("@Address", obj.Address),
                new SqlParameter("@CondoPicture", obj.CondoPicture));
            return ReturnBool(Result);
        }
    }



    public bool EditUserDetail(Int64 Id, UpdateAdminData obj)
    {
        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            int Result = exec.ExecuteNonQuery(CommandType.StoredProcedure, "EditUserDataBySuperAdmin",
                new SqlParameter("@UserId", Id),
                new SqlParameter("@Name", obj.Name),
                new SqlParameter("@Username", obj.Username),
                new SqlParameter("@Email", obj.Email),
                new SqlParameter("@RoleType",2),
                new SqlParameter("@Password", EncryptPassword(obj.ConfirmPassword))
                );
            return ReturnBool(Result);
        }
    }
    public LoginSuperAdminDbModel LoginSuperAdminCMS(string Username, string EncrytypePassword)
    {
        string abc = EncryptPassword(EncrytypePassword);
        LoginSuperAdminDbModel superAdminDbModel = new LoginSuperAdminDbModel();
        using (AdoExecution exec = new AdoExecution(SQLSuperAdmin.GetConnectionString()))
        {
            using (IDataReader dr = exec.ExecuteReader(CommandType.StoredProcedure, "SignInSuperAdmin",
                new SqlParameter("@Username", Username),
                new SqlParameter("@Password", EncryptPassword(EncrytypePassword))
                ))
            {
                while (dr.Read())
                {
                    superAdminDbModel.UserId = Convert.ToInt64(dr["UserId"]);
                    superAdminDbModel.Username = Convert.ToString(dr["Username"]);
                    superAdminDbModel.Password = EncryptPassword(Convert.ToString(dr["Password"]));
                    superAdminDbModel.Email = Convert.ToString(dr["Email"]);
                    superAdminDbModel.Name = Convert.ToString(dr["Name"]);
                    superAdminDbModel.RoleType = Convert.ToInt32(dr["RoleType"]);
                }
                return superAdminDbModel;
            }
        }
    }





    private static bool ReturnBool(int Result)
    {
        return Result > 0;
    }


}






}
