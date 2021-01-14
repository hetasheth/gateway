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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookingController : ApiController
    {
        private readonly IBookingManager _bookingManager;

        public BookingController(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }

        
        // POST: api/Booking
        [Route("api/Booking/Create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody]Booking book)
        {
            return Ok(_bookingManager.Create(book));
        }

        // PUT: api/Booking/5
        [Route("api/Booking/UpdateStatus")]
        [HttpPut]
        public IHttpActionResult UpdateStatus([FromBody]Booking book)
        {
            return Ok(_bookingManager.UpdateStatus(book));
        }

        [Route("api/Booking/UpdateDate")]
        [HttpPut]
        public IHttpActionResult UpdateDate([FromBody]Booking book)
        {
            return Ok(_bookingManager.UpdateBookingDate(book));
        }

        // DELETE: api/Booking/5
        public IHttpActionResult Delete(int id)
        {
            return Ok(_bookingManager.Delete(id));
        }
    }
}
