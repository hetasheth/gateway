using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using PMSWebApi.Models;

namespace PMSWebApi.Controllers
{
    public class UserAPIController : ApiController
    {
        PMSystemDBEntities dbContext = new PMSystemDBEntities();
        ILog log = log4net.LogManager.GetLogger(typeof(UserAPIController));

        [Route("api/UserAPI/LoginUser")]
        [HttpPost]
        public IHttpActionResult LoginUser(User user)
        {
            try
            {
                var usr = dbContext.Users.Where(x => x.EmailID.Equals(user.EmailID) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (usr != null)
                {
                    return Ok(usr);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return NotFound();
            }

        }
    }
}
