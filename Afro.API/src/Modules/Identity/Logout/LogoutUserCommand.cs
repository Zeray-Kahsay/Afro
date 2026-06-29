namespace Afro.API.src.Modules.Identity.Logout;

/*
    Why pass refresh token: When the user logs out, frontend sends a Refresh token
    Backend Revokes it
    after that POST /api/auth/refresh
    will fail
*/
public sealed record LogoutUserCommand(string RefreshToken);

