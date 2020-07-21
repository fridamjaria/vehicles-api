using System;
using Akka.Actor;
using Newtonsoft.Json;
using RestSharp;

namespace Vehicles.API
{
    public class ClientAuthenticationActor : ReceiveActor
    {
        public static string AccessToken { get; private set; }
        public static readonly string startCommand = "start";

        public ClientAuthenticationActor()
        {
            var client = new RestClient("https://identity.whereismytransport.com/connect/token")
            {
                Timeout = -1
            };
            IRestRequest request = new RestRequest(Method.POST);

            var clientId = Environment.GetEnvironmentVariable("WHERE_IS_MY_TRANSPORT_CLIENT_ID");
            var clientSecret = Environment.GetEnvironmentVariable("WHERE_IS_MY_TRANSPORT_CLIENT_SECRET");

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("scope", "transportapi:all");

            Receive<string>(message =>
            {
                if(string.Equals(message, startCommand, StringComparison.OrdinalIgnoreCase))
                {
                    IRestResponse response = client.Execute(request);
                    dynamic jObject = JsonConvert.DeserializeObject(response.Content);
                    AccessToken = jObject.access_token;

                    Sender.Tell("continue");
                } 
            });

        }
    }
}
