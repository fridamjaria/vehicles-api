using Akka.Actor;
using Vehicles.API.LineActors;
using Vehicles.API.Models;
using Vehicles.API.Services;

namespace Vehicles.API
{
    public class CreateBusActor : ReceiveActor
    {
        private readonly BusService _busService;
        private Bus _bus;
        private IActorRef _originalSender;

        public CreateBusActor(BusService busService)
        {
            _busService = busService;

            Receive<Bus>(bus =>
            {
                _originalSender = Sender;
                _bus = bus;
                if(_bus.BusLineId == null)
                {
                    Props AssignLineActorProps = Props.Create(() => new AssignLineActor());
                    var assignLineActor = Context.ActorOf(AssignLineActorProps);

                    assignLineActor.Tell(AssignLineActor.startCommand);
                }
                else
                {
                    Bus createdBus = _busService.Create(_bus);
                    _originalSender.Tell(createdBus);
                }
            });

            Receive<string>(message =>
            {
                Bus createdBus;

                /* create the bus with no line assigned if no line could be retrieved
                 * could then update it later or add logging around this without
                 * stopping a bus from being created in the system
                 */

                if (string.IsNullOrEmpty(message))
                {
                    createdBus = _busService.Create(_bus);
                    
                }
                else
                {
                    _bus.BusLineId = message;
                    createdBus = _busService.Create(_bus);
                }
                _originalSender.Tell(createdBus);
            });
        }
    }
}
