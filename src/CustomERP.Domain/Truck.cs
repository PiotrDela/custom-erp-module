using CustomERP.Domain.Exceptions;

namespace CustomERP.Domain;

public class Truck
{
    public static Truck CreateNew(string code, string name, string description = null, TrackUsageStatus? usageStatus = TrackUsageStatus.OutOfService)
    {
        return new Truck(TruckCode.Create(code), name, description, usageStatus);
    }

    public TruckCode Code { get; }
    public string Name { get; }
    public string Description { get; }
    public TrackUsageStatus UsageStatus { get; private set; }

    private Truck(TruckCode code, string name, string description, TrackUsageStatus? usageStatus)
    {        
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"{nameof(name)} cannot be null nor empty" , nameof(name));
        }

        this.Code = code ?? throw new ArgumentNullException($"{nameof(code)}");
        this.Name = name;
        this.Description = description ?? string.Empty;
        this.UsageStatus = usageStatus ?? throw new ArgumentException($"{nameof(usageStatus)} cannot be null", nameof(usageStatus)); ;
    }

    public void ChangeStatus(TrackUsageStatus status)
    {
        if (status == TrackUsageStatus.OutOfService || this.UsageStatus == TrackUsageStatus.OutOfService)
        {
            this.UsageStatus = status;
            return;
        }

        switch (this.UsageStatus)
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

        this.UsageStatus = status;
    }
}
