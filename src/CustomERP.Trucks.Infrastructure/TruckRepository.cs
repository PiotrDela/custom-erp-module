using CustomERP.Domain.SeedWork;
using CustomERP.Trucks.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomERP.Trucks.Infrastructure
{
    public class TruckRepository : ITruckRepository, ITruckCodeUniquenessConstraint
    {
        public async Task AddAsync(Truck entity)
        {
            using (var context = new TrucksDbContext())
            {
                context.Trucks.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Truck entity)
        {
            using (var context = new TrucksDbContext())
            {
                context.Trucks.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Truck> GetByIdAsync(TruckId id)
        {
            using (var context = new TrucksDbContext())
            {
                return await context.Trucks.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public bool IsInUse(TruckCode code)
        {
            using (var context = new TrucksDbContext())
            {
                return context.Trucks.Any(x => x.Code == code);
            }
        }

        public Task UpdateAsync(Truck entity)
        {
            throw new NotImplementedException();
        }
    }
}
