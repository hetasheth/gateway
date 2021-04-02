using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestingWebAPI.Repository.Interface;
using TestingWebAPI.Models;

namespace TestingWebAPI.Controllers
{
    public class PassengerController : ApiController
    {
        private readonly IPassengerRepository _passengerRepository;

        public PassengerController(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        [HttpGet]
        [Route("api/Passenger/GetPassengers")]
        public IList<Passenger> GetPassengers()
        {
            return _passengerRepository.GetPassengers().ToList();
        }

        [HttpGet]
        [Route("api/Passenger/GetPassengerById")]
        public IHttpActionResult GetPassengerById(String id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var passenger = _passengerRepository.GetPassengerById(id);
                    if (passenger.FirstName != null)
                    {
                        return Ok(passenger);
                    }
                    else
                    {
                        return InternalServerError();
                    }
                }
                return BadRequest("Invalid passenger id");
            }
            catch (Exception ex)
            {                
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("api/Passenger/RegisterPassenger")]
        public IHttpActionResult RegisterPassenger(Passenger passenger)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model");
            else
                return Ok(_passengerRepository.Register(passenger));
        }

        [HttpPut]
        [Route("api/Passenger/UpdatePassenger")]
        public IHttpActionResult UpdatePassenger(Passenger passenger)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model");
            else
                return Ok(_passengerRepository.Update(passenger));
        }

        [HttpDelete]
        [Route("api/Passenger/DeletePassenger")]
        public IHttpActionResult DeletePassenger(String id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Not a valid passenger id");
            else
                return Ok(_passengerRepository.Delete(id));
        }
    }
}

