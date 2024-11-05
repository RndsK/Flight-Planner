using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Core;
using FlightPlanner.Services.Features.Airports;
using FlightPlanner.Services.Features.Flights.Models;
using MediatR;

namespace FlightPlanner.Services.Features.Flights.UseCases.Add
{
    public class AddFlightCommand : IRequest<Result>
    {
        public AddAirportCommand From { get; set; }
        public AddAirportCommand To { get; set; }
        public string? Carrier { get; set; }
        public string? DepartureTime { get; set; }
        public string? ArrivalTime { get; set; }
    }
}
