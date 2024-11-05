using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Services.Features.Airports
{
    public class AddAirportCommand
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Airport { get; set; }
    }
}
