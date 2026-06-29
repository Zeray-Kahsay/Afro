using Microsoft.AspNetCore.Identity;

namespace Afro.API.src.Modules.Identity.Domain;

public class AppUser : IdentityUser<Guid>
{
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private readonly List<RefreshAccessToken> _refreshAccessTokens = [];

    public IReadOnlyCollection<RefreshAccessToken> RefreshAccessTokens => _refreshAccessTokens;

    private AppUser() { }

    public static AppUser Create(string phoneNumber)
    {
        return new AppUser
        {
            UserName = phoneNumber,
            PhoneNumber = phoneNumber
        };
    }
}
