using Akka.Actor;
using Vehicles.API.Models;
using Vehicles.API.Services;

namespace Vehicles.API
{
    public class UpdateBusActor : ReceiveActor
    {
        private readonly string _busId;
        private readonly BusService _busService;

        public UpdateBusActor(string busId, BusService busService)
        {
            _busId = busId;
            _busService = busService;

            Receive<Bus>(bus =>
            {
                _busService.Update(_busId, bus);
                Sender.Tell(true);
            });
        }
    }
}
