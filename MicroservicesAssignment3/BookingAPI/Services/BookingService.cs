using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingEntities.Entities;

namespace BookingAPI.Services
{
    public class BookingService
    {
        public IEnumerable<Booking> GetBookings(long? id = null)
        {
            var bookings = new List<Booking>();
            var flights = GetFlights();
            if (id != null)
            {
                flights = flights.Where(r => r.FlightId == id);
            }
            foreach (var flight in flights)
            {
                for (int i = 1; i <= 2; i++)
                {
                    bookings.Add(new Booking
                    {
                        Id = i,
                        Number = $"{flight.FlightNo}_BookingNumber_{i}",
                        Amount = (double)i * 10000,
                        Seat = $"Seat_{i}",
                        SeatId = i,
                        BookingDate = DateTime.Now.AddMinutes(i * (-10)),                        
                        FlightId = flight.FlightId,
                        Flight = flight
                    });
                }
            }
            return bookings;
        }

        private IEnumerable<Flight> GetFlights()
        {
            var flights = new List<Flight>();
            for (int i = 1; i <= 4; i++)
            {
                flights.Add(new Flight
                {
                    Id = i,
                    FlightId = i,
                    FlightNo = $"Flight_{i}"
                });
            }
            return flights;
        }
    }
}
