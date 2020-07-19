using System;
using System.Collections.Generic;
using System.Net.Http;
using Akka.Actor;
using Newtonsoft.Json.Linq;

namespace Vehicles.API
{
    public class ClientAuthenticationActor : ReceiveActor
    {
        public static string AccessToken { get; private set; }
        private readonly IHttpClientFactory _clientFactory;

        public ClientAuthenticationActor()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "connect/token");

            var ClientId = Environment.GetEnvironmentVariable("WHERE_IS_MY_TRANSPORT_CLIENT_ID");
            var ClientSecret = Environment.GetEnvironmentVariable("WHERE_IS_MY_TRANSPORT_CLIENT_SECRET");

            var payload = new Dictionary<string, string>
            {
                { "client_id",     ClientId },
                { "client_secret", ClientSecret },
                { "grant_type",    "client_credentials" },
                { "scope",         "transportapi:all" }
            };

            Receive<String>(message =>
            {
                var client = _clientFactory.CreateClient("whereIsMyTransportAuth");
                var response = client.PostAsync("connect/token", new FormUrlEncodedContent(payload)).Result;
                string json = response.Content.ReadAsStringAsync().Result;
                var jObject = JObject.Parse(json);
                AccessToken = jObject["access_token"].ToString();
            });

        }
    }
}
