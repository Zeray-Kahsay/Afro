namespace Afro.API.src.Modules.Listings.Domain;

public sealed class Owner
{
    public Guid Id  { get; private set; }
    public string  FullName  { get; private set; } = null!;
    public string  PhoneNumber  { get; private set; } = null!;
    public string?  Email  { get; private set; }
    public string? Address  { get; private set; }
    public string? Notes  { get; private set; }
    public DateTime CreatedAtUtc  { get; private set; }
    private readonly List<Property> _properties = [];

    public IReadOnlyCollection<Property> Properties => _properties.AsReadOnly();

    private Owner(){}

    public static Owner Create(
        string fullName,
        string phoneNumber,
        string? email,
        string? address,
        string? notes
    )
    {
        return new Owner
        {
            Id = Guid.NewGuid(),
            FullName = fullName,
            PhoneNumber = phoneNumber,
            Email = email,
            Address = address,
            Notes = notes,
            CreatedAtUtc = DateTime.UtcNow
        };
    }

    public void UpdateProfile(
        string fullName,
        string phoneNumber,
        string? email,
        string? address,
        string? notes
    )
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        Notes = notes;
    }
}
