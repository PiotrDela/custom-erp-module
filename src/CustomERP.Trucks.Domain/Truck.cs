using CustomERP.Domain.Exceptions;
using CustomERP.Domain.SeedWork;
using CustomERP.Domain.Trucks;

namespace CustomERP.Trucks.Domain;

public class Truck: Entity
{
    public static Truck Create(string code, string name, ITruckCodeUniquenessConstraint uniqueConstraint, string description = null, TrackUsageStatus usageStatus = TrackUsageStatus.OutOfService)
    {
        var truckCode = TruckCode.Create(code);

        if (uniqueConstraint.IsInUse(truckCode))
        {
            throw new DuplicatedEntityException("Truck code already in use");
        }

        var truckId = new TruckId(Guid.NewGuid());
        return new Truck(truckId, truckCode, name, description, usageStatus);
    }

    public TruckId Id { get; }
    public TruckCode Code { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public TrackUsageStatus UsageStatus { get; private set; }

    private Truck(TruckId id, TruckCode code, string name, string description, TrackUsageStatus usageStatus)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"{nameof(name)} cannot be null nor empty", nameof(name));
        }

        Id = id ?? throw new ArgumentNullException(nameof(id));
        Code = code ?? throw new ArgumentNullException($"{nameof(code)}");
        Name = name;
        Description = description ?? string.Empty;
        UsageStatus = usageStatus;
    }

    public void ChangeStatus(TrackUsageStatus status)
    {
        if (status == TrackUsageStatus.OutOfService || UsageStatus == TrackUsageStatus.OutOfService)
        {
            UsageStatus = status;
            return;
        }

        switch (UsageStatus)
        {
            case TrackUsageStatus.Loading:
                if (status != TrackUsageStatus.ToJob) throw new BusinessRuleViolationException();
                break;
            case TrackUsageStatus.ToJob:
                if (status != TrackUsageStatus.AtJob) throw new BusinessRuleViolationException();
                break;
            case TrackUsageStatus.AtJob:
                if (status != TrackUsageStatus.Returning) throw new BusinessRuleViolationException();
                break;
            case TrackUsageStatus.Returning:
                if (status != TrackUsageStatus.Loading) throw new BusinessRuleViolationException();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(status));
        }

        UsageStatus = status;
    }

    public void ChangeCode(string newCode, ITruckCodeUniquenessConstraint uniqueConstraint)
    {
        var truckCode = TruckCode.Create(newCode);

        if (uniqueConstraint.IsInUse(truckCode))
        {
            throw new DuplicatedEntityException("Truck code already in use");
        }

        this.Code = truckCode;
    }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"{nameof(name)} cannot be null nor empty", nameof(name));
        }

        this.Name = name;
    }

    public void ChangeDescription(string description)
    {
        this.Description = description;
    }
}

