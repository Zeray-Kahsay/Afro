using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.BuildingBlocks.Storage;

namespace Afro.API.src.Modules.Media;

public sealed class GenerateUploadSignatureHandler(IMediaStorageProvider cloudinaryProvider)
{
    public Task<Result<GenerateUploadSignatureResponse>> HandleAsync(CancellationToken ct)
    {
        var result = cloudinaryProvider.GenerateUploadSignature();

        return Task.FromResult(Result<GenerateUploadSignatureResponse>.Success(
            new GenerateUploadSignatureResponse(
                result.CloudName,
                result.ApiKey,
                result.Timestamp,
                result.Signature
            )
        ));
    }
}
