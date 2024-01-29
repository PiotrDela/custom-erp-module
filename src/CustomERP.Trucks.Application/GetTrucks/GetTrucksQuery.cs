namespace CustomERP.Trucks.Application.GetTrucks
{
    public class GetTrucksQuery : IQuery<IEnumerable<TruckDto>>
    {
        public GetTrucksQueryParameters Parameters { get; }

        public GetTrucksQuery(GetTrucksQueryParameters parameters)
        {
            Parameters = parameters ?? GetTrucksQueryParameters.Empty;
        }
    }
}
