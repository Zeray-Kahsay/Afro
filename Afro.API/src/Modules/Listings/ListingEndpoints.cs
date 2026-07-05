using Afro.API.src.Modules.Listings.Owners.CreateOwner;
using Afro.API.src.Modules.Listings.Owners.GetOwner;
using Afro.API.src.Modules.Listings.Owners.SearchOwners;
using Afro.API.src.Modules.Listings.Owners.UpdateOwner;
using Afro.API.src.Modules.Listings.Properties.ArchiveProperty;
using Afro.API.src.Modules.Listings.Properties.CreateProperty;
using Afro.API.src.Modules.Listings.Properties.GetProperty;
using Afro.API.src.Modules.Listings.Properties.PublishProperty;
using Afro.API.src.Modules.Listings.Properties.SearchProperties;
using Afro.API.src.Modules.Listings.Properties.SetPropertyCoverImage;
using Afro.API.src.Modules.Listings.Properties.UpdateProperty;
using Afro.API.src.Modules.Listings.Properties.UploadPropertyImages;

namespace Afro.API.src.Modules.Listings;

public static class ListingEndpoints
{
    public static IEndpointRouteBuilder MapListingEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api");

        group.MapCreateOwner();

        group.MapGetOwner();

        group.MapUpdateOwner();

        group.MapSearchOwners();

        group.MapCreateProperty();

        group.MapUploadPropertyImages();

        group.MapSetPropertyCoverImage();

        group.MapGetProperty();

        group.MapPublishProperty();

        group.MapSearchProperties();

        group.MapArchiveProperty();

        group.MapUpdateProperty();


        return app;
    }
}
