using CondoBll;
using CondoDll.DataAccess;
using CondoDll.Interface;
using CondoEntity;
using SuperAdminCondoBll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
namespace CondoManagementSystem.Controllers
{
 
    public class AdminController : Controller
    {
        AdminBll adminBll = new AdminBll();


        // GET: Admin 

        public ActionResult Index()
        {
            return View();
        }

        
       
        public ActionResult AdminDashboard()
        {


            if (Session["Username"] == null)
            {
                
                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
               
                return View(); 
            }
      
             
        }

      
        public ActionResult EditPassword()
        {
            if (Session["Username"]==null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult EditPassword(string username,string Password,ChangePasswordDbModel obj)
        {
            if (Session["Username"]==null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    username = Convert.ToString(Session["Username"]);
                    if (!adminBll.UpdateUserPassword(username,Password,obj))
                    {
                        ModelState.AddModelError("","Incorect old password");
                        return View();
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "Change your password Successfuly";
                        return RedirectToAction("AdminDashboard", "Admin");
                    }

                }
                else
                {
                    return View();
                }
            }

        }
        public ActionResult EditCondo(Int64 condoId)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                EditUserCondoEntity userCondoData = adminBll.GetAllCondoData(condoId);
                

                return View(userCondoData);
            }
        }

        [HttpPost]
        public ActionResult EditCondo(Int64 condoId, HttpPostedFileBase CondoPicture, EditUserCondoEntity obj)
        {

            if (Session["Username"] == null)
            {
                return RedirectToAction("AdminLogin", "Admin");
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
                    }
                    adminBll.EditCondoData(condoId, CondoPicture, obj);
                    TempData["UpdateCondoMessage"] = "Update condo successfuly ";

                    return RedirectToAction("CondoInformation", "Admin");


                }
                else
                {
                    return View();
                }
            }
           
            
        }
        public ActionResult AdminProfile()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                var row = adminBll.GetUserData(Convert.ToInt64(Session["UserId"]));
                return View(row);
            }
        }

        public ActionResult CondoInformation()
        {
            if (Session["Username"]==null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                var temp = Convert.ToInt64(Session["UserId"]);
                UserCondoData condoData =adminBll.CondoData(Convert.ToInt64(temp));
                return View(condoData);
            }
        }
        
        public ActionResult Logout()
        {
            Session.Clear();
           return RedirectToAction("AdminLogin","Admin");
        }
       
        public ActionResult UpdateAdmin()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                var row = adminBll.GetUserData(Convert.ToInt64(Session["UserId"]));
                return View(row);
            }
        }
        [HttpPost]
        public ActionResult UpdateAdmin(Int64 userId,EditUserDataEntity obj)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
               if(ModelState.IsValid)
                {
                      userId = Convert.ToInt64(Session["UserId"]);
                    adminBll.EditUserDetails(userId,obj);
                    TempData["UpdateAdminMessage"] = "Update admin profile successfuly";
                    return RedirectToAction("AdminProfile", "Admin");
                }
               else
                {
                    return View();
                }
            }
           
        }
        
        public ActionResult AdminLogin() 
        {
                return View();    
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(CMSLoginView model)
        {
            if (ModelState.IsValid)
            {
                var cmsEntity = adminBll.LoginCMS(model.Username, model.Password);
               
                if (cmsEntity == null || cmsEntity.Username == null || model.Password == null)
                {
                    ModelState.AddModelError("", "Your username or password is incorrect.");
                }
                else
                {
                    if (model.Rememberme == true)
                    {
                        HttpCookie condoCMS = new HttpCookie("condoCMS");
                        condoCMS["Name"] = cmsEntity.Name;                      
                        condoCMS["UserId"] = Convert.ToString(cmsEntity.UserId);
                        condoCMS["Email"] = cmsEntity.EmailAddress;
                        condoCMS["Username"] = cmsEntity.Username;
                        condoCMS["RoleType"] = Convert.ToString(cmsEntity.RoleType);
                        condoCMS.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(condoCMS);
                    }

                    Session["UserCondoId"] = cmsEntity.UserCondoId;
                    Session["Name"] = cmsEntity.Name;
                    Session["CondoId"] = Convert.ToInt64(cmsEntity.CondoId);
                    Session["RoleType"] = Convert.ToString(cmsEntity.RoleType);
                    Session["Email"] = cmsEntity.EmailAddress;
                    Session["Username"] = cmsEntity.Username;
                    Session["UserId"] = cmsEntity.UserId;
                    Session["Password"]= cmsEntity.Password;
                    return RedirectToAction("AdminDashboard", "Admin");
                }
            }
            return View();
        }

        
    }
}