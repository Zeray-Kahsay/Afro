namespace Afro.API.src.BuildingBlocks.Storage;

public sealed class StorageOptions
{
    public const string SectionName = "Storage";
    public string Provider  { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
}
