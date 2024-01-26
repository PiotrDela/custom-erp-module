namespace CustomERP.Trucks.Application.GetTruckById
{
    public class GetTruckByIdQuery : IQuery<TruckDto>
    {
        public Guid TruckId { get; }

        public GetTruckByIdQuery(Guid truckId)
        {
            TruckId = truckId;
        }
    }
}
