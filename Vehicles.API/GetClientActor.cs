using System.Collections.Generic;
using Akka.Actor;
using Newtonsoft.Json;
using RestSharp;
using Vehicles.API.Models;

namespace Vehicles.API
{
    /// <summary>
    /// Generic Api Client Actor for making all external http calls
    /// Takes in name of client that has been created in Startup.cs
    /// Returns HttpResponseMessage to Sender
    /// 
    /// Note: Not fully generic, since auth is not being passed in -
    /// can currently only be used for WhereIsMyTransport API calls
    /// </summary>
    public class GetClientActor : ReceiveActor
    {
        public const string continueCommand = "continue";
        public const string startCommand = "start";
        private readonly string _url;

        public GetClientActor(string url)
        {
            _url = url;

            Receive<string>(message =>
            {

                if(string.Equals(message, startCommand, System.StringComparison.OrdinalIgnoreCase))
                {
                    Props ClientAuthenticationActorProps = Props.Create(() =>
                    new ClientAuthenticationActor());

                    var clientAuthenticationActor =
                    Context.ActorOf(ClientAuthenticationActorProps);

                    clientAuthenticationActor.Tell(ClientAuthenticationActor.startCommand);
                }

                else if(string.Equals(message, continueCommand, System.StringComparison.OrdinalIgnoreCase))
                {
                    var client = new RestClient(_url)
                    {
                        Timeout = -1
                    };
                    IRestRequest request = new RestRequest(Method.GET);
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("authorization", $"Bearer {ClientAuthenticationActor.AccessToken}");


                    var response = client.Execute(request);
                    List<Line> list = JsonConvert.DeserializeObject<List<Line>>(response.Content);

                    Context.Parent.Tell(list);
                }
            });
        }
    }
}
