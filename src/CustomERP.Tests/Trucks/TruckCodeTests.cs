using CustomERP.Trucks.Domain;
using Xunit;

namespace CustomERP.Tests.Trucks;

public class TruckCodeTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("ABC 123")]
    [InlineData("ABC123!")]
    [InlineData("ABC")]
    [InlineData("123")]
    public void CreateShouldThrowFormatExceptionWhenCodeHasInvalidFormat(string code)
    {
        Assert.Throws<FormatException>(() => TruckCode.Create(code));
    }

    [Fact]
    public void ShouldHaveCaseInsensitiveSemantics()
    {
        var code1 = TruckCode.Create("abc123");
        var code2 = TruckCode.Create("ABC123");

        Assert.Equal(code1, code2);
    }
}
