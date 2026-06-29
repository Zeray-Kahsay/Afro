namespace Afro.API.src.BuildingBlocks.Storage;

public static class StorageExtensions
{
    public static IServiceCollection AddStorageExtensions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<StorageOptions>(config.GetSection(StorageOptions.SectionName));
        services.Configure<CloudinaryOptions>(config.GetSection(CloudinaryOptions.SectionName));
        //services.AddScoped<IStorageProvider, CloudinaryStorageProvider>();
        services.AddScoped<IMediaStorageProvider, CloudinaryMediaStorageProvider>();

        return services;
    }
}
