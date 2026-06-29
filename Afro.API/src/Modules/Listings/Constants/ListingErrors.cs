using Afro.API.src.BuildingBlocks.Results;

namespace Afro.API.src.Modules.Listings.Constants;

public static class ListingErrors
{
    public static readonly Error OwnerPhoneAlreadyExists = 
       new (
        "Listings.OwnerPhoneAleadyExists",
        "Owner with this phone number already exists"
    );

    public static readonly Error OwnerNotFound = 
        new(
            "Listings.OwnerNotFound",
            "Owner was not found"
        );
    
    public static readonly Error PropertyNotFound = 
        new(
            "Listings.PropertyNotFound",
            "Property was not found"
        );
    
    public static readonly Error PropertyAlreadyAchived = 
        new (
            "Listings.PropertyAlreadyArchived",
            "Property is already archived"
        );

    public static readonly Error ArchivedPropertyCannotBeUpdated = 
        new (
            "Listings.ArchivedPropertyCannotBeUpdated",
            "Achived properties cannot be updated"
        );
}
