using CustomERP.Trucks.Domain;

namespace CustomERP.Trucks.Application.GetTrucks
{
    public class GetTrucksQueryCommandHandler : IQueryHandler<GetTrucksQuery, IEnumerable<TruckDto>>
    {
        private readonly ITruckRepository truckRepository;

        public GetTrucksQueryCommandHandler(ITruckRepository truckRepository)
        {
            this.truckRepository = truckRepository ?? throw new ArgumentNullException(nameof(truckRepository));
        }

        public async Task<IEnumerable<TruckDto>> Handle(GetTrucksQuery request, CancellationToken cancellationToken)
        {
            var trucks = await truckRepository.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.Parameters.Name) == false)
            {
                trucks = trucks.Where(x => x.Name.Contains(request.Parameters.Name));
            }

            return trucks.Select(TruckDto.Create);
        }
    }
}
