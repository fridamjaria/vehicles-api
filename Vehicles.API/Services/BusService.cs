using Vehicles.API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Vehicles.API.Services
{
    public class BusService
    {
        private readonly IMongoCollection<Bus> _busses;

        public BusService(IVehiclesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _busses = database.GetCollection<Bus>(settings.BussesCollectionName);
        }

        public List<Bus> Get() =>
            _busses.Find(bus => true).ToList();

        public Bus Get(string id) =>
           _busses.Find<Bus>(bus => bus.Id == id).FirstOrDefault();

        public Bus Create(Bus bus)
        {
            _busses.InsertOne(bus);
            return bus;
        }

        public void Update(string id, Bus busIn) =>
            _busses.ReplaceOne(bus => bus.Id == id, busIn);

        public void Remove(string id) =>
            _busses.DeleteOne(bus => bus.Id == id);
    }
}