using System.Collections.Generic;
using System.Net.Http;
using Akka.Actor;
using Newtonsoft.Json;
using Vehicles.API.Models;

namespace Vehicles.API
{
    /// <summary>
    /// Actor responsible for getting lines for specifuc  WhereIsMyTransport API.
    /// Takes in HttpRequestMessage with defined url and request payload
    /// and returns response from external call to Sender.
    /// </summary>
    public class LineClientActor : ReceiveActor
    {
        public const string startCommand = "start";
        private readonly string _agency;
        private const string _baseUrl = "https://platform.whereismytransport.com";
        private IActorRef _originalSender;

        public LineClientActor(string agency)
        {
            _agency = agency;
            
            Receive<string>(message =>
            {
                _originalSender = Sender;
                if (string.Equals(message, startCommand, System.StringComparison.OrdinalIgnoreCase))
                {
                    Props GetClientActorProps = Props.Create(() =>
                    new GetClientActor($"{_baseUrl}/api/lines?agencies={_agency}"));
                    var getClientActor = Context.ActorOf(GetClientActorProps);

                    getClientActor.Tell(GetClientActor.startCommand);
                }
                
            });

            Receive<HttpResponseMessage>(async response =>
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                List<Line> list = JsonConvert.DeserializeObject<List<Line>>(responseContent);

                _originalSender.Tell(list);
            });
        }
    }
}
