namespace Afro.API.src.BuildingBlocks.Storage;

public interface IMediaStorageProvider
{
    GenerateUploadSignatureResult GenerateUploadSignature(string storageKey);

    Task<VerfiedMediaAsset?> VerfiyImageAsync(string storageKey, CancellationToken ct = default);
}
