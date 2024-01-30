using CustomERP.Domain.Trucks;

namespace CustomERP.Trucks.Application.UpdateTruck
{
    public class UpdateTruckCommand : ICommand
    {
        public Guid TruckId { get; }
        public string Code { get; }
        public string Name { get; }
        public string Description { get; }
        public TrackUsageStatus? UsageStatus { get; }

        public UpdateTruckCommand(Guid truckId, string code, string name, string description, TrackUsageStatus? usageStatus)
        {
            this.TruckId = truckId;
            this.Code = code;
            this.Name = name;
            this.Description = description;
            this.UsageStatus = usageStatus;
        }
    }
}
