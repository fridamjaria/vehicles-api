using System.Collections.Generic;
using MongoDB.Driver;
using Vehicles.API.Models;

namespace Vehicles.API.Services
{
    public class LineService
    {
        private readonly IMongoCollection<Line> _lines;

        public LineService(IVehiclesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _lines = database.GetCollection<Line>(settings.LinesCollectionName);
        }

        public List<Line> Get() =>
            _lines.Find(line => true).ToList();

        public Line Get(string id) =>
           _lines.Find<Line>(line => line.Id == id).FirstOrDefault();

        public Line Create(Line line)
        {
            _lines.InsertOne(line);
            return line;
        }

        public void Update(string id, Line lineIn) =>
            _lines.ReplaceOne(line => line.Id == id, lineIn);

        public void RemoveAll() =>
            _lines.DeleteMany(line => true);
    }
}
