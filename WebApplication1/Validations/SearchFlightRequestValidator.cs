using FluentValidation;
using WebApplication1.Models;

namespace WebApplication1.Validations
{
    public class SearchFlightRequestValidator : AbstractValidator<SearchFlightsRequest>
    {
        public SearchFlightRequestValidator()
        {
            RuleFor(request => request.To).NotEmpty().NotEqual(request => request.From, StringComparer.OrdinalIgnoreCase);
            RuleFor(request => request.From).NotEmpty();
            RuleFor(request => request.DepartureDate).NotEmpty();
        }
    }
}
