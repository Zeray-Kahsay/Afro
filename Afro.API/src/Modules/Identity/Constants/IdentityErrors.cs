using Afro.API.src.BuildingBlocks.Results;

namespace Afro.API.src.Modules.Identity.Constants;

public static class IdentityErrors
{
    public static readonly Error PhoneAlreadyExists = new("Identity.InvalidCredentials", "Invalid phone number or password.");
    public static readonly Error InvalidCredentials = new("Identity.InvalidCredentials", "Invalid phone number or password.");
    public static readonly Error InvalidRefreshToken = new("Identity.InvalidCredentials", "Refresh token is invalid");
}
