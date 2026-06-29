namespace Afro.API.src.Modules.Media;

public static class MediaModule
{
    public static IServiceCollection AddMediaModule(this IServiceCollection services)
    {
        services.AddScoped<GenerateUploadSignatureHandler>();

        return services;
    }
}
