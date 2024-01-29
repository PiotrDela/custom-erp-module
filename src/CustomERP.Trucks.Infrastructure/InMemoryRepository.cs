using CustomERP.Trucks.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace CustomERP.Trucks.Infrastructure
{
    public class InMemoryRepository : ITruckRepository, ITruckCodeUniquenessConstraint
    {
        private readonly IMemoryCache memoryCache;
        private static HashSet<Truck> trucks = new HashSet<Truck>();

        public InMemoryRepository(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public Task AddAsync(Truck entity)
        {
            this.memoryCache.Set(entity.Id, entity);
            this.memoryCache.Set(entity.Code, entity);
            trucks.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Truck entity)
        {
            this.memoryCache.Remove(entity.Id);
            this.memoryCache.Remove(entity.Code);

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

        public bool IsInUse(TruckCode code)
        {
            return this.memoryCache.TryGetValue(code, out var truck);
        }

        public Task UpdateAsync(Truck entity)
        {
            this.memoryCache.Set(entity.Id, entity);
            this.memoryCache.Set(entity.Code, entity);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Truck>> GetAllAsync()
        {
            return Task.FromResult(trucks.AsEnumerable());
        }
    }
}
