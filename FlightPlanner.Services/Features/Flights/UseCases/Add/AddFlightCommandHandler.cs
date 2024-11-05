using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FlightPlanner.Core;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Services.Features.Flights.Models;
using FluentValidation;
using MediatR;

namespace FlightPlanner.Services.Features.Flights.UseCases.Add
{
    public class AddFlightCommandHandler : IRequestHandler<AddFlightCommand, Result>
    {
        private readonly IValidator<AddFlightCommand> _validator;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public AddFlightCommandHandler(IValidator<AddFlightCommand> validator, IFlightService flightService, IMapper mapper)
        {
            _validator = validator;
            _flightService = flightService;
            _mapper = mapper;
        }

        public Task<Result> Handle(
            AddFlightCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return Task.FromResult(new Result
                {
                    Status = ResultStatus.BadRequest
                });
            }

            var flight = _mapper.Map<Flight>(request);
            _ = _flightService.Create(flight);
            return Task.FromResult(new Result
            {
                Status = ResultStatus.Success,
                Response = _mapper.Map<FlightViewModel>(flight)
            });

        }
    }
}
