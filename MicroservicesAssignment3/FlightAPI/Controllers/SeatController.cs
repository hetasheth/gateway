using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightEntities.Entities;
using FlightAPI.Services;

namespace FlightAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ILogger<SeatController> _logger;

        public SeatController(ILogger<SeatController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get seats
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSeats")]
        public IEnumerable<Seat> Get()
        {
            return new SeatService().GetSeats();
        }
    }
}
