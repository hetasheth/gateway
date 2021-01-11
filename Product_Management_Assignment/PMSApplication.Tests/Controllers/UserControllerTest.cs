using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMSApplication;
using PMSApplication.Controllers;
using PMSApplication.Models;

namespace PMSApplication.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        UserController controller = new UserController();

        [TestMethod]
        public void Dashboard()
        {
            controller.Dashboard();
        }

        [TestMethod]
        public void Login()
        {
            controller.Login();
        }

        [TestMethod]
        public void LoginUser()
        {
            User usr = new User { UserName = "Admin", EmailID = "admin@gmail.com", Password = "Admin*1234" };
            controller.LoginUser(usr);
        }

        [TestMethod]
        public void LogOff()
        {
            controller.LogOff();
        }
    }
}
