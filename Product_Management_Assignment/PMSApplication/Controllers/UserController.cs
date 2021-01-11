using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using log4net;
using PMSApplication.Models;

namespace PMSApplication.Controllers
{
    public class UserController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(UserController));

        HttpClient client;
        Uri baseAddress = new Uri("http://localhost:22128/api");

        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = client.PostAsJsonAsync<User>(client.BaseAddress + "/UserAPI/LoginUser", user).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        User newUser = response.Content.ReadAsAsync<User>().Result;
                        Session["uname"] = newUser.UserName;
                        return RedirectToAction("Dashboard", "User");
                    }
                    else
                    {
                        TempData["invalidLogin"] = "Invalid Username or Password!!!";
                        return RedirectToAction("Login");
                    }
                    
                }
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Login");
            }
        }
                
        public ActionResult LogOff()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "User");
        }
    }
}