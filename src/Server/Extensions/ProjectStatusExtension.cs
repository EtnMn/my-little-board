using Etn.MyLittleBoard.Domain.Aggregates.Projects;
using MudBlazor;

namespace Etn.MyLittleBoard.Server.Extensions;

public static class ProjectStatusExtension
{
    public static Color GetColor(this ProjectStatus value)
    {
        return value switch
        {
            ProjectStatus.Draft => Color.Secondary,
            ProjectStatus.Inactive => Color.Warning,
            ProjectStatus.Complete => Color.Dark,
            _ => Color.Default,
        };
    }
}
