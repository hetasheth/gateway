using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalApp.Models;
using log4net;

namespace FinalApp.Controllers
{
    public class UserController : Controller
    {
        GatewayUserDBEntities db = new GatewayUserDBEntities();
        ILog log = log4net.LogManager.GetLogger(typeof(UserController));

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterModel usr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tblUser nu = new tblUser();
                    nu.UserID = 0;
                    nu.FirstName = usr.FirstName;
                    nu.LastName = usr.LastName;
                    nu.EmailID = usr.EmailID;
                    nu.PhoneNumber = usr.PhoneNumber;
                    nu.DOB = usr.DOB;
                    nu.Gender = usr.Gender;
                    nu.Address = usr.Address;
                    nu.City = usr.City;
                    nu.State = usr.State;
                    nu.Pincode = usr.Pincode;                    
                    string img = Path.GetFileName(usr.Photo.FileName);
                    usr.Photo.SaveAs(Server.MapPath("~/Images/" + img));
                    nu.Photo = img;
                    nu.Password = usr.Password;
                    db.tblUsers.Add(nu);
                    db.SaveChanges();
                    log.Info("Registration Successful...");
                    return View("Login");
                }
                else
                {
                    log.Info("ModelState Invalid...");
                    return View("Registration");
                }
            }
            catch(Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Registration");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(LoginModel usr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tblUser user = db.tblUsers.Where(x => x.EmailID.Equals(usr.EmailID) && x.Password.Equals(usr.Password)).FirstOrDefault();
                    if (user != null)
                    {
                        Session["uid"] = user.UserID;
                        log.Info("Login Successful...");
                        return View("Index", user);
                    }
                    else
                    {
                        ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                        log.Info("Login Fail...");
                        return View("Login");
                    }
                }
                else
                {
                    log.Info("ModelState Invalid...");
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception : "+ex);
                return View();
            }
        }

        public ActionResult Index()
        {
            if (Session["uid"] != null)
                return View();
            else
                return View("Login");
        }

        public ActionResult Logout()
        {
            Session["uid"]=null;
            return View("Login");
        }
    }
}