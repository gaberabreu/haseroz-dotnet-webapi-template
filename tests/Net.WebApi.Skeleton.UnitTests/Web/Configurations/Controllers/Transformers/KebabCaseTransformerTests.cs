using Net.WebApi.Skeleton.Web.Configurations.Transformers;

namespace Net.WebApi.Skeleton.UnitTests.Web.Configurations.Controllers.Transformers;

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
    public void Given_StringInput_Then_InputIsConvertToKebabCase(string? input, string? expected)
    {
        // Act
        var result = _transformer.TransformOutbound(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
