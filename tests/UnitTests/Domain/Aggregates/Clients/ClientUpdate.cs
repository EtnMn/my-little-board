using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Clients;

public sealed class ClientUpdate
{
    private readonly Fixture fixture = new();
    private readonly Client client;

    public ClientUpdate()
    {
        this.client = new(
            ClientName.From(this.fixture.Create<string>()),
            ClientNote.Unspecified);
    }

    [Fact]
    public void Should_Update_ClientName()
    {
        string name = this.fixture.Create<string>();
        this.client.UpdateName(ClientName.From(name));
        this.client.Name.Value.Should().Be(name);
    }

    [Fact]
    public void Should_Update_ClientNote()
    {
        string note = this.fixture.Create<string>();
        this.client.UpdateNote(ClientNote.From(note));
        this.client.Note.Value.Should().Be(note);
    }

    [Fact]
    public void Should_Enable_ClientState()
    {
        this.client.Enable();
        this.client.State.Value.Should().Be(true);
    }

    [Fact]
    public void Should_Disable_ClientState()
    {
        this.client.Disable();
        this.client.State.Value.Should().Be(false);
    }
}
