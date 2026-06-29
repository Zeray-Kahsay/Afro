using Afro.API.Modules.BuildingBlocks.Authentication;
using Afro.API.src.BuildingBlocks.Authentication;
using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Identity.Constants;
using Afro.API.src.Modules.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Afro.API.src.Modules.Identity.LoginUser;

public sealed class LoginUserHandler(
    UserManager<AppUser> userManager,
    AppDbContext dbContext,
    IJwtProvider jwtProvider,
    IRefreshTokenProvider refreshTokenProvider,
    IOptions<JwtOptions> jwtOptions
)
{
    public async Task<Result<AuthResponse>> HandleAsync(LoginUserCommand command, CancellationToken ct)
    {
        var normalizedPhone = command.PhoneNumber.Trim();

        var user = await userManager.FindByNameAsync(normalizedPhone);

        if (user is null)
        {
            return Result<AuthResponse>.Failure(IdentityErrors.InvalidCredentials);
        }

        var validPassword = await userManager.CheckPasswordAsync(user, command.Password);

        if (!validPassword)
        {
            return Result<AuthResponse>.Failure(IdentityErrors.InvalidCredentials);
        }

        var roles = await userManager.GetRolesAsync(user);

        var accessToken = jwtProvider.GenerateAccessToken(user, roles);

        var refreshTokenValue = refreshTokenProvider.GenerateRefreshToken();

        var refreshAccessTokenEntity = Domain.RefreshAccessToken.Create(user.Id, refreshTokenValue, DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenDays));

        dbContext.RefreshAccessTokens.Add(refreshAccessTokenEntity);

        await dbContext.SaveChangesAsync(ct);
        

        return Result<AuthResponse>.Success(new AuthResponse(accessToken, refreshAccessTokenEntity.Token, DateTime.UtcNow.AddMinutes(jwtOptions.Value.AccessTokenMinutes)));
    }
}
