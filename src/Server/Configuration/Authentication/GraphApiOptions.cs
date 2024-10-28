using System.ComponentModel.DataAnnotations;

namespace Etn.MyLittleBoard.Server.Configuration.Authentication;

public sealed class GraphApiOptions
{
    public static readonly string Key = "GraphApi";

    [Required(AllowEmptyStrings = false)]
    public required string BaseUrl { get; set; }

    [Required]
    public required string[] Scopes { get; set; }
}
