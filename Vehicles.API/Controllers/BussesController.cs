using System;
using Microsoft.AspNetCore.Mvc;
using Akka.Actor;
using System.Collections.Generic;
using Vehicles.API.Models;
using System.Threading.Tasks;

namespace Vehicles.API.Controllers
{
    [ApiController]
    [Route("api/busses")]
    public class BussesController : ControllerBase
    {
        private ActorSystem _actorSystem;

        public BussesController(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;
        }

        [HttpGet]
        public async Task<JsonResult> GetBussesAsync()
        {
            Props GetBussesActorProps = Props.Create(() => new GetBusActor());
            var getBussesActor = _actorSystem.ActorOf(GetBussesActorProps, "getBussesActor");
            var response = await getBussesActor.Ask<List<Bus>>("listAll");

            return new JsonResult(response);
        }

        [HttpGet("{busId:guid}")]
        public async Task<JsonResult> GetBus(Guid busId)
        {
            Props GetBussesActorProps = Props.Create(() => new GetBusActor());
            var getBussesActor = _actorSystem.ActorOf(GetBussesActorProps, "getBusActor");
            var response = await getBussesActor.Ask<Bus>(busId);

            return new JsonResult(response);
        }

        [HttpPatch("{busId:guid}")]
        public async Task<ActionResult> UpdateBus(Guid busId, JsonOptions options)
        {
            if (options == null)
            {
                return BadRequest();
            }

            Props UpdateBusActorProps = Props.Create(() => new UpdateBusActor());
            var updateBusActor = _actorSystem.ActorOf(UpdateBusActorProps, "updateBusActor");
            var response = await updateBusActor.Ask<Boolean>(options);

            return new JsonResult(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBus(JsonOptions options)
        {
            if (options == null)
            {
                return BadRequest();
            }

            Props createBusActorProps = Props.Create(() => new CreateBusActor());
            var createBusActor = _actorSystem.ActorOf(createBusActorProps, "createBusActor");
            var response = await createBusActor.Ask<Boolean>(options);

            return new JsonResult(response);
        }

        [HttpDelete("{busId:guid}")]
        public async Task<ActionResult> DeleteBus(Guid busId)
        {
            Props DeleteBusActorProps = Props.Create(() => new GetBussesActor());
            var deleteBusActor = _actorSystem.ActorOf(DeleteBusActorProps, "deleteBusActor"); ;
            var response = await deleteBusActor.Ask<Boolean>(busId);

            return new JsonResult(response);
        }
    }
}
