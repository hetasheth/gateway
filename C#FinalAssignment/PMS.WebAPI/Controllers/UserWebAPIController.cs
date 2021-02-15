using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PMS.BAL.Interface;
using PMS.Models;
using log4net;

namespace PMS.WebAPI.Controllers
{
    public class UserWebAPIController : ApiController
    {
        ILog log = log4net.LogManager.GetLogger(typeof(UserWebAPIController));
        private readonly IUserManager _userManager;

        public UserWebAPIController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [Route("api/UserAPI/LoginUser")]
        [HttpPost]
        public IHttpActionResult LoginUser(UserLogin userLogin)
        {
            try
            {
                return Ok(_userManager.Login(userLogin));
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return NotFound();
            }

        }
    }
}
