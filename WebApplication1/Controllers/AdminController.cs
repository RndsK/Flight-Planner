using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using IValidator = WebApplication1.Validations.IValidator;

namespace WebApplication1.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController(
        IFlightService flightService,
        IEnumerable<IValidator> validators,
        IValidator<Flight> validator,
        IMapper mapper) : ControllerBase
    {
        private readonly IFlightService _flightService = flightService;
        private readonly IEnumerable<IValidator> _validators = validators;
        private readonly IValidator<Flight> _validator = validator;
        private IMapper _mapper = mapper;

        private static readonly object _lock = new();

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var result = _flightService.GetFullFlightById(id);
            if (result == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FlightResponse>(result);
            return Ok(response);
        }

        [HttpPost]
        [Route("flights")]
        public IActionResult AddFlight(FlightRequest request)
        {
            lock (_lock)
            {
                try
                {
                    var flight = _mapper.Map<Flight>(request);
                    var validationResult = _validator.Validate(flight);

                    if (!validationResult.IsValid)
                    {
                        return BadRequest();
                    }

                    if (_flightService.CheckForDuplicates(flight))
                    {
                        return Conflict();
                    }

                    var result = _flightService.Create(flight);
                    var response = _mapper.Map<FlightResponse>(flight);
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
        }

        
        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_lock)
            {
                var flight = _flightService.GetFullFlightById(id);

                if (flight != null)
                {
                    _flightService.Delete(flight);
                }

                return Ok();
            }
        }
        
    }
}
