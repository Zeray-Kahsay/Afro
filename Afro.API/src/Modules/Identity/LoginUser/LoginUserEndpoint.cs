using Afro.API.src.BuildingBlocks.Results;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Afro.API.src.Modules.Identity.LoginUser;

public static class LoginUserEndpoint
{
    public static RouteGroupBuilder MapLoginUser(this RouteGroupBuilder group)
    {
        group.MapPost("/loginUser", async (
            LoginUserCommand command,
            [FromServices] LoginUserValidator validator,
            [FromServices] LoginUserHandler handler,
            CancellationToken ct) =>
        {
            await validator.ValidateAndThrowAsync(command, ct);

            var result = await handler.HandleAsync(command, ct);

            return result.ToApiResult();
        });

        return group;
    }
}

