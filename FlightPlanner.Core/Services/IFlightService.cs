using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight? GetFullFlightById(int id);

        bool Exists(Flight flight);

        public List<Flight> SearchFlights(string from, string to, string departureDate);
    }
}
