using Akka.Actor;
using Vehicles.API.Services;

namespace Vehicles.API
{
    public class GetLineActor : ReceiveActor
    {
        public static readonly string findAllCommand = "findAll";
        private readonly LineService _lineService;

        public GetLineActor(LineService lineService)
        {
            _lineService = lineService;

            Receive<string>(message =>
            {
                if (string.Equals(message, findAllCommand, System.StringComparison.OrdinalIgnoreCase))
                {
                    Sender.Tell(_lineService.Get());
                }
                else
                {
                    Sender.Tell(_lineService.Get(message));
                }
            });
        }
    }
}
