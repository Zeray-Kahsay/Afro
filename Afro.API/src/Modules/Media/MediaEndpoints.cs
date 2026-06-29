namespace Afro.API.src.Modules.Media;

public static class MediaEndpoints
{
    public static IEndpointRouteBuilder MapMediaEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGenerateUploadSignature();

        return app; 
    }
}
