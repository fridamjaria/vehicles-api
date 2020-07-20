using System.Net.Http; 
using System.Net.Http.Headers;
using Akka.Actor;

namespace Vehicles.API
{
    /// <summary>
    /// Generic Api Client Actor for making all external http calls
    /// Takes in name of client that has been created in Startup.cs
    /// Returns HttpResponseMessage to Sender
    /// </summary>
    public class ApiClientActor : ReceiveActor
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _client;

        public ApiClientActor(string client)
        {
            _client = client;

            Receive<HttpRequestMessage>(request => {
                Props ClientAuthenticationActorProps = Props.Create(() =>
                    new ClientAuthenticationActor()
                );

                var clientAuthenticationActor =
                Context.ActorOf(ClientAuthenticationActorProps);

                clientAuthenticationActor.Tell(ClientAuthenticationActor.startCommand);

                var client = _clientFactory.CreateClient(_client);
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", ClientAuthenticationActor.AccessToken);

                var response = client.SendAsync(request);

                Sender.Tell(response);
            });
        }
    }
}
