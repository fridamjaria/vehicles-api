using Akka.Actor;
using Vehicles.API.Services;

namespace Vehicles.API
{
    public class DeleteBusActor : ReceiveActor
    {
        private readonly BusService _busService;
        public DeleteBusActor(BusService busService)
        {
            _busService = busService;

            Receive<string>(busId =>
            {
                _busService.Remove(busId);
                Sender.Tell(true);
            });
        }
    }
}
