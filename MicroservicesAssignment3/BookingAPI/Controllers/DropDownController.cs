using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonObject.Models;
using BookingAPI.Services;

namespace BookingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DropDownController : ControllerBase
    {
        private readonly ILogger<DropDownController> _logger;

        public DropDownController(ILogger<DropDownController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get bookings
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetBookings")]
        public IEnumerable<DropdownDto> GetBookings()
        {
            return new BookingService().GetBookings().Select(r => new DropdownDto { Id = r.Id, Name = r.Number });
        }
    }
}
