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
    public GenerateUploadSignatureResult GenerateUploadSignature(string storageKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storageKey);

        var normalizedStorageKey = storageKey.Trim();
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        // These parameters must match the parameters sent by the frontend during signed upload.

        var parameters = new SortedDictionary<string, object>
        {
            ["overwrite"] = false,
            ["public_id"] = normalizedStorageKey,
            ["timestamp"] = timestamp
        };

        var signature = _cloudinary.Api.SignParameters(parameters);

        return new GenerateUploadSignatureResult(
            _options.CloudName,
            _options.ApiKey,
            timestamp,
            signature,
            normalizedStorageKey
        );
    }


    public async Task<VerfiedMediaAsset?> VerfiyImageAsync(string storageKey, CancellationToken ct = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(storageKey);

        var normalizedStorageKey = storageKey.Trim();

        var result = await _cloudinary.GetResourceAsync(normalizedStorageKey, ct);

        // Cloudinary returns an error when the requested asset cannot be retrieved

        if (result.Error is not null)
        {
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
               return null;

            throw new InvalidOperationException($"Cloudinary asset verification failed: {result.Error.Message}");
        }

        if (!string.Equals(
        result.PublicId,
        normalizedStorageKey,
        StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The Cloudinary asset does not match the expected storage key.");
        }

        if (string.IsNullOrWhiteSpace(result.SecureUrl))
        {
            throw new InvalidOperationException("Cloudinary returned an asset without a secure URL");
        }

        return new VerfiedMediaAsset(
            result.PublicId,
            result.SecureUrl,
            result.Bytes,
            result.Format
        );
    }
}
