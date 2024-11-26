using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : IAirportService
    {
        private readonly FlightPlannerDbContext _context;

        public AirportService(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public List<Airport> SearchAirports(string search)
        {
            search = search.Trim().ToLower();

            var airports = _context.Airports
                .Where(a => a.Country.ToLower().Contains(search) ||
                            a.City.ToLower().Contains(search) ||
                            a.AirportCode.ToLower().Contains(search)).ToList();


            return airports;
        }
    }
}
