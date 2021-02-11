using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingEntities.Entities;
using BookingAPI.Services;
using Microsoft.Extensions.Logging;

namespace BookingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;

        public BookingController(ILogger<BookingController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get bookings
        /// </summary>
        /// <returns></returns>      
        [HttpGet]
        public IEnumerable<Booking> GetBookings()
        {
            return new BookingService().GetBookings();
        }

        /// <summary>
        /// Add booking
        /// </summary>
        /// <param name="dto">request dto</param>
        /// <returns>response dto</returns>
        [HttpGet, Route("GetBooking/{id}")]
        public IEnumerable<Booking> GetBooking(long id)
        {
            return new BookingService().GetBookings(id: id);
        }
    }
}
