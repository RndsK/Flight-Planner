using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Core;
using MediatR;

namespace FlightPlanner.Services.Features.Flights.UseCases.Get
{
    public class GetFlightCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
