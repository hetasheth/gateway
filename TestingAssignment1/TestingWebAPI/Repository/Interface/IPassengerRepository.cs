using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestingWebAPI.Models;

namespace TestingWebAPI.Repository.Interface
{
    public interface IPassengerRepository
    {
        bool Register(Passenger passenger);
        bool Update(Passenger passenger);
        bool Delete(string id);
        IQueryable<Passenger> GetPassengers();
        Passenger GetPassengerById(Guid id);
    }
}