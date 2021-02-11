using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightEntities.Entities;
using FlightEntities.Enum;

namespace FlightAPI.Services
{
    public class SeatService
    {
        public List<Seat> GetSeats()
        {
            var seats = new List<Seat>();
            for (int i = 1; i <= 10; i++)
            {
                seats.Add(new Seat
                {
                    Id = i,
                    SeatNumber = $"SeatNumber_{i}",
                    Type = SeatEnum.BusinessClass,
                });
            }
            return seats;
        }
    }
}
