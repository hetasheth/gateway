using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMSWebApi;
using PMSWebApi.Controllers;
using PMSWebApi.Models;

namespace PMSWebApi.Tests.Controllers
{
    [TestClass]
    public class UserAPIControllerTest
    {
        [TestMethod]
        public void LoginUser()
        {
            UserAPIController controller = new UserAPIController();
            User usr = new User { UserID = 1, UserName = "Admin", EmailID = "admin@gmail.com", Password = "Admin*1234" };
            controller.LoginUser(usr);
        }
    }
}
