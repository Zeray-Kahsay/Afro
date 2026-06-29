using System.Security.Claims;

namespace Afro.API.src.BuildingBlocks.Authentication;

public sealed class CurrentUser (IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public Guid UserId
    {
        get
        {
            var value = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(value, out var userId) ? userId : Guid.Empty;
        }
    }

    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true;

    public string? PhoneNumber => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.MobilePhone);
}
