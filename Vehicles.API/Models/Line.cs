using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vehicles.API.Models
{
    [BsonIgnoreExtraElements]
    public class Line
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ExternalId { get; set; }

        public string Mode { get; set; }

        public string Name { get; set; }

        public int VehicleCount { get; set; }

    }
}
