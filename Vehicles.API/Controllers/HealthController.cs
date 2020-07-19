using System;
using Microsoft.AspNetCore.Mvc;

namespace Vehicles.API.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [Route("/health")]
        public IActionResult GetHealth()
        {
            return new JsonResult("Ok");
        }
    }
}
