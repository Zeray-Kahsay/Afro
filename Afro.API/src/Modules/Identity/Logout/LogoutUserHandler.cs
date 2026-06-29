using Afro.API.Modules.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Identity.Constants;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Identity.Logout;

public sealed class LogoutUserHandler(AppDbContext dbContext)
{
    public async Task<Result> HandleLogoutAsync(LogoutUserCommand command, CancellationToken ct)
    {
        var refreshToken = await dbContext.RefreshAccessTokens.FirstOrDefaultAsync(
         r => r.Token == command.RefreshToken, ct);

        if (refreshToken is null)
        {
            return Result.Failure(IdentityErrors.InvalidRefreshToken);
        }

        if (!refreshToken.IsActive)
        {
            return Result.Success();
        }

        refreshToken.Revoke();

        await dbContext.SaveChangesAsync(ct);

        return Result.Success();

        /*
        Why Return Success if Already revoked
        consider logout -> Network Retry -> Logout again
        we do not want 400 Bad Request
        for a logout that already succeeded.
        Logout should be idempotent
        */
    }

}
