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
            
        public bool Register(Passenger passenger)
        {
            if (passenger != null)
            {
                passenger.PassengerNumber = Guid.NewGuid();
                _dbContext.Passengers.Add(passenger);
                var result =_dbContext.SaveChanges();
                return result == 1 ? true : false;
            }
            return false;
        }

        public bool Update(Passenger passenger)
        {
            var passengerObj = _dbContext.Passengers.FirstOrDefault(x => x.PassengerNumber == passenger.PassengerNumber);
            if (passengerObj != null)
            {
                _dbContext.Passengers.Attach(passenger);
                var result= _dbContext.SaveChanges();
                return result == 1 ? true : false;
            }
            return false;
        }

        public bool Delete(string id)
        {
            var passenger = _dbContext.Passengers.FirstOrDefault(x => x.PassengerNumber == Guid.Parse(id));
            if (passenger != null)
            {
                _dbContext.Passengers.Remove(passenger);
                var result = _dbContext.SaveChanges();
                return result == 1 ? true : false;
            }
            return false;
        }

        public IQueryable<Passenger> GetPassengers()
        {
            return _dbContext.Passengers;
        }

        public Passenger GetPassengerById(Guid id)
        {
            // var passenger = _dbContext.Passengers.Where(s => (id.Equals(s.PassengerNumber.ToString()))).FirstOrDefault();
            return _dbContext.Passengers.FirstOrDefault(p => p.PassengerNumber == id);            
        }

    }
}