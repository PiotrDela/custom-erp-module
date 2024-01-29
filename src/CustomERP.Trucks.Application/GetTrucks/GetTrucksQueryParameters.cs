namespace CustomERP.Trucks.Application.GetTrucks
{
    public class GetTrucksQueryParameters
    {
        public static GetTrucksQueryParameters Empty = new GetTrucksQueryParameters();

        public string Name { get; set; } = string.Empty;
    }
}
