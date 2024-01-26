using CustomERP.Domain.Exceptions;
using CustomERP.Domain.SeedWork;
using CustomERP.Domain.Trucks;

namespace CustomERP.Trucks.Domain;

public class Truck: Entity
{
    public static Truck CreateNew(string code, string name, string description = null, TrackUsageStatus usageStatus = TrackUsageStatus.OutOfService)
    {
        var truckId = new TruckId(Guid.NewGuid());
        var truckCode = TruckCode.Create(code);
        return new Truck(truckId, truckCode, name, description, usageStatus);
    }

    public TruckId Id { get; }
    public TruckCode Code { get; }
    public string Name { get; }
    public string Description { get; }
    public TrackUsageStatus UsageStatus { get; private set; }

    private Truck(TruckId id, TruckCode code, string name, string description, TrackUsageStatus? usageStatus)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"{nameof(name)} cannot be null nor empty", nameof(name));
        }

        Id = id ?? throw new ArgumentNullException(nameof(id));
        Code = code ?? throw new ArgumentNullException($"{nameof(code)}");
        Name = name;
        Description = description ?? string.Empty;
        UsageStatus = usageStatus ?? throw new ArgumentException($"{nameof(usageStatus)} cannot be null", nameof(usageStatus)); ;
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
                if (status != TrackUsageStatus.ToJob) throw new BusinessRuleValidationException();
                break;
            case TrackUsageStatus.ToJob:
                if (status != TrackUsageStatus.AtJob) throw new BusinessRuleValidationException();
                break;
            case TrackUsageStatus.AtJob:
                if (status != TrackUsageStatus.Returning) throw new BusinessRuleValidationException();
                break;
            case TrackUsageStatus.Returning:
                if (status != TrackUsageStatus.Loading) throw new BusinessRuleValidationException();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(status));
        }

        UsageStatus = status;
    }
}

