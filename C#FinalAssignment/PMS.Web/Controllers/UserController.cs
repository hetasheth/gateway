using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PMS.Models;
using PMS.Web.Models;
using log4net;
using AutoMapper;

namespace PMS.Web.Controllers
{
    public class UserController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(UserController));

        HttpClient client;
        Uri baseAddress = new Uri("http://localhost:27170/api");

        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(LoginVM loginVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<LoginVM, UserLogin>());
                    var mapper = new Mapper(config);
                    UserLogin user = mapper.Map<UserLogin>(loginVM);
                    HttpResponseMessage response = client.PostAsJsonAsync<UserLogin>(client.BaseAddress + "/UserAPI/LoginUser", user).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        User newUser = response.Content.ReadAsAsync<User>().Result;
                        if (newUser.EmailID != null)
                        {
                            Session["uname"] = newUser.UserName;
                            return RedirectToAction("Dashboard", "User");
                        }
                        else
                        {
                            TempData["invalidLogin"] = "Invalid Username or Password!!!";
                            return RedirectToAction("Login");
                        }
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