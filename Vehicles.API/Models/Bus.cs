using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Vehicles.API.Models
{
    [BsonIgnoreExtraElements]
    public class Bus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string BusName { get; set; }

        [BsonElement("LineId")]
        [JsonProperty("LineId")]
        public string BusLineId { get; set; }

        public string RegNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }
    }
}
