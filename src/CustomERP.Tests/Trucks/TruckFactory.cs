using CustomERP.Domain.Trucks;
using CustomERP.Trucks.Domain;

namespace CustomERP.Tests.Trucks;

public class TruckFactory
{
    public static Truck Create(TrackUsageStatus usageStatus = TrackUsageStatus.OutOfService)
    {
        return Truck.Create("ABC123", "Ford transit", new TruckCodeUniquenessConstraintStub(), "Has extra space in the back", usageStatus);
    }

    private class TruckCodeUniquenessConstraintStub : ITruckCodeUniquenessConstraint
    {
        public bool IsInUse(TruckCode code)
        {
            return false;
        }
    }
}
