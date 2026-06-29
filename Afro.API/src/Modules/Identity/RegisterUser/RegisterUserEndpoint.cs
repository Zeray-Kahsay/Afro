using Afro.API.src.BuildingBlocks.Results;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Afro.API.src.Modules.Identity.RegisterUser;

public static class RegisterUserEndpoint
{
    public static RouteGroupBuilder MapRegisterUser(this RouteGroupBuilder group)
    {
        group.MapPost(
       "/registerUser",
       async (
           RegisterUserCommand command,
           [FromServices] RegisterUserValidator validator,
           [FromServices] RegisterUserHandler handler,
           CancellationToken ct) =>
       {
           await validator.ValidateAndThrowAsync(command, ct);


           var result = await handler.HandleAsync(command, ct);

           return result.ToApiResult();
       });

        return group;
    }
}
