namespace Afro.API.src.Modules.Listings.Domain;

public sealed class PropertyImage
{
    public Guid Id  { get; private set; }
    public Guid PropertyId  { get; private set; }
    public string  Url  { get; private set; } = null!;
    public bool  IsCover  { get; private set; }
    public int  SortOrder  { get; private set; }
    public string StorageProvider { get; private set; } = null!; // Cloudinary || Azure Blob || something else
    public Property Property  { get; private set; } = null!;

    private PropertyImage(){}

    public static PropertyImage Create(
        Guid propertyId,
        string url,
        int sortOrder
    )
    {
        return new PropertyImage
        {
            Id = Guid.NewGuid(),
            PropertyId = propertyId,
            Url = url,
            SortOrder = sortOrder
        };
    }

    internal void SetAsCover()
    {
        IsCover = true;
    }

    internal void RemoveCover()
    {
        IsCover = false;
    }


}
