using CustomERP.Trucks.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomERP.Trucks.Infrastructure
{
    public class TrucksDbContext : DbContext
    {
        public DbSet<Truck> Trucks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "TrucksDb");            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var truckEntity = modelBuilder.Entity<Truck>();

            truckEntity.HasKey(t => t.Id);
            truckEntity.HasIndex(t => t.Code).IsUnique();

            truckEntity.Property(x => x.Id)
                .HasConversion(id => id.Value, value => new TruckId(value));

            truckEntity.Property(x => x.Code)
                .HasConversion(code => code.Value, value => TruckCode.Create(value));

            truckEntity.Property(x => x.Name);
            truckEntity.Property(x => x.Description);
            truckEntity.Property(x => x.UsageStatus);
        }
    }
}
