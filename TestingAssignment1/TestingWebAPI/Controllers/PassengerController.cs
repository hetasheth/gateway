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
        public List<Passenger> GetPassengers()
        {
            return _passengerRepository.GetPassengers();
        }

        [HttpGet]
        [Route("api/Passenger/GetPassengerById")]
        public HttpResponseMessage GetPassengerById(String id)
        {
            try
            {
                if (id != null)
                {
                    var passenger = _passengerRepository.GetPassengerById(id);
                    if (passenger.FirstName != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, passenger);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/Passenger/RegisterPassenger")]
        public Passenger RegisterPassenger(Passenger passenger)
        {
            return _passengerRepository.Register(passenger);
        }

        [HttpPut]
        [Route("api/Passenger/UpdatePassenger")]
        public Passenger UpdatePassenger(Passenger passenger)
        {
            return _passengerRepository.Update(passenger);
        }

        [HttpDelete]
        [Route("api/Passenger/DeletePassenger")]
        public bool DeletePassenger(String id)
        {
            return _passengerRepository.Delete(id);
        }
    }
}

