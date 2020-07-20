using System.Net.Http; 
using System.Net.Http.Headers;
using Akka.Actor;

namespace Vehicles.API
{
    /// <summary>
    /// Actor responsible for making external calls to WhereIsMyTransport API.
    /// Takes in HttpRequestMessage with defined url and request payload
    /// and returns response from external call to Sender.
    /// </summary>
    public class ApiClientActor : ReceiveActor
    {
        private readonly IHttpClientFactory _clientFactory;

        public ApiClientActor()
        {
            Receive<HttpRequestMessage>(async request => {
                Props ClientAuthenticationActorProps = Props.Create(() =>
                    new ClientAuthenticationActor()
                );

                var clientAuthenticationActor =
                Context.ActorOf(ClientAuthenticationActorProps);

                clientAuthenticationActor.Tell(ClientAuthenticationActor.startCommand);

                var client = _clientFactory.CreateClient("whereIsMyTransport");
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", ClientAuthenticationActor.AccessToken);

                var response = await client.SendAsync(request);
                Sender.Tell(response);
            });
        }
    }
}
