namespace Afro.API.src.BuildingBlocks.Storage;

// <summary>
// Represents a Cloudinary specific upload authorization behavior. 
public interface IMediaStorageProvider
{
    GenerateUploadSignatureResult GenerateUploadSignature();
}
