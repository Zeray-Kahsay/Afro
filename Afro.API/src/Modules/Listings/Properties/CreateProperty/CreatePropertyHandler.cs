using Afro.API.Modules.BuildingBlocks.Results;
using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Afro.API.src.Modules.Listings.Domain;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Properties.CreateProperty;

public sealed class CreatePropertyHandler(AppDbContext dbContext)
{
    public async Task<Result<PropertyRespose>> HandleAsync(
        CreatePropertyCommand command, CancellationToken ct)
    {
        var ownerExists = await dbContext.Owners.AnyAsync(
            x => x.Id == command.OwnerId,
            ct);
        
        if (!ownerExists)
        {
            return Result<PropertyRespose>.Failure(ListingErrors.OwnerNotFound);
        }

        var property = Property.Create(
                command.OwnerId,
                command.Title,
                command.Description,
                command.Price,
                command.Purpose,
                command.Type,
                command.Bedrooms,
                command.Bathrooms,
                command.Area,
                command.Country,
                command.City,
                command.AddressLine);
        
        await dbContext.SaveChangesAsync(ct);

        return Result<PropertyRespose>.Success(
                new PropertyRespose(
                    property.Id,
                    property.OwnerId,
                    property.Title,
                    property.Price,
                    property.Purpose,
                    property.Type,
                    property.Status,
                    property.Country,
                    property.City));
    }
}
