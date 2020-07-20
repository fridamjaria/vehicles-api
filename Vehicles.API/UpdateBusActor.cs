using System;
using Akka.Actor;

namespace Vehicles.API
{
    public class UpdateBusActor : ReceiveActor
    {
        private readonly Guid _busId;
        public UpdateBusActor(Guid busId)
        {
            _busId = busId;
        }
    }
}
