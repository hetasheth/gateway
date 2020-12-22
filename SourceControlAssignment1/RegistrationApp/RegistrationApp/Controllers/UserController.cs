using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegistrationApp.Models;

namespace RegistrationApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addUser(Users u)
        {
            return View("Registration");
        }
    }
}