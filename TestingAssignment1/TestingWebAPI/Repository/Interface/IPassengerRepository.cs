using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestingWebAPI.Models;

namespace TestingWebAPI.Repository.Interface
{
    public interface IPassengerRepository
    {
        Passenger Register(Passenger passenger);
        Passenger Update(Passenger passenger);
        bool Delete(string id);
        List<Passenger> GetPassengers();
        Passenger GetPassengerById(string id);


    }
}