using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonObject.Models;
using FlightAPI.Services;

namespace FlightAPI.Controllers
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
        /// Get seats
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSeats")]
        public IEnumerable<DropdownDto> GetSeats()
        {
            return new SeatService().GetSeats().Select(r => new DropdownDto { Id = r.Id, Name = r.SeatNumber });
        }

        /// <summary>
        ///  Get flights
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetFlights")]
        public IEnumerable<DropdownDto> GetFlights()
        {
            return new FlightService().GetFlights().Select(r => new DropdownDto { Id = r.Id, Name = r.FlightNo });
        }
    }
}
