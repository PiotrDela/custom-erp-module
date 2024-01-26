using CustomERP.Domain.Exceptions;
using CustomERP.Domain.Trucks;
using CustomERP.Trucks.Domain;

namespace CustomERP.Trucks.Application.GetTruckById
{
    public class GetTrackByIdQueryHandler : IQueryHandler<GetTruckByIdQuery, TruckDto>
    {
        private readonly ITruckRepository truckRepository;

        public GetTrackByIdQueryHandler(ITruckRepository truckRepository)
        {
            this.truckRepository = truckRepository ?? throw new ArgumentNullException(nameof(truckRepository));
        }

        public async Task<TruckDto> Handle(GetTruckByIdQuery request, CancellationToken cancellationToken)
        {
            var truck = await truckRepository.GetByIdAsync(new TruckId(request.TruckId));
            if (truck == null)
            {
                throw new EntityNotFoundException($"{nameof(Truck)} has not been found");
            }
            return TruckDto.Create(truck);
        }
    }
}
