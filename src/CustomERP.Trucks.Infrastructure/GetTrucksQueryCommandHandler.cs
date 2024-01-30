using CustomERP.Trucks.Infrastructure;

namespace CustomERP.Trucks.Application.GetTrucks
{
    public class GetTrucksQueryCommandHandler : IQueryHandler<GetTrucksQuery, IEnumerable<TruckDto>>
    {
        public Task<IEnumerable<TruckDto>> Handle(GetTrucksQuery request, CancellationToken cancellationToken)
        {
            using (var context = new TrucksDbContext())
            {
                var trucks = context.Trucks.AsQueryable();

                if (string.IsNullOrWhiteSpace(request.Parameters.Name) == false)
                {
                    trucks = trucks.Where(x => x.Name.Contains(request.Parameters.Name));
                }

                var result = trucks.ToArray();

                return Task.FromResult(result.Select(TruckDto.Create));
            }
        }
    }
}
