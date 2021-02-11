using System;
using System.Collections.Generic;
using System.Text;

namespace FlightEntities.Entities
{
    public class FlightSeat
    {
        public long SeatId { get; set; }
        public Seat Seat { get; set; }
    }

    public class FlightSeatOptions
    {
        public FlightSeatOptions()
        {
            Seats = new List<FlightSeat>();
        }

        public long Id { get; set; }
        public long FlightId { get; set; }
        public Flight Flight { get; set; }
        public List<FlightSeat> Seats { get; }
    }
}
