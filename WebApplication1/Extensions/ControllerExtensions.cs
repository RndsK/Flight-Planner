using System.Net;
using FlightPlanner.Core;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ToActionResult(
            this ControllerBase controller, Result result)
        {
            switch (result.Status)
            {
                case ResultStatus.Success:
                    return new OkObjectResult(result.Response);
                    break;
                case ResultStatus.Created:
                    return new CreatedResult("", result.Response);
                case ResultStatus.NotFound:
                    return new NotFoundObjectResult(null);
                    break;
                case ResultStatus.BadRequest:
                    return new BadRequestObjectResult(result.Response);
                    break;
                case ResultStatus.Conflict:
                    return new ConflictObjectResult(null);
                    break;
                default:
                    return new OkObjectResult(null);
            }
        }
    }
}
