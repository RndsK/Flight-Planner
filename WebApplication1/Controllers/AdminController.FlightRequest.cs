namespace WebApplication1.Controllers
{
    public record FlightRequest
    {
        public required AirportRequest From { get; set; }
        public required AirportRequest To { get; set; }
        public required string Carrier { get; set; }
        public required string DepartureTime { get; set; }
        public required string ArrivalTime { get; set; }
    }

    public record AirportRequest
    {
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Airport { get; set; }
    }

    public class PageResult<T>
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<T> Items { get; set; }
    }

    public record SearchFlightsRequest
    {
        public required string From { get; set; }
        public required string To { get; set; }
        public string DepartureDate { get; set; }
    }
}
