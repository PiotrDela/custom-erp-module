using CustomERP.Trucks.Domain;
using MediatR;

namespace CustomERP.Trucks.Application.CreateTruck
{
    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, Guid>
    {
        private readonly ITruckRepository truckRepository;

        public CreateTruckCommandHandler(ITruckRepository truckRepository)
        {
            this.truckRepository = truckRepository ?? throw new ArgumentNullException(nameof(truckRepository));
        }

        public async Task<Guid> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {
            var truck = Truck.CreateNew(request.Code, request.Name, request.Description);
            await truckRepository.AddAsync(truck);
            return truck.Id.Value;
        }
    }
}
