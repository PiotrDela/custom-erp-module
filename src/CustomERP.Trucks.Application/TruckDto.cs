using CustomERP.Trucks.Domain;

namespace CustomERP.Trucks.Application
{
    public class TruckDto
    {
        public Guid Id { get; }
        public string Code { get; }
        public string Name { get; }
        public string Description { get; }
        public string Status { get; }

        public static TruckDto Create(Truck truck)
        {
            return new TruckDto(truck.Id.Value, truck.Code.Value, truck.Name, truck.Description, truck.UsageStatus.ToString());
            // TODO: use automapper for example
        }

        private TruckDto(Guid id, string code, string name, string description, string status)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Description = description;
            this.Status = status;
        }
    }
}
