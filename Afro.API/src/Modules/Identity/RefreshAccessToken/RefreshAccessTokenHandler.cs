using Afro.API.Modules.BuildingBlocks.Authentication;
using Afro.API.Modules.BuildingBlocks.Results;
using Afro.API.src.BuildingBlocks.Authentication;
using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Identity.Constants;
using Afro.API.src.Modules.Identity.Domain;
using Afro.API.src.Modules.Identity.LoginUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Afro.API.src.Modules.Identity.RefreshAccessToken;

public sealed class RefreshAccessTokenHandler (
    AppDbContext dbContext,
    UserManager<AppUser> userManager,
    IJwtProvider jwtProvider,
    IRefreshTokenProvider refreshTokenProvider,
    IOptions<JwtOptions> jwtOptions
)
{
    public async Task<Result<AuthResponse>> HandleRefreshAccessTokenAsync(RefreshAccessTokenCommand command, CancellationToken ct)
    {
        var refreshToken = await dbContext.RefreshAccessTokens.FirstOrDefaultAsync(
                r => r.Token == command.RefreshToken, ct);
        
        if (refreshToken is null || !refreshToken.IsActive)
        {
            return Result<AuthResponse>.Failure(IdentityErrors.InvalidCredentials);
        }

        var user = await userManager.FindByIdAsync(refreshToken.UserId.ToString());
        if (user is null)
        {
            return Result<AuthResponse>.Failure(IdentityErrors.InvalidCredentials);
        }

        var roles = await userManager.GetRolesAsync(user);

        var accessToken = jwtProvider.GenerateAccessToken(user, roles);

        var newRefreshTokenValue = refreshTokenProvider.GenerateRefreshToken();

        var newRefreshToken = Domain.RefreshAccessToken.Create(
            user.Id,
            newRefreshTokenValue,
            DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenDays)
        );

        refreshToken.Revoke();

        dbContext.RefreshAccessTokens.Add(newRefreshToken);

        await dbContext.SaveChangesAsync(ct);

        return Result<AuthResponse>.Success(
            new AuthResponse(
                accessToken,
                newRefreshToken.Token,
                DateTime.UtcNow.AddMinutes(jwtOptions.Value.AccessTokenMinutes)
            )
        );


    }
}
