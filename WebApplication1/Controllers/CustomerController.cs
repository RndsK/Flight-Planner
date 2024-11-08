using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Models;
using WebApplication1.Models;
using AutoMapper;

namespace WebApplication1.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IAirportService _airportService;

        public CustomerController(IFlightService flightService, IMapper mapper, IAirportService airportService)
        {
            _flightService = flightService;
            _mapper = mapper;
            _airportService = airportService;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirport(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest();
            }

            search = search.Trim().ToLower();

            var airports = _airportService.SearchAirports(search);

            var response = airports.Select(airport => new AirportResponse
            {
                Country = airport.Country,
                City = airport.City,
                Airport = airport.AirportCode
            }).ToList();

            return Ok(response);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flightService.GetFullFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FlightResponse>(flight);

            return Ok(response);
        }

        
        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlightsRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.From) || string.IsNullOrWhiteSpace(request.To) || string.IsNullOrWhiteSpace(request.DepartureDate))
            {
                return BadRequest();
            }
            if (request.From.Trim().ToLower() == request.To.Trim().ToLower())
            {
                return BadRequest();
            }
            var flights = _flightService.SearchFlights(request.From, request.To, request.DepartureDate);
            var pageResult = new PageResult<Flight>
            {
                Page = 0,
                TotalItems = flights.Count,
                Items = flights
            };

            return Ok(pageResult);

        }
         
    }
       
}
