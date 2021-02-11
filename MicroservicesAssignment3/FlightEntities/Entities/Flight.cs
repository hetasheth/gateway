using System;
using System.Collections.Generic;
using System.Text;

namespace FlightEntities.Entities
{
    public class Flight
    {
        public Flight()
        {
            
        }

        public long Id { get; set; }
        public string FlightNo { get; set; }
        public string SourceCity { get; set; }
        public string DestinationCity { get; set; }
    }
}
