using System.Collections.Generic;
using System.Net.Http;
using Akka.Actor;
using Newtonsoft.Json;
using Vehicles.API.Models;

namespace Vehicles.API
{
    /// <summary>
    /// Actor responsible for making external calls to WhereIsMyTransport API.
    /// Takes in HttpRequestMessage with defined url and request payload
    /// and returns response from external call to Sender.
    /// </summary>
    public class LineClientActor : ReceiveActor
    {
        public const string startCommand = "start";
        private readonly string _agency;
        private const string _client = "WhereIsMyTransport";

        public LineClientActor(string agency)
        {
            _agency = agency;

            Receive<string>(message =>
            {
                if (string.Equals(message, startCommand, System.StringComparison.OrdinalIgnoreCase))
                {
                    HttpRequestMessage getLinesRequest =
                    new HttpRequestMessage(HttpMethod.Get, $"/lines?agencies={_agency}");

                    Props ApiClientActorProps = Props.Create(() => new ApiClientActor(_client));
                    var apiClientActor = Context.ActorOf(ApiClientActorProps);

                    apiClientActor.Tell(getLinesRequest);
                }
                
            });

            Receive<HttpResponseMessage>(async response =>
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                List<Line> list = JsonConvert.DeserializeObject<List<Line>>(responseContent);

                Context.Parent.Tell(list);
            });
        }
    }
}
