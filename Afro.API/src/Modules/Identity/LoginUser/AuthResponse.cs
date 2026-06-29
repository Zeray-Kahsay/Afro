namespace Afro.API.src.Modules.Identity.LoginUser;

public sealed record AuthResponse(string AccessToken, string RefreshToken, DateTime ExpiresAtUtc);

