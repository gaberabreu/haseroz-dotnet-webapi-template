using Haseroz.WebApiTemplate.Web.Extensions;

namespace Haseroz.WebApiTemplate.UnitTests.Web.Services;

public class KebabCaseTransformerTests
{
    private readonly KebabCaseTransformer _transformer = new();

    [Theory]
    [InlineData("TestString", "test-string")]
    [InlineData("AnotherExample", "another-example")]
    [InlineData("SimpleTest", "simple-test")]
    [InlineData("NoChange", "no-change")]
    [InlineData("alllowercase", "alllowercase")]
    [InlineData("UPPERCASE", "uppercase")]
    [InlineData("", null)]
    [InlineData(null, null)]
    public void GIVEN_StringInput_THEN_InputIsConvertToKebabCase(string? input, string? expected)
    {
        // Act
        var result = _transformer.TransformOutbound(input);

        // Assert
        Assert.Equal(expected, result); 
    }
}
