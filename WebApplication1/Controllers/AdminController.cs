using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Services.Features.Flights.UseCases.Add;
using FlightPlanner.Services.Features.Flights.UseCases.Get;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Extensions;

namespace WebApplication1.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController(
        IMapper mapper,
        IMediator mediator) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        private static readonly object _lock = new();

        [Route("flights/{id}")]
        [HttpGet]
        public  async Task<IActionResult> GetFlight(int id)
        {
            return this.ToActionResult(await _mediator.Send(new GetFlightCommand
            {
                Id = id
            }));
        }

        [HttpPost]
        [Route("flights")]
        public async Task<IActionResult> AddFlight(FlightRequest request)
        {
            return this.ToActionResult(await _mediator.Send(
                _mapper.Map<AddFlightCommand>(request)));









            /*lock (_lock)
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
            }*/
        }

        
        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            /*lock (_lock)
            {
                var flight = _flightService.GetFullFlightById(id);

                if (flight != null)
                {
                    _flightService.Delete(flight);
                }
            }
             */

                return Ok();
            
        }
        
    }
}
