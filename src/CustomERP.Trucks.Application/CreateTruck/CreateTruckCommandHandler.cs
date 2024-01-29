using CustomERP.Trucks.Domain;
using MediatR;

namespace CustomERP.Trucks.Application.CreateTruck
{
    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, Guid>
    {
        private readonly ITruckRepository truckRepository;
        private readonly ITruckCodeUniquenessConstraint truckCodeUniquenessConstraint;

        public CreateTruckCommandHandler(ITruckRepository truckRepository, ITruckCodeUniquenessConstraint truckCodeUniquenessConstraint)
        {
            this.truckRepository = truckRepository ?? throw new ArgumentNullException(nameof(truckRepository));
            this.truckCodeUniquenessConstraint = truckCodeUniquenessConstraint ?? throw new ArgumentNullException(nameof(truckCodeUniquenessConstraint));
        }

        public async Task<Guid> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {
            var truck = Truck.Create(request.Code, request.Name, this.truckCodeUniquenessConstraint, request.Description);
            await truckRepository.AddAsync(truck);
            return truck.Id.Value;
        }
    }
}
