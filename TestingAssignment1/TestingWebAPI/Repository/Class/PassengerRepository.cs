using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestingWebAPI.Models;
using TestingWebAPI.Repository.Interface;

namespace TestingWebAPI.Repository.Class
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly GatewayCruiseDBEntities _dbContext;

        public PassengerRepository()
        {
            _dbContext = new GatewayCruiseDBEntities();
        }    
            
        public Passenger Register(Passenger passenger)
        {
            if (passenger != null)
            {
                passenger.PassengerNumber = Guid.NewGuid();
                _dbContext.Passengers.Add(passenger);
                _dbContext.SaveChanges();
                return passenger;
            }
            return passenger;
        }

        public Passenger Update(Passenger passenger)
        {
            var p = _dbContext.Passengers.Where(x => (passenger.Equals(x.PassengerNumber.ToString()))).FirstOrDefault();
            _dbContext.Passengers.Attach(passenger);
            _dbContext.SaveChanges();
            return passenger;
        }

        public bool Delete(string id)
        {
            var passenger = _dbContext.Passengers.Where(s => (id.Equals(s.PassengerNumber.ToString()))).FirstOrDefault();
            if (passenger.FirstName != null)
            {
                _dbContext.Passengers.Remove(passenger);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Passenger> GetPassengers()
        {
            var entities = _dbContext.Passengers.OrderBy(c => c.FirstName).ToList();
            return entities;
        }

        public Passenger GetPassengerById(string id)
        {
            var passenger = _dbContext.Passengers.Where(s => (id.Equals(s.PassengerNumber.ToString()))).FirstOrDefault();
            return passenger;
        }

    }
}