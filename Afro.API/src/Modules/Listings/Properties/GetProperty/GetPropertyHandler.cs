using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Properties.GetProperty;

public sealed class GetPropertyHandler(AppDbContext dbContext)
{
    public async Task<Result<PropertyDetailsResponse>> HandleAsync(Guid propertyId, CancellationToken ct)
    {
        var property = await dbContext.Properties
                    .AsNoTracking()
                    .Include(x => x.Owner)
                    .Include(x => x.Images)
                    .FirstOrDefaultAsync(x => x.Id == propertyId, ct);

        if (property is null)
        {
            return Result<PropertyDetailsResponse>.Failure(ListingErrors.PropertyNotFound);
        }

        return Result<PropertyDetailsResponse>.Success(
            new PropertyDetailsResponse(
                property.Id,
                property.OwnerId,
                property.Owner.FullName,
                property.Owner.PhoneNumber,
                property.Title,
                property.Description,
                property.Price,
                property.Purpose,
                property.Type,
                property.Status,
                property.Bedrooms,
                property.Bathrooms,
                property.Area,
                property.Country,
                property.City,
                property.AddressLine,
                property.Images
                    .OrderBy(x => x.SortOrder)
                    .Select(x => new PropertyImageResponse(
                        x.Id,
                        x.Url,
                        x.IsCover,
                        x.SortOrder))
                    .ToList()));
    }
}