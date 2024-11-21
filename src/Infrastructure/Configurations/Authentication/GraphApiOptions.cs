using System.ComponentModel.DataAnnotations;

namespace Etn.MyLittleBoard.Infrastructure.Configurations.Authentication;

public sealed class GraphApiOptions
{
    public static readonly string Key = "GraphApi";

    [Required]
    public required string[] Scopes { get; set; }
}
