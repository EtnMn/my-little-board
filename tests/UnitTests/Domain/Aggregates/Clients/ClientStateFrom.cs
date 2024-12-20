using Etn.MyLittleBoard.Domain.Aggregates.Clients;

namespace Etn.MyLittleBoard.UnitTests.Domain.Aggregates.Clients;

public sealed class ClientStateFrom
{
    [Fact]
    public void Should_Initialize_Enabled_ClientState()
    {
        ClientState clientState = ClientState.From(true);
        Assert.Equal(ClientState.Enabled, clientState);
    }

    [Fact]
    public void Should_Initialize_Disabled_ClientState()
    {
        ClientState clientState = ClientState.From(false);
        Assert.Equal(ClientState.Disabled, clientState);
    }
}
