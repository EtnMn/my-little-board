using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using MudBlazor;

namespace Etn.MyLittleBoard.Server.Extensions;

public static class ProjectStatusExtension
{
    public static Color GetColor(this ProjectStatus value)
    {
        return value switch
        {
            ProjectStatus.Draft => Color.Warning,
            ProjectStatus.Active => Color.Primary,
            ProjectStatus.Inactive => Color.Secondary,
            ProjectStatus.Complete => Color.Success,
            _ => Color.Default,
        };
    }
}
