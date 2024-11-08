using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(FlightPlannerDbContext context) : base(context)
        {
        }


        public Flight? GetFullFlightById(int id)
        {
            return _context.Flights
                .Include(flight => flight.From)
                .Include(flight => flight.To)
                .SingleOrDefault(flight => flight.Id == id);
        }

        public bool CheckForDuplicates(Flight flight)
        {
            return _context.Flights.Any(existingFlight =>
                existingFlight.From.AirportCode == flight.From.AirportCode &&
                existingFlight.To.AirportCode == flight.To.AirportCode &&
                existingFlight.Carrier == flight.Carrier &&
                existingFlight.DepartureTime == flight.DepartureTime &&
                existingFlight.ArrivalTime == flight.ArrivalTime);
        }

        public List<Flight> SearchFlights(string from, string to, string departureDate)
        {
            return _context.Flights
                .Where(flight =>
                    flight.From.AirportCode.ToLower() == from.ToLower() &&
                    flight.To.AirportCode.ToLower() == to.ToLower() &&
                    flight.DepartureTime.StartsWith(departureDate))
                .ToList();
        }

    }
}
