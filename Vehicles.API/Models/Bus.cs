using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vehicles.API.Models
{
    public class Bus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string BusName { get; set; }

        [BsonElement("Line")]
        public string BusLine { get; set; }

        public string RegNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }
    }
}
