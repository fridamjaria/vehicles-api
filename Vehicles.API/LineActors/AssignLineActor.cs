using Akka.Actor;
using Vehicles.API.Models;
using Vehicles.API.Services;

namespace Vehicles.API.LineActors
{
    /// <summary>
    /// Actor to fetch the first line with least amount of vehicles, update the vehicle count
    /// and return WhereIsMyTransport ID (externalId) of the line
    /// </summary>
    public class AssignLineActor : ReceiveActor
    {
        public const string startCommand = "start";
        private static LineService _lineService = new LineService();

        public AssignLineActor()
        {
            Receive<string>(message =>
            {
                if(string.Equals(message, startCommand, System.StringComparison.OrdinalIgnoreCase))
                {
                    Line line = _lineService.GetLeastVehiclesLine();
                    if(line != null)
                    {
                        line.VehicleCount += 1;
                        Props UpdateLineActorProps = Props.Create(() =>
                        new UpdateLineActor(line.Id, _lineService));

                        var updateLineActor = Context.ActorOf(UpdateLineActorProps);

                        updateLineActor.Tell(line);
                        Sender.Tell(line.ExternalId);
                    }

                    Sender.Tell("");
                }
            });
        }
    }
}
