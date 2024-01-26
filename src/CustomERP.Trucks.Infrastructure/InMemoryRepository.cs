using CustomERP.Trucks.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace CustomERP.Trucks.Infrastructure
{
    public class InMemoryRepository : ITruckRepository
    {
        private readonly IMemoryCache memoryCache;

        public InMemoryRepository(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public Task AddAsync(Truck entity)
        {
            this.memoryCache.Set(entity.Id, entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Truck entity)
        {
            this.memoryCache.Remove(entity.Id);
            return Task.CompletedTask;
        }

        public Task<Truck> GetByIdAsync(TruckId id)
        {
            if (this.memoryCache.TryGetValue<Truck>(id, out var truck))
            {
                return Task.FromResult(truck);
            }

            return Task.FromResult((Truck)null);
        }

        public Task UpdateAsync(Truck entity)
        {
            this.memoryCache.Set(entity.Id, entity);
            return Task.CompletedTask;
        }
    }
}
