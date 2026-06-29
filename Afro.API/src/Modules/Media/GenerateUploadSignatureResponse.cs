namespace Afro.API.src.Modules.Media;

public sealed record GenerateUploadSignatureResponse(
    string CloudName,
    string ApiKey,
    long Timestamp,
    string Signature // never expose ApiSecret, only backend knows it.instead use signature
);



