using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightEntities.Entities;
using FlightAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;

        public FlightController(ILogger<FlightController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get flights
        /// </summary>
        /// <returns></returns>        
        [HttpGet, Route("GetFlights")]
        public IEnumerable<Flight> Get()
        {
            return new FlightService().GetFlights();
        }

        /// <summary>
        /// Add flight
        /// </summary>
        /// <param name="dto">request dto</param>
        /// <returns>response dto</returns>
        [HttpGet, Route("GetSeatOption/{id}")]
        public FlightSeatOptions GetFlightSeatOptions(long id)
        {
            return new FlightService().GetSeatOptions(id: id);
        }
    }
}
