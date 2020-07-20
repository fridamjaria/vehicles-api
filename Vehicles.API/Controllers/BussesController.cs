using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Models;
using Vehicles.API.Services;

namespace Vehicles.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BussesController : ControllerBase
    {
        private readonly ActorSystem _actorSystem;
        private readonly BusService _busService;

        public BussesController(ActorSystem actorSystem, BusService busService)
        {
            _actorSystem = actorSystem;
            _busService = busService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBus(Bus payload)
        {
            if (payload == null)
            {
                return BadRequest();
            }

            Props createBusActorProps = Props.Create(() => new CreateBusActor(_busService));
            var createBusActor = _actorSystem.ActorOf(createBusActorProps);

            var response = await createBusActor.Ask<Bus>(payload);

            return new JsonResult(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetBusses()
        {
            Props GetBusActorProps = Props.Create(() => new GetBusActor(_busService));
            var getBusActor = _actorSystem.ActorOf(GetBusActorProps);
            var response = await getBusActor.Ask<List<Bus>>(GetBusActor.findAllCommand);

            return new JsonResult(response);
        }

        [HttpGet("{busId:length(24)}")]
        public async Task<JsonResult> GetBus(string busId)
        {
            Props GetBusActorProps = Props.Create(() => new GetBusActor(_busService));
            var getBusActor = _actorSystem.ActorOf(GetBusActorProps);
            var response = await getBusActor.Ask<Bus>(busId);

            return new JsonResult(response);
        }

        [HttpPatch("{busId:length(24)}")]
        public async Task<ActionResult> UpdateBus(string busId, Bus payload)
        {
            if (payload == null)
            {
                return BadRequest();
            }

            Props UpdateBusActorProps = Props.Create(() => new UpdateBusActor(busId, _busService));
            var updateBusActor = _actorSystem.ActorOf(UpdateBusActorProps);

            payload.Id = busId;
            await updateBusActor.Ask<Boolean>(payload);

            return NoContent();
        }

        [HttpDelete("{busId:length(24)}")]
        public async Task<ActionResult> DeleteBus(string busId)
        {
            Props DeleteBusActorProps = Props.Create(() => new DeleteBusActor(_busService));
            var deleteBusActor = _actorSystem.ActorOf(DeleteBusActorProps);

            await deleteBusActor.Ask<Boolean>(busId);

            return NoContent();
        }
    }
}
