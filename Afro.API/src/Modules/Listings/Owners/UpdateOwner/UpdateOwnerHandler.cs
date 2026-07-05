using Afro.API.Modules.BuildingBlocks.Results;
using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Owners.UpdateOwner;

public sealed class UpdateOwnerHandler(AppDbContext context)
{
    public async Task<Result> Handle(UpdateOwnerCommand command, CancellationToken ct)
    {
        var owner = await context.Owners
            .FirstOrDefaultAsync(x => x.Id == command.OwnerId, ct);

        if (owner is null)
        {
            return Result.Failure(ListingErrors.OwnerNotFound);
        }

        owner.Update(
            command.FullName,
            command.PhoneNumber,
            command.Email,
            command.Address,
            command.Notes

        );

        var phoneExists = await context.Owners.AnyAsync(
            x => x.Id != command.OwnerId && x.PhoneNumber == command.PhoneNumber, ct
        );

        if (phoneExists)
        {
            return Result.Failure(ListingErrors.OwnerPhoneAlreadyExists);
        }

        owner.Update(
            command.FullName,
            command.PhoneNumber,
            command.Email,
            command.Address,
            command.Notes
        );

        await context.SaveChangesAsync(ct);

        return Result.Success();

    }
}
