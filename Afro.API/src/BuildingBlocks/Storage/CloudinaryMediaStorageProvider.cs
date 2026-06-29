using CloudinaryDotNet;
using Microsoft.Extensions.Options;

namespace Afro.API.src.BuildingBlocks.Storage;

public sealed class CloudinaryMediaStorageProvider : IMediaStorageProvider
{
    private readonly Cloudinary _cloudinary;
    private readonly CloudinaryOptions _options;

    public CloudinaryMediaStorageProvider(IOptions<CloudinaryOptions> options)
    {
        _options = options.Value;

        var account = new Account(
            _options.CloudName,
            _options.ApiKey,
            _options.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
    }
    public GenerateUploadSignatureResult GenerateUploadSignature()
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var parameters = new SortedDictionary<string, object>
        {
            ["timestamp"] = timestamp
        };

        var signature = _cloudinary.Api.SignParameters(parameters);

        return new GenerateUploadSignatureResult(
            _options.CloudName,
            _options.ApiKey,
            timestamp,
            signature
        );
    }
}
