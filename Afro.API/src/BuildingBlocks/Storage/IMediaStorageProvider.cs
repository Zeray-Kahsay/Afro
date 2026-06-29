namespace Afro.API.src.BuildingBlocks.Storage;

public interface IMediaStorageProvider
{
    GenerateUploadSignatureResult GenerateUploadSignature();
}
