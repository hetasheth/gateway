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
    //[Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoomController : ApiController
    {
        private readonly IRoomManager _roomManager;

        public RoomController(IRoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        // GET: api/Room
        [Route("api/Room/GetRooms")]
        [HttpGet]
        public IHttpActionResult GetRooms(string City="", string Pincode="", string Category="", decimal? Price=null)
        {
            return Ok(_roomManager.GetRooms(City,Pincode,Category,Price));
        }

        // GET: api/Room/5
        [Route("api/Room/Create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody]Room room)
        {
            return Ok(_roomManager.Create(room));
        }

        [Route("api/Room/CheckAvailability")]
        [HttpGet]
        public IHttpActionResult CheckAvailability(int RoomID = 0, DateTime? BookDate=null)
        {
            return Ok(_roomManager.CheckRoomAvailability(RoomID, BookDate));
        }

    }
}
