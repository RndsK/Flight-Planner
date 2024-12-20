﻿using System.ComponentModel.DataAnnotations;

namespace FlightPlanner.Core.Models
{
    public class Airport : Entity
    {
        public string Country { get; set; }
        [MaxLength(200)]
        public string City { get; set; }
        [MaxLength(10)]
        public string AirportCode { get; set; }
    }
}
