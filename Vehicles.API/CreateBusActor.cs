using Akka.Actor;
using Vehicles.API.Models;
using Vehicles.API.Services;

namespace Vehicles.API
{
    public class CreateBusActor : ReceiveActor
    {
        private readonly BusService _busService;

        public CreateBusActor(BusService busService)
        {
            _busService = busService;

            Receive<Bus>(bus =>
            {
                Bus createdBus = _busService.Create(bus);
                Sender.Tell(createdBus);
            });
        }
    }
}
