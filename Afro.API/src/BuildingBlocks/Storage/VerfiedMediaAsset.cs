namespace Afro.API.src.BuildingBlocks.Storage;

public sealed record VerfiedMediaAsset(
    string StorageKey,
    string SecureUrl,
    long Bytes,
    string Format);

