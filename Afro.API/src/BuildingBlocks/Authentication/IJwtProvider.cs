using Afro.API.src.Modules.Identity.Domain;

namespace Afro.API.src.BuildingBlocks.Authentication;

public interface IJwtProvider
{
    string GenerateAccessToken(AppUser user, IEnumerable<string> roles);
}
