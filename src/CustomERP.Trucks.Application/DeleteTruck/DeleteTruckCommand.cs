namespace CustomERP.Trucks.Application.DeleteTruck
{
    public class DeleteTruckCommand : ICommand
    {
        public Guid TruckId { get; }

        public DeleteTruckCommand(Guid truckId)
        {
            TruckId = truckId;
        }
    }
}
