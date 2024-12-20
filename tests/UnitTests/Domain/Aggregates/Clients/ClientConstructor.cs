using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Clients;

public sealed class ClientConstructor
{
    private readonly Fixture fixture = new();

    [Fact]
    public void Should_Initialize_Client_MandatoryProperties()
    {
        string name = this.fixture.Create<string>();
        Client client = new(ClientName.From(name), ClientNote.Unspecified);
        client.Name.Value.Should().Be(name);
        Assert.Equal(ClientNote.Unspecified, client.Note);
        Assert.Equal(ClientState.Enabled, client.State);
    }

    [Fact]
    public void Should_Initialize_Client_Properties()
    {
        string name = this.fixture.Create<string>();
        string note = this.fixture.Create<string>();
        Client client = new(ClientName.From(name), ClientNote.From(note));
        client.Name.Value.Should().Be(name);
        client.Note.Value.Should().Be(note);
        Assert.Equal(ClientState.Enabled, client.State);
    }
}
