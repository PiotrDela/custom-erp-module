using CustomERP.Trucks.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomERP.Trucks.Infrastructure
{
    public class TruckRepository : ITruckRepository, ITruckCodeUniquenessConstraint
    {
        private readonly TrucksDbContext dbContext;

        public TruckRepository(TrucksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(Truck entity)
        {
            this.dbContext.Trucks.Add(entity);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Truck entity)
        {
            this.dbContext.Trucks.Remove(entity);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<Truck> GetByIdAsync(TruckId id)
        {
            return await this.dbContext.Trucks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool IsInUse(TruckCode code)
        {
            return this.dbContext.Trucks.Any(x => x.Code == code);
        }

        public async Task UpdateAsync(Truck entity)
        {
            this.dbContext.Trucks.Entry(entity).State = EntityState.Modified;
            await this.dbContext.SaveChangesAsync();
        }
    }
}
