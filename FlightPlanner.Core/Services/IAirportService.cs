﻿using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService
    {
        List<Airport> SearchAirports(string search);
    }
}
