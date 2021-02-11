using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonObject.Models;
using Microsoft.Extensions.Logging;
using Web.Gateway.Util;

namespace Web.Gateway.Controllers
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
        ///  Get flights
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetFlights")]
        public async Task<IEnumerable<DropdownDto>> GetFlights()
        {
            var flights = await HttpCall.GetRequest<List<DropdownDto>>("https://localhost:44300/DropDown/GetFlights");
            var seats = await HttpCall.GetRequest<List<DropdownDto>>("https://localhost:44300/DropDown/GetSeats");
            flights.AddRange(seats);
            return flights;
        }

        /// <summary>
        ///  Get bookings
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetBookings")]
        public async Task<IEnumerable<DropdownDto>> GetBookings()
        {
            var bookings = await HttpCall.GetRequest<List<DropdownDto>>("https://localhost:44382/DropDown/GetBookings");
            return bookings;
        }
    }
}
