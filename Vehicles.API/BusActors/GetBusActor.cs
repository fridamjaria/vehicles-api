using Akka.Actor;
using Vehicles.API.Services;

namespace Vehicles.API
{
    public class GetBusActor : ReceiveActor
    {
        public static readonly string findAllCommand = "findAll";
        private readonly BusService _busService;

        public GetBusActor(BusService busService)
        {
            _busService = busService;

            Receive<string>(message =>
            {
                if(string.Equals(message, findAllCommand, System.StringComparison.OrdinalIgnoreCase))
                {
                    Sender.Tell(_busService.Get());
                }
                else
                {
                    Sender.Tell(_busService.Get(message));
                }
            });
        }
    }
}
