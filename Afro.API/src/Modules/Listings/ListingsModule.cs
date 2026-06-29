using Afro.API.src.Modules.Listings.Owners.CreateOwner;
using Afro.API.src.Modules.Listings.Owners.GetOwner;
using Afro.API.src.Modules.Listings.Owners.SearchOwners;
using Afro.API.src.Modules.Listings.Properties.ArchiveProperty;
using Afro.API.src.Modules.Listings.Properties.CreateProperty;
using Afro.API.src.Modules.Listings.Properties.GetProperty;
using Afro.API.src.Modules.Listings.Properties.PublishProperty;
using Afro.API.src.Modules.Listings.Properties.SearchProperties;
using Afro.API.src.Modules.Listings.Properties.SetPropertyCoverImage;
using Afro.API.src.Modules.Listings.Properties.UpdateProperty;
using Afro.API.src.Modules.Listings.Properties.UploadPropertyImages;

namespace Afro.API.src.Modules.Listings;

public static class ListingsModule
{
    public static IServiceCollection AddListingsModule(this IServiceCollection services)
    {
        services.AddScoped<CreateOwnerHandler>();
        services.AddScoped<CreateOwnerValidator>();

        services.AddScoped<GetOwnerHandler>();

        services.AddScoped<SearchOwnerHandler>();

        services.AddScoped<CreatePropertyHandler>();
        services.AddScoped<CreatePropertyValidator>();

        services.AddScoped<UploadPropertyImagesValidator>();

        services.AddScoped<SetPropertyCoverImageHandler>();

        services.AddScoped<GetPropertyHandler>();

        services.AddScoped<PublishPropertyHandler>();

        services.AddScoped<SearchPropertiesHandler>();
        services.AddScoped<ArchivePropertyHandler>();

        services.AddScoped<UpdatePropertyHandler>();
        services.AddScoped<UpdatePropertyValidator>();


        return services;
    }
}
