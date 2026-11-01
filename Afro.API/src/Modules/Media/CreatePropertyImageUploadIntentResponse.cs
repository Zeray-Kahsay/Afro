namespace Afro.API.src.Modules.Media;

public sealed record CreatePropertyImageUploadIntentResponse(
    Guid UploadIntentId,
    string CloudName,
    string ApiKey,
    long Timestamp,
    string Signature,
    string StorageKey,
    DateTime ExpiresAtUtc
);

