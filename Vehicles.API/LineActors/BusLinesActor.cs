using System;
using System.Collections.Generic;
using System.Net.Http;
using Akka.Actor;
using Vehicles.API.Models;
using Vehicles.API.Services;

namespace Vehicles.API
{
    /// <summary>
    /// BusLineActor calls WhereIsMyTransport to get bus
    /// lines for Algoa Bus Company and store them in Line Document
    /// </summary>
    public class BusLinesActor : ReceiveActor
    {
        public const string startCommand = "start";

        /* The Agency being used is Algoa Bus Company.
         * From the list of agencies returned from WhereIsMyTransport API,
         * there is no agency for MyCiTi. For the purpose of this exercise
         * I've defaulted to using Algoa Bus Company agency.
         */
        private const string _agency = "_UxoeIJ20UqKSKfLAP-bXw";
        private static readonly LineService _lineService = new LineService();

        public BusLinesActor()
        {

            Receive<string>(message =>
            {
            if (string.Equals(message, startCommand, StringComparison.OrdinalIgnoreCase))
            {
                Props LineClientActorProps = Props.Create(() => new LineClientActor(_agency));
                var lineClientActor = Context.ActorOf(LineClientActorProps);
                lineClientActor.Tell(LineClientActor.startCommand);
                }
            });

            Receive<List<Line>>(list =>
            {
                Props CreateLineActorProps = Props.Create(() =>
                   new CreateLineActor(_lineService));

                var createLineActor = Context.ActorOf(CreateLineActorProps);

                foreach (Line line in list)
                {
                    line.ExternalId = line.Id; // Set the Id received from response object as ExternalId
                    line.Id = null; // clear out the Id value from response object
                    line.VehicleCount = 0; // setting default value for VehicleCount to 0

                    createLineActor.Tell(line);
                }
            });
        }
    }
}
