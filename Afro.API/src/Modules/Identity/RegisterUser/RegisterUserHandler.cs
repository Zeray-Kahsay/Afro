using Afro.API.Modules.BuildingBlocks.Results;
using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using Afro.API.src.Modules.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Identity.RegisterUser;

public sealed class RegisterUserHandler(UserManager<AppUser> userManager)
{
    public async Task<Result> HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var normalizedPhone = command.PhoneNumber.Trim();

        var existingUser = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == normalizedPhone, cancellationToken);
        if (existingUser is not null)
        {
            return Result.Failure(IdentityErrors.PhoneAlreadyExists);
        }

        var user = AppUser.Create(normalizedPhone);

        var createResult = await userManager.CreateAsync(user, command.Password);

        if (!createResult.Succeeded)
        {
            return Result.Failure(new Error("Identity.CreateUserFailed", string.Join
                         (" ,", createResult.Errors.Select(e => e.Description))));
        }

        await userManager.AddToRoleAsync(user, RoleNames.Customer);

        return Result.Success();
    }
}
