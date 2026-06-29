namespace Afro.API.src.BuildingBlocks.Storage;

public sealed record GenerateUploadSignatureResult(
    string CloudName,
    string ApiKey,
    long Timestamp,
    string Signature
);



