namespace CustomERP.Domain.Trucks;

public enum TrackUsageStatus
{
    OutOfService = 0,
    Loading = 1,
    ToJob = 2,
    AtJob = 3,
    Returning = 4
}