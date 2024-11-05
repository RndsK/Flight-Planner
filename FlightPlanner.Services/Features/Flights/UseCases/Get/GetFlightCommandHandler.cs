﻿using AutoMapper;
using FlightPlanner.Core;
using FlightPlanner.Core.Services;
using MediatR;

namespace FlightPlanner.Services.Features.Flights.UseCases.Get
{
    public class GetFlightCommandHandler : IRequestHandler<GetFlightCommand, Result>
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public GetFlightCommandHandler(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }

        public Task<Result> Handle(GetFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = _flightService.GetFullFlightById(request.Id);
            if (flight == null)
            {
                return Task.FromResult(new Result
                {
                    Status = ResultStatus.NotFound
                });
            }

            return Task.FromResult(new Result
            {
                Status = ResultStatus.Success,
                Response = flight
            });
        }
    }
}
