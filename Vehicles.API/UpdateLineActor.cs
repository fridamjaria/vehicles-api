using Akka.Actor;
using Vehicles.API.Models;
using Vehicles.API.Services;

namespace Vehicles.API
{
    public class UpdateLineActor : ReceiveActor
    {
        private readonly string _lineId;
        private readonly LineService _lineService;

        public UpdateLineActor(string lineId, LineService lineService)
        {
            _lineId = lineId;
            _lineService = lineService;

            Receive<Line>(line =>
            {
                _lineService.Update(_lineId, line);
                Sender.Tell(true);
            });
        }
    }
}
