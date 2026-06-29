namespace Afro.API.src.Modules.Identity.Domain;

public sealed class RefreshAccessToken
{
    public Guid Id { get; private set; }
    public Guid  UserId { get; private set; }
    public string Token { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiresAtUtc { get; private set; }
    public DateTime? RevokedAtUtc { get; private set; }
    public AppUser User { get; private set; } = null!;

    private RefreshAccessToken() { }

    public static RefreshAccessToken Create(Guid userId, string token, DateTime expiresAtUtc)
    {
        return new RefreshAccessToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            CreatedAt = DateTime.UtcNow,
            ExpiresAtUtc = expiresAtUtc
        };
    }

    public void Revoke()
    {
        RevokedAtUtc = DateTime.UtcNow;
    }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;
    public bool IsActive => RevokedAtUtc is null && !IsExpired;
}
