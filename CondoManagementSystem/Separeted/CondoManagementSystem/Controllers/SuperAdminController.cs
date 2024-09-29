using CondoBll;
using CondoEntity;
using SuperAdminCondoBll;
using SuperCondoEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CondoManagementSystem.Controllers
{
    public class SuperAdminController : Controller
    {

        SuperAdminBll _superAdminBll = new SuperAdminBll();

        // Index
        //-----------------------------------------------------------------------------------------
        public ActionResult UserIndex()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                return View(_superAdminBll.UserCondoList().OrderByDescending(userid=>userid.UserId));
            }
        }

        public ActionResult CondoIndex()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                return View(_superAdminBll.GetAllUserCondoList().OrderByDescending(condoid=>condoid.CondoId));
            }
        }

        //-------------------------------------------------------------------------------------------------
        //Add Method

        public ActionResult AddUser()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddUser(UserDbModel userDb)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _superAdminBll.AddUserData(userDb);
                    TempData["UserAddMessage"] = "Add user successfuly ";
                    return RedirectToAction("UserIndex", "SuperAdmin");
                }
                else
                {
                    return View();
                }
            }
        }

        //---------------------------------------------------------------------------------------------------
        public ActionResult AssignCondoUser(Int64 id)
        {
           if(Session==null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
           else
            {
                ViewBag.itemlistData = new SelectList(_superAdminBll.GetCondoId().OrderBy(e => e.CondoId), "CondoId", "CondoName");
                
                return View(_superAdminBll.getUserDataByid(id));
            }
        }

        [HttpPost]
        public ActionResult AssignCondoUser(AssignUserCondoData obj)
        {
            if (Session == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {

                if (ModelState.IsValid)
                {
                    AssignUserCondoData userCondoData = new AssignUserCondoData()
                    {
                        UserId = obj.UserId,
                        CondoId = obj.CondoId
                    };
                    _superAdminBll.AssignCondoToUsers(userCondoData);
                    TempData["AssignCondo"] = "Assign condo to user successfuly";
                    return RedirectToAction("UserIndex", "SuperAdmin");
                }
                else
                {
                    ViewBag.itemlist = new SelectList(_superAdminBll.GetUserId().OrderBy(e => e.UserId), "UserId", "Name");
                    ViewBag.itemlistData = new SelectList(_superAdminBll.GetCondoId().OrderBy(e => e.CondoId), "CondoId", "CondoName");
                    return View();
                }
            }

        }


        public ActionResult AssignUserCondo(Int64 id)
        {
            if (Session == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                ViewBag.itemlistData = new SelectList(_superAdminBll.GetUserId().OrderBy(e => e.UserId), "UserId", "Name");

                return View(_superAdminBll.GetCondo(id));
            }
        }

        [HttpPost]
        public ActionResult AssignUserCondo(AssignUserCondoData obj)
        {
            if (Session == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {

                if (ModelState.IsValid)
                {
                    AssignUserCondoData userCondoData = new AssignUserCondoData()
                    {
                        UserId = obj.UserId,
                        CondoId = obj.CondoId
                    };
                    _superAdminBll.AssignCondoToUsers(userCondoData);
                    TempData["AssignCondo"] = "Assign condo to user successfuly";
                    return RedirectToAction("CondoIndex", "SuperAdmin");
                }
                else
                {
                    ViewBag.itemlist = new SelectList(_superAdminBll.GetUserId().OrderBy(e => e.UserId), "UserId", "Name");
                    ViewBag.itemlistData = new SelectList(_superAdminBll.GetCondoId().OrderBy(e => e.CondoId), "CondoId", "CondoName");
                    return View();
                }
            }

        }

        public ActionResult UpdateCondoUserData(Int64 Id)
        {
            
            ViewBag.itemlist = new SelectList(_superAdminBll.GetUserId().OrderBy(e => e.UserId), "UserId", "Name");
            ViewBag.itemlistData = new SelectList(_superAdminBll.GetCondoId().OrderBy(e => e.CondoId), "CondoId", "CondoName");
            return View(_superAdminBll.GetUserCondoDataById(Id));
        }

        [HttpPost]
        public ActionResult UpdateCondoUserData(Int64 Id,UpdateUserCondoDbModel obj)
        {
            if(ModelState.IsValid)
            {
                _superAdminBll.UpdateUserCondoData(Id,obj);
                TempData["UpdateUserCondo"] = "Update admin condo details successfuly";
                return RedirectToAction("UserCondoIndex","SuperAdmin");
            }
            else
            {
                return View();
            }
        }

//----------------------------------------------------------------------------------------------------
      

        
        public ActionResult AddCondoData()
        {
            if (Session == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCondoData(CondoDbModel condoDb,HttpPostedFileBase CondoPicture)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (CondoPicture != null)
                    {
                        string mapPath = Server.MapPath("/Images");
                        Guid guid = Guid.NewGuid();
                        string fileExtention = Path.GetExtension(CondoPicture.FileName);
                        string FullImageName = guid.ToString() + fileExtention;
                        
                            string fullPath = Path.Combine(mapPath, FullImageName);
                            CondoPicture.SaveAs(fullPath);
                            condoDb.CondoPicture = FullImageName;
                        
                       
                    }
                }


                _superAdminBll.AddCondoData(condoDb);
                TempData["AddCondoMessage"] = "Add condo successfuly ";
                return RedirectToAction("CondoIndex", "SuperAdmin");
            }
              
        }
       
        

        //---------------------------------------------------------------------------------------------------------
        //Edit Methods------------------------------------------------------------------------------
        public ActionResult EditUserData(Int64 UserId)
        {
            if (Session == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                var row = _superAdminBll.GetUserProfileById(UserId);
                return View(row);
            }
        }
        [HttpPost]
        public ActionResult EditUserData(Int64 UserId, UpdateAdminData obj)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _superAdminBll.EditUserDetail(UserId, obj);
                    TempData["UserUpdateMessage"] = "Update user profile successfuly ";
                    return RedirectToAction("UserIndex", "SuperAdmin");
                }
                else
                {
                    return View(obj);
                }
            }
        }

        public ActionResult EditCondoData(Int64 id)
        {
            if (Session == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                var row = _superAdminBll.GetCondoDataById(id);
                return View(row);
            }
        }
        [HttpPost]

        public ActionResult EditCondoData(Int64 id, CondoDbModel obj,HttpPostedFileBase CondoPicture)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (CondoPicture != null)
                    {
                        string mapPath = Server.MapPath("/Images");
                        Guid guid = Guid.NewGuid();
                        string fileExtention = Path.GetExtension(CondoPicture.FileName);
                        string FullImageName = guid.ToString() + fileExtention;
                        if (fileExtention == ".jpg" || fileExtention == ".png" || fileExtention == ".jfif")
                        {
                            string fullPath = Path.Combine(mapPath, FullImageName);
                            CondoPicture.SaveAs(fullPath);
                            obj.CondoPicture = FullImageName;
                        }
                        else
                        {
                            return View();
                        }
                    }
                    
                    _superAdminBll.EditCondo(id,obj);
                    TempData["UpdateCondoMessage"] = "Update condo successfuly ";

                    return RedirectToAction("CondoIndex", "SuperAdmin");
                }
                else
                {
                    return View();
                }
            }
        }



//------------------------------------------------------------------------------------------------------------
//validation Method

        [AcceptVerbs("Get", "Post")]
        public JsonResult IsEmailAvailable([Bind(Prefix = "Email")]string Email, string initialEmail)
        {
            if (initialEmail != "")
            {
                if (Email.ToLower() == initialEmail.ToLower())
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }

            var result = _superAdminBll.GetAllUsers().FirstOrDefault(a => a.Email.ToLower() == Email.ToLower());
            if (result == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs("Get", "Post")]
        public JsonResult IsCondoEmailAvailable([Bind(Prefix = "CondoEmail")] string CondoEmail, string initialEmail)
        {
            if (initialEmail !="")
            {
                if (CondoEmail.ToLower() == initialEmail.ToLower())
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }

            var result = _superAdminBll.GetAllCondo().FirstOrDefault(a => a.CondoEmail.ToLower() == CondoEmail.ToLower());
            if (result == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs("Get", "Post")]
        public JsonResult IsUsernameAvailable([Bind(Prefix = "Username")] string Username, string initialUsername)
        {
            if (initialUsername != "" && initialUsername != null)
            {
                if (Username.ToLower() == initialUsername.ToLower())
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }

            var result = _superAdminBll.GetAllUsers().FirstOrDefault(a => a.Username.ToLower() == Username.ToLower());
            if (result == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        

        //----------------------------------------------------------------------------------------------------
        //Admin Dashboard

      
        
        public ActionResult SuperAdminDashboard()
        {


            if (Session["Username"] == null)
            {

                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {

                return View();
            }


        }

//---------------------------------------------------------------------------------------------------------------
//Login Super Admin Method

      
        public ActionResult SuperAdminLogin()
        {
            return View();
        }
       
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuperAdminLogin(LoginSuperAdminDbModel dbModel)
        {
            if (ModelState.IsValid)
            {
                var cmsEntity = _superAdminBll.LoginSuperAdminCMS(dbModel.Username, dbModel.Password);

                if (cmsEntity == null || cmsEntity.Username == null || dbModel.Password == null)
                {
                    ModelState.AddModelError("", "Your username or password is incorrect.");
                }
                else
                {
                    if (dbModel.Rememberme == true)
                    {
                        HttpCookie condoCMS = new HttpCookie("condoCMS");
                        condoCMS["UserId"] = Convert.ToString(cmsEntity.UserId);
                        condoCMS["Name"] = Convert.ToString(cmsEntity.Name);
                        condoCMS["Username"] = Convert.ToString(cmsEntity.Username);
                        condoCMS["RoleType"] = Convert.ToString(cmsEntity.RoleType);
                        condoCMS.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(condoCMS);
                    }
                    Session["UserId"] = cmsEntity.UserId;
                    Session["Username"] = Convert.ToString(cmsEntity.Username);
                    Session["Email"] = Convert.ToString(cmsEntity.Email);
                    Session["Name"] = Convert.ToString(cmsEntity.Name);
                    Session["RoleType"] = Convert.ToInt64(cmsEntity.RoleType);
                    return RedirectToAction("SuperAdminDashboard", "SuperAdmin");
                }
            }

            return View();
        }
        //----------------------------------------------------------------------------------------------------------
        //Edit Password

       
        
        public ActionResult SuperAdminEditPassword()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                return View();
            }
        }

        
        
        [HttpPost]
        public ActionResult SuperAdminEditPassword(string username, string Password, ChangePasswordDbModel obj)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    username = Convert.ToString(Session["Username"]);
                    if (!_superAdminBll.UpdateUserPassword(username, Password, obj))
                    {
                        ModelState.AddModelError("", "Incorect old password");
                        return View();
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "Change your password successfuly";
                        return RedirectToAction("SuperAdminDashboard", "SuperAdmin");
                    }

                }
                else
                {
                    return View();
                }
            }

        }

        //----------------------------------------------------------------------------------------------------------
        //Details Method 

       
        [HttpGet]
        public ActionResult UserCondoDetail(Int64 id)
        {
            if (Session["Username"]==null) 
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                return View(_superAdminBll.GetUserCondoDataById(id));
            }
        }

      
        [HttpGet]
        public ActionResult SuperAdminProfile(Int64 userId)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SuperAdminDashboard", "SuperAdmin");
            }
            else
            {
              
                var row = _superAdminBll.GetUserProfileById(Convert.ToInt64(userId));
                return View(row);
            }
        }


        [HttpGet]
        public ActionResult AdminDetails(Int64 userId)
        {
            if (Session["Username"]==null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                var row = _superAdminBll.GetUserDataById(Convert.ToInt64(userId));
                return View(row);
            }
        }


        public ActionResult CondoDetails(Int64 CondoId)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SuperAdminDashboard", "SuperAdmin");
            }
            else
            {

                var row = _superAdminBll.GetCondoDataById(CondoId);
                return View(row);
            }
        }
        //---------------------------------------------------------------------------------------------------
       //Delete Condo
            
        public ActionResult CondoUserData(Int64 Id)
        {
            if (Session["Username"]==null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                _superAdminBll.DeleteUserCondoData(Id);
                TempData["DeleteCondoUserMsg"] = "Delete user condo data successfuly.";
                return RedirectToAction("UserCondoIndex", "SuperAdmin");
            }
        }

        public ActionResult DeleteCondosData(Int64 CondoId)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                _superAdminBll.DeleteCondo(CondoId);
                TempData["DeleteCondoMessage"] = "Delete condo successfuly ";
                return RedirectToAction("CondoIndex", "SuperAdmin");
            }
        }

        public ActionResult DeleteUserData(Int64 UserId)
        {
            if (Session == null)
            {
                return RedirectToAction("SuperAdminLogin", "SuperAdmin");
            }
            else
            {
                _superAdminBll.DeleteUsers(UserId);
                TempData["DeleteUserMessage"] = "Delete user successfuly ";
                return RedirectToAction("UserIndex", "SuperAdmin");
            }

        }
//------------------------------------------------------------------------------------------------------------
//Logout Super Admin Method
    
        
        public ActionResult LogoutSuperAdmin()
        {
            Session.Clear();
            return RedirectToAction("SuperAdminLogin", "SuperAdmin");
        }
    }
}