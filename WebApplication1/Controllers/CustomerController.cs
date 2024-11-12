using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Core.Services;
using WebApplication1.Models;
using AutoMapper;
using FluentValidation;

namespace WebApplication1.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IAirportService _airportService;
        private readonly IValidator<SearchFlightsRequest> _validator;

        public CustomerController(IFlightService flightService, IMapper mapper, IAirportService airportService, IValidator<SearchFlightsRequest> validator)
        {
            _flightService = flightService;
            _mapper = mapper;
            _airportService = airportService;
            _validator = validator;
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

            var response = airports.Select(airport => _mapper.Map<AirportResponse>(airport)).ToList();

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
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest();
            }

            var flights = _flightService.SearchFlights(request.From, request.To, request.DepartureDate);

            var response = _mapper.Map<PageResult<FlightResponse>>(flights);

            return Ok(response);
        }
         
    }
       
}
