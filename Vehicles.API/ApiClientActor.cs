using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Akka.Actor;

namespace Vehicles.API
{
    public class ApiClientActor : ReceiveActor
    {
        private readonly IHttpClientFactory _clientFactory;

        public ApiClientActor()
        {
            Receive<HttpRequestMessage>(async request => {
                if(String.IsNullOrEmpty(ClientAuthenticationActor.AccessToken))
                {
                    Props ClientAuthenticationActorProps = Props.Create(() =>
                        new ClientAuthenticationActor()
                    );

                    var clientAuthenticationActor = Context.ActorOf(
                        ClientAuthenticationActorProps, "clientAuthenticationActor"
                    );

                    clientAuthenticationActor.Tell("start");
                }

                var client = _clientFactory.CreateClient("whereIsMyTransport");
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", ClientAuthenticationActor.AccessToken);

                var response = await client.SendAsync(request);
                Sender.Tell(response);
            });
        }
    }
}
