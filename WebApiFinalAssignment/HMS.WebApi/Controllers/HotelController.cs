using HMS.BAL.Interface;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HMS.WebApi.Controllers
{
    [BasicAuthenticationClasses.BasicAuthentication]
    [Authorize]
    [EnableCors(origins: "*", headers:"*",methods:"*")]
    public class HotelController : ApiController
    {
        private readonly IHotelManager _hotelManager;

        public HotelController(IHotelManager hotelManager)
        {
            _hotelManager = hotelManager;
        }

        // GET: api/Hotel
        [Route("api/Hotel")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_hotelManager.GetAllHotels());
        }        

        // POST: api/Hotel
        [Route("api/Hotel/Create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody]Hotel hotel)
        {
            return Ok(_hotelManager.Create(hotel));
        }

        
    }
}
