using CustomERP.Domain;
using Xunit;

namespace CustomERP.Tests;

public class TruckCodeTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("ABC 123")]
    [InlineData("ABC123!")]
    public void CreateShouldThrowFormatExceptionWhenCodeHasInvalidFormat(string code)
    {
        Assert.Throws<FormatException>(() => TruckCode.Create(code));        
    }
}
