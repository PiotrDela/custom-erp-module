using CustomERP.Domain.Trucks;
using CustomERP.Trucks.Domain;

namespace CustomERP.Tests.Trucks;

public class TruckFactory
{
    public static Truck Create(TrackUsageStatus usageStatus = TrackUsageStatus.OutOfService)
    {
        return Truck.CreateNew("ABC123", "Ford transit", "Has extra space in the back", usageStatus);
    }
}
