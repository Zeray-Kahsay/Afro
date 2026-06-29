namespace Afro.API.Modules.BuildingBlocks.Authentication;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public int AccessTokenMinutes { get; set; }
    public int RefreshTokenDays { get; set; }
}
