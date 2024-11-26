using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController(IDbClearingService dbClearingService) : ControllerBase
    {

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            dbClearingService.Clear<Airport>();
            dbClearingService.Clear<Flight>();

            return Ok();
        }
    }
}
