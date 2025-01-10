using MudBlazor;

namespace Etn.MyLittleBoard.Server.Components.Shared.DashboardCards;

public sealed class Card
{
    public required string Name { get; set; }

    public string Text { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;

    public Color Color { get; set; } = Color.Inherit;
}
