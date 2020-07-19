using System;
using System.Net.Http;
using Akka.Actor;

namespace Vehicles.API
{
    public class ApiClientActor : ReceiveActor
    {
        private readonly IHttpClientFactory _clientFactory;

        public ApiClientActor()
        {
            Receive<HttpRequestMessage>(request => {
                var client = _clientFactory.CreateClient("whereIsMyTransport");
                var response = client.SendAsync(request);

                Sender.Tell("Ok");
            });
        }
    }
}
