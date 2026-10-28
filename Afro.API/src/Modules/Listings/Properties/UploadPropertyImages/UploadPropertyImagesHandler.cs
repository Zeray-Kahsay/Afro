using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Properties.UploadPropertyImages;

public class UploadPropertyImagesHandler(AppDbContext dbContext)
{
    public async Task<Result<UploadPropertyImagesResponse>> HandleAsync(
        Guid propertyId,
        UploadPropertyImagesCommand command,
        CancellationToken ct)
    {
        var property = await dbContext.Properties
                            .Include(x => x.Images)
                            .FirstOrDefaultAsync(x => x.Id == propertyId,
                                ct);
        
        if (property is null)
        {
            return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.PropertyNotFound);
        }

        if (property.Status == Domain.ListingStatus.Archived)
        {
            return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.ArchivedPropertyCannotBeUpdated);
        }

        // foreach (var image in command.Images)
        // {
        //     property.AddImage(
        //         image.Url,
        //         image.StorageProvider,
        //         image.StorageKey,   
        //         string.Empty,
        //         property.Images.Count);
        // }

        // TODO: Implement batch behavior and error mapping for the upload handler. This may include handling cases where some images fail to upload while others succeed, and returning appropriate error messages or statuses for each image.
        // upload handler's bach behavior and error mapping


        await dbContext.SaveChangesAsync(ct);

        return Result<UploadPropertyImagesResponse>.Success(
            new UploadPropertyImagesResponse(property.Id, property.Images.Count));
    }
}
