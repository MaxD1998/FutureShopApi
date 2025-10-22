using Shared.Shared.Extensions;

namespace Shared.Test.Shared.Extensions;

public class StringExtensionTest
{
    [Theory]
    [InlineData("[\"apple\",\"banana\",\"cherry\"]", new[] { "apple", "banana", "cherry" })]
    [InlineData("[]", new string[0])]
    [InlineData("[\"\"]", new[] { "" })]
    [InlineData(null, new string[0])]
    [InlineData("custom_string", new string[0])]
    public void ToListString_ValidJsonArray_ReturnList(string input, string[] expected)
    {
        // Act
        var result = input.ToListString();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("11c43ee8-b9d3-4e51-b73f-bd9dda66e29c")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public void ToNullableGuid_InputIsGuidString_ReturnGuid(string input)
    {
        // Act
        var result = input.ToNullableGuid();

        // Assert
        Assert.IsType<Guid>(result);
        Assert.NotNull(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("custom_string")]
    public void ToNullableGuid_InputIsNullOrEmptyOrNotGuidString_ReturnNull(string input)
    {
        // Act
        var result = input.ToNullableGuid();

        // Assert
        Assert.Null(result);
    }
}