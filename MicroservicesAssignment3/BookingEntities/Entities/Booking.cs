using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEntities.Entities
{
    public class Booking
    {
        public long Id { get; set; }
        public string Number { get; set; }

        public DateTime BookingDate { get; set; }
        public long FlightId { get; set; }
        public Flight Flight { get; set; }

        public long SeatId { get; set; }
        public string Seat { get; set; }

        public double Amount { get; set; }
    }
}
