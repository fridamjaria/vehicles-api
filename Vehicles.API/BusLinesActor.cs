using System;
using System.Collections.Generic;
using System.Net.Http;
using Akka.Actor;

namespace Vehicles.API
{
    public class BusLinesActor : ReceiveActor
    {
        public const string startCommand = "start";

        /* The Agency being used is Algoa Bus Company.
         * From the list of agencies returned, there is no agency
         * for MyCiTi. For the purpose of this exercise I've defaulted
         * to using Algoa Bus Company agency.
         */
        private const string _agency = "_UxoeIJ20UqKSKfLAP-bXw";

        public BusLinesActor()
        {
            Receive<string>(message =>
            {
                if (string.Equals(message, startCommand, StringComparison.OrdinalIgnoreCase))
                {
                    HttpRequestMessage getLinesRequest =
                    new HttpRequestMessage(HttpMethod.Get, $"/lines?agencies={_agency}");

                    Props ApiClientActorProps = Props.Create(() => new ApiClientActor());
                    var apiClientActor = Context.ActorOf(ApiClientActorProps);

                    apiClientActor.Tell(getLinesRequest);
                }
            });

            Receive<List<object>>(list =>
            {

            });
        }
    }
}
