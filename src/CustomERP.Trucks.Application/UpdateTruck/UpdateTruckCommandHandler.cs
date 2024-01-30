using CustomERP.Domain.Exceptions;
using CustomERP.Trucks.Domain;

namespace CustomERP.Trucks.Application.UpdateTruck
{
    public class UpdateTruckCommandHandler : ICommandHandler<UpdateTruckCommand>
    {
        private readonly ITruckRepository truckRepository;
        private readonly ITruckCodeUniquenessConstraint uniqueConstraint;

        public UpdateTruckCommandHandler(ITruckRepository truckRepository, ITruckCodeUniquenessConstraint uniqueConstraint)
        {
            this.truckRepository = truckRepository ?? throw new ArgumentNullException(nameof(truckRepository));
            this.uniqueConstraint = uniqueConstraint ?? throw new ArgumentNullException(nameof(uniqueConstraint));
        }

        public async Task Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
        {
            var truck = await truckRepository.GetByIdAsync(new TruckId(request.TruckId));
            if (truck == null)
            {
                throw new EntityNotFoundException($"{nameof(Truck)} has not been found");
            }

            if (string.IsNullOrEmpty(request.Code) == false)
            {
                truck.ChangeCode(request.Code, uniqueConstraint);
            }

            if (string.IsNullOrEmpty(request.Name) == false)
            {
                truck.ChangeName(request.Name);
            }

            if (string.IsNullOrWhiteSpace(request.Description) == false)
            {
                truck.ChangeDescription(request.Description);
            }

            if (request.UsageStatus.HasValue)
            {
                truck.ChangeStatus(request.UsageStatus.Value);
            }

            await truckRepository.UpdateAsync(truck);
        }
    }
}
