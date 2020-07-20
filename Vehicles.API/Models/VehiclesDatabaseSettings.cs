namespace Vehicles.API.Models
{
    public class VehiclesDatabaseSettings : IVehiclesDatabaseSettings
    {
        public string BussesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string LinesCollectionName { get; set; }
    }

    public interface IVehiclesDatabaseSettings
    {
        string BussesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string LinesCollectionName { get; set; }
    }
}
