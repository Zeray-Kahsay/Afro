using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Afro.API.src.Modules.Listings.Properties.CreateProperty;

public static class CreatePropertyEndpoint
{
    public static RouteGroupBuilder MapCreateProperty(this RouteGroupBuilder group)
    {
        group.MapPost(
                "/properties",
                [Authorize(
                    Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
                )]
        async (
            CreatePropertyCommand command,
            [FromServices] CreatePropertyValidator validator,
            [FromServices] CreatePropertyHandler handler,
            CancellationToken ct
            ) =>
         {
             await validator.ValidateAndThrowAsync(command, ct);

             var result = await handler.HandleAsync(command, ct);

             return result.ToApiResult();
         });

        return group;
    }
}
