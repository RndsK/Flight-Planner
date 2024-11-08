using FlightPlanner.Core.Models;
using FluentValidation;

namespace WebApplication1.Validations
{
    public class FlightValidator : AbstractValidator<Flight>
    {
        public FlightValidator()
        {
            RuleFor(flight => flight.ArrivalTime).NotEmpty();
            RuleFor(flight => flight.DepartureTime).NotEmpty();

            RuleFor(flight => flight.DepartureTime)
                .Must((flight, departureTime) =>
                    DateTime.Parse(departureTime) < DateTime.Parse(flight.ArrivalTime))
                .When(flight =>
                    !string.IsNullOrEmpty(flight.ArrivalTime) && !string.IsNullOrEmpty(flight.DepartureTime));

            RuleFor(flight => flight.Carrier).NotEmpty();

            RuleFor(flight => flight.To).NotEmpty();
            RuleFor(flight => flight.From).NotEmpty();
            RuleFor(flight => flight.From.AirportCode).NotEmpty();
            RuleFor(flight => flight.To.AirportCode).NotEmpty();

            RuleFor(flight => flight)
                .Must(flight =>
                    !string.Equals(flight.From.AirportCode?.Trim(),
                        flight.To.AirportCode?.Trim(),
                        StringComparison.OrdinalIgnoreCase));


        }
    }
}
