using CustomERP.Domain.Exceptions;
using CustomERP.Trucks.Domain;

namespace CustomERP.Trucks.Application.DeleteTruck
{
    public class DeleteTruckCommandHandler : ICommandHandler<DeleteTruckCommand>
    {
        private readonly ITruckRepository truckRepository;

        public DeleteTruckCommandHandler(ITruckRepository truckRepository)
        {
            this.truckRepository = truckRepository ?? throw new ArgumentNullException(nameof(truckRepository));
        }

        public async Task Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
        {
            var truck = await truckRepository.GetByIdAsync(new TruckId(request.TruckId));
            if (truck == null)
            {
                throw new EntityNotFoundException($"{nameof(Truck)} has not been found");
            }
            await truckRepository.DeleteAsync(truck);
        }
    }
}
