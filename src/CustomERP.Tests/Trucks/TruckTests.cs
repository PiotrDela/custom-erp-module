using CustomERP.Domain.Exceptions;
using CustomERP.Domain.Trucks;
using Xunit;

namespace CustomERP.Tests.Trucks;

public class TruckTests
{
    public static readonly IEnumerable<object[]> AllAvailableStatuses = Enum.GetValues<TrackUsageStatus>().Select(x => new object[] { x });

    [Theory]
    [MemberData(nameof(AllAvailableStatuses))]
    public void OutOfServiceStatusCanBeSetRegardlessOfTheTrackCurrentStatus(TrackUsageStatus currentStatus)
    {
        var truck = TruckFactory.Create(currentStatus);

        truck.ChangeStatus(TrackUsageStatus.OutOfService);

        Assert.Equal(TrackUsageStatus.OutOfService, truck.UsageStatus);
    }

    [Theory]
    [MemberData(nameof(AllAvailableStatuses))]
    public void EachStatusCanBeSetCurrentStatusIsOutOfService(TrackUsageStatus newStatus)
    {
        var truck = TruckFactory.Create(TrackUsageStatus.OutOfService);

        truck.ChangeStatus(newStatus);

        Assert.Equal(newStatus, truck.UsageStatus);
    }

    [Theory]
    [InlineData(TrackUsageStatus.Loading, TrackUsageStatus.ToJob)]
    [InlineData(TrackUsageStatus.ToJob, TrackUsageStatus.AtJob)]
    [InlineData(TrackUsageStatus.AtJob, TrackUsageStatus.Returning)]
    [InlineData(TrackUsageStatus.Returning, TrackUsageStatus.Loading)]
    public void ChangingStatusAccordingToAllowedOrderTests(TrackUsageStatus currentStatus, TrackUsageStatus newStatus)
    {
        var truck = TruckFactory.Create(currentStatus);

        truck.ChangeStatus(newStatus);

        Assert.Equal(newStatus, truck.UsageStatus);
    }

    [Theory]
    [MemberData(nameof(InvalidNewStatusTestCases))]
    public void ChangingStatusToNotAllowedTests(TrackUsageStatus currentStatus, TrackUsageStatus newStatus)
    {
        var truck = TruckFactory.Create(currentStatus);

        Assert.Throws<BusinessRuleViolationException>(() => truck.ChangeStatus(newStatus));
    }

    public static IEnumerable<object[]> InvalidNewStatusTestCases()
    {
        var enumValues = Enum.GetValues<TrackUsageStatus>().Except(new[] { TrackUsageStatus.OutOfService });

        var currentStatus = TrackUsageStatus.Loading;
        foreach (var invalidStatus in enumValues.Except(new[] { TrackUsageStatus.ToJob }))
        {
            yield return new object[] { currentStatus, invalidStatus };
        }

        currentStatus = TrackUsageStatus.ToJob;
        foreach (var invalidStatus in enumValues.Except(new[] { TrackUsageStatus.AtJob }))
        {
            yield return new object[] { currentStatus, invalidStatus };
        }

        currentStatus = TrackUsageStatus.AtJob;
        foreach (var invalidStatus in enumValues.Except(new[] { TrackUsageStatus.Returning }))
        {
            yield return new object[] { currentStatus, invalidStatus };
        }

        currentStatus = TrackUsageStatus.Returning;
        foreach (var invalidStatus in enumValues.Except(new[] { TrackUsageStatus.Loading }))
        {
            yield return new object[] { currentStatus, invalidStatus };

        }

        yield break;
    }
}
