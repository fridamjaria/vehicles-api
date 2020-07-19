using System;
using Microsoft.AspNetCore.Mvc;

namespace Vehicles.API.Controllers
{
    [ApiController]
    [Route("api/busses")]
    public class BussesController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetBusses()
        {
            return new JsonResult("Ok");
        }

        [HttpGet("{busId:guid}")]
        public IActionResult GetBus(Guid busId)
        {
            return new JsonResult("Ok");
        }

        [HttpPatch("{busId:guid}")]
        public ActionResult UpdateBus(Guid busId, JsonOptions options)
        {
            if (options == null)
            {
                return BadRequest();
            }

            return new JsonResult("Updated");
        }

        [HttpPost]
        public ActionResult CreateBus(JsonOptions options)
        {
            if (options == null)
            {
                return BadRequest();
            }

            return new JsonResult("Created");
        }

        [HttpDelete("{busId:guid}")]
        public ActionResult DeleteBus(Guid busId)
        {
            return new JsonResult("Deleted");
        }
    }
}
