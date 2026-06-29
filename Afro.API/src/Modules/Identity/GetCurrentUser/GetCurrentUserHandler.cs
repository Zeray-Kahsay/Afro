using Afro.API.Modules.BuildingBlocks.Results;
using Afro.API.src.BuildingBlocks.Authentication;
using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Afro.API.src.Modules.Identity.GetCurrentUser;

public sealed class GetCurrentUserHandler(ICurrentUser currentUser, UserManager<AppUser> userManager)
{
    public async Task<Result<CurrentUserResponse>> HandleCurrentUserAsync(CancellationToken ct)
    {
        if (!currentUser.IsAuthenticated)
        {
            return Result<CurrentUserResponse>.Failure(
                new Error("Identity.Unauthorized", "User is not authorized")
            );
        }

        var user = await userManager.FindByIdAsync(currentUser.UserId.ToString());

        if (user is null)
        {
            return Result<CurrentUserResponse>.Failure(
                new Error("Identity.UserNotFound", "User was not found")
            );
        }

        var roles = await userManager.GetRolesAsync(user);

        return Result<CurrentUserResponse>.Success(
            new CurrentUserResponse(user.Id, user.PhoneNumber!, [.. roles])
        );
    }
}
