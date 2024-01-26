using CustomERP.Domain.SeedWork;

namespace CustomERP.Trucks.Domain;

public interface ITruckRepository : IRepository<Truck>
{
    Task<Truck> GetByIdAsync(TruckId id);
}