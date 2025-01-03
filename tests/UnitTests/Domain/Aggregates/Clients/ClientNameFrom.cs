using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Clients;

public sealed class ClientNameFrom
{
    private readonly Fixture fixture = new();

    [Fact]
    public void Should_Initialize_ClientName()
    {
        string value = this.fixture.Create<string>();
        ClientName clientName = ClientName.From(value);
        clientName.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_ThrowException_When_ClientName_IsEmpty(string? value)
    {
        Action action = () => ClientName.From(value!);
        action.Should().Throw<ValueObjectValidationException>();
    }

    [Fact]
    public void Should_ThrowException_When_ClientName_ExceedsMaxLength()
    {
        string value = StringHelpers.GenerateOverMaximumLengthString(ValidationConstants.DefaultTextLength);
        Action action = () => ClientName.From(value);
        action.Should().Throw<ValueObjectValidationException>();
    }

    [Theory]
    [InlineData(" x")]
    [InlineData("x ")]
    [InlineData(" x ")]
    public void Should_Trim_ClientName_Value(string value)
    {
        ClientName clientName = ClientName.From(value);
        clientName.Value.Should().Be(value.Trim());
    }
}
