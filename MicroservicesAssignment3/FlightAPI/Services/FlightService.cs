using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightEntities.Entities;

namespace FlightAPI.Services
{
    public class FlightService
    {
        public List<Flight> GetFlights()
        {
            var flights = new List<Flight>();
            for (int i = 1; i <= 5; i++)
            {
                flights.Add(new Flight
                {
                    Id = i,
                    FlightNo = $"Flight_{i}",
                    SourceCity = $"Source_{i}",
                    DestinationCity = $"Destination_{i}"
                });
            }
            return flights;
        }

        public FlightSeatOptions GetSeatOptions(long id)
        {
            var flight = GetFlights().Find(r => r.Id == id);
            var so = new FlightSeatOptions()
            {
                Id = 1,
                Flight = flight,
                FlightId = flight.Id
            };

            var seats = new SeatService().GetSeats();
            for (int i = 0; i < seats.Count; i++)
            {
                var seat = seats[i];
                so.Seats.Add(new FlightSeat
                {
                    Seat = seat,
                    SeatId = seat.Id
                });
            }
            return so;
        }
    }
}
