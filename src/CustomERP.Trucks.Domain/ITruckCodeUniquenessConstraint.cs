namespace CustomERP.Trucks.Domain;

public interface ITruckCodeUniquenessConstraint
{
    bool IsInUse(TruckCode code);
}

