using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Clients;

public sealed class ClientNoteFrom
{
    private readonly Fixture fixture = new();

    [Fact]
    public void Should_Initialize_ClientNote()
    {
        string value = this.fixture.Create<string>();
        ClientNote clientNote = ClientNote.From(value);
        clientNote.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_Be_Unspecified_When_ClientNote_IsNullOrEmpty(string? value)
    {
        ClientNote clientNote = ClientNote.From(value!);
        Assert.Equal(ClientNote.Unspecified, clientNote);
    }

    [Fact]
    public void Should_ThrowException_When_ClientNote_ExceedsMaxLength()
    {
        string value = StringHelpers.GenerateOverMaximumLengthString(ValidationConstants.LongTextLength);
        Action action = () => ClientNote.From(value);
        action.Should().Throw<ValueObjectValidationException>();
    }

    [Theory]
    [InlineData(" x")]
    [InlineData("x ")]
    [InlineData(" x ")]
    public void Should_Trim_ClientNote_Value(string value)
    {
        ClientNote clientNote = ClientNote.From(value);
        clientNote.Value.Should().Be(value.Trim());
    }
}
