using Akka.Actor;
using Vehicles.API.Models;
using Vehicles.API.Services;

namespace Vehicles.API
{
    public class CreateLineActor : ReceiveActor
    {
        private readonly LineService _lineService;

        public CreateLineActor(LineService lineService)
        {
            _lineService = lineService;

            Receive<Line>(line =>
            {
                Line createdLine = _lineService.Create(line);
                Sender.Tell(createdLine);
            });
        }
    }
}
