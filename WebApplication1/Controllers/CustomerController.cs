using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Core.Models;

namespace WebApplication1.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        /*
        private readonly FlightStorage _storage;

        public CustomerController(FlightStorage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirport(string search)
        {
            var searchedAirport = _storage.SearchAirport(search);

            return Ok(new[] { searchedAirport });
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight([FromBody] SearchFlightsRequest? search)
        {
            try
            {
                var flights = _storage.SearchFlights(search);

                if (flights.Count == 0)
                {
                    return Ok(new PageResult<Flight>(0, 0, new List<Flight>()));
                }

                return Ok(new PageResult<Flight>(0, flights.Count, flights));

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            try
            {
                var flight = _storage.FindFlightById(id);

                return Ok(flight);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound();
            }
        }
 */
    }
       
}
