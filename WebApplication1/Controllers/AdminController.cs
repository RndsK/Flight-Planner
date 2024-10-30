using Azure.Core;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController(IEntityService<Flight> flightService) : ControllerBase
    {
        private readonly IEntityService<Flight> _flightService = flightService;
       

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var result = _flightService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            var response = GetFromFlight(result);
            return Ok(response);
        }

        [HttpPost]
        [Route("flights")]
        public IActionResult AddFlight(FlightRequest request)
        {
            try
            {
                var flight = GetFromRequest(request);
                var result = _flightService.Create(flight);
                var response = GetFromFlight(flight);
                response.Id = result.Entity.Id;
                return Created("", response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            
        }

        /*
        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            _storage.DeleteFlight(id);

            return Ok();
        }
        */

        private Flight GetFromRequest(FlightRequest request)
        {
            return new Flight
            {
                ArrivalTime = request.ArrivalTime,
                Carrier = request.Carrier,
                DepartureTime = request.DepartureTime,
                From = new Airport
                {
                    AirportCode = request.From.Airport,
                    City = request.From.City,
                    Country = request.From.Country,
                },
                To = new Airport
                {
                    AirportCode = request.To.Airport,
                    City = request.To.City,
                    Country = request.To.Country,
                }
            };

        }

        private FlightResponse GetFromFlight(Flight flight)
        {
            return new FlightResponse()
            {
                Id = flight.Id,
                ArrivalTime = flight.ArrivalTime,
                Carrier = flight.Carrier,
                DepartureTime = flight.DepartureTime,
                From = new AirportResponse()
                {
                    Airport = flight.From.AirportCode,
                    City = flight.From.City,
                    Country = flight.From.Country,
                },
                To = new AirportResponse()
                {
                    Airport = flight.To.AirportCode,
                    City = flight.To.City,
                    Country = flight.To.Country,
                }
            };
        }
    }
}
