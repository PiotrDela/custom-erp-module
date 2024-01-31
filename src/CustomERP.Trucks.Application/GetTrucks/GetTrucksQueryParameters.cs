namespace CustomERP.Trucks.Application.GetTrucks
{
    public class GetTrucksQueryParameters
    {
        public static GetTrucksQueryParameters Empty = new GetTrucksQueryParameters();

        public string Name { get; set; } = string.Empty;
        public GetTrucksQuerySortBy? OrderBy { get; set; }
    }

    public enum GetTrucksQuerySortBy
    {
        Code = 0,
        Name = 1,
        Status = 2,
    }
}
