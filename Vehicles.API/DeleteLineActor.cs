using Akka.Actor;
using Vehicles.API.Services;

namespace Vehicles.API
{
    public class DeleteLineActor : ReceiveActor
    {
        public const string startCommand = "start";
        private readonly LineService _lineService;

        public DeleteLineActor(LineService lineService)
        {
            _lineService = lineService;

            Receive<string>(message =>
            {
                if(string.Equals(message, startCommand, System.StringComparison.OrdinalIgnoreCase))
                {
                    _lineService.RemoveAll();
                    Sender.Tell(true);
                }
            });
        }
    }
}
