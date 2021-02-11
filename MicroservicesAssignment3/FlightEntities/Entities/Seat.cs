using System;
using System.Collections.Generic;
using System.Text;
using FlightEntities.Enum;

namespace FlightEntities.Entities
{
    public class Seat
    {
        public Seat()
        {

        }

        public long Id { get; set; }
        public string SeatNumber { get; set; }
        public SeatEnum Type { get; set; }

        public string TypeName => Type.ToString();
    }
}
