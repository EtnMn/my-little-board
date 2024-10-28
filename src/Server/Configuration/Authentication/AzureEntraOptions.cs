using System.ComponentModel.DataAnnotations;

namespace Etn.MyLittleBoard.Server.Configuration.Authentication;

public sealed class AzureEntraOptions
{
    public static readonly string Key = "Azure:Entra";

    [Required(AllowEmptyStrings = false)]
    public required string Instance { get; set; }

    [Required(AllowEmptyStrings = false)]
    public required string TenantId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public required string ClientId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public required string ClientSecret { get; set; }

    [Required(AllowEmptyStrings = false)]
    public required string CallbackPath { get; set; }
}
