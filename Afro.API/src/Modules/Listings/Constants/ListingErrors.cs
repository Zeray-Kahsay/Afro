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
    
    public static readonly Error OwnerNotArchived = 
        new (
            "Listings.OwnerNotArchived",
            "Owner is not archived"
        );

    public static readonly Error OwnerHasActiveListings = 
        new (
            "Listings.OwnerHasActiveListings",
            "Owner has active listings and cannot be cleared"
        );

    public static readonly Error InvalidImageUploadRequest = 
        new (
            "Listings.InvalidImageUploadRequest",
            "Invalid image upload request"
        );

    public static readonly Error UploadIntentNotFound = 
        new (
            "Listings.UploadIntentNotFound",
            "One or more upload intents were not found"
        );

    public static readonly Error UploadIntentDoesNotBelongToProperty = 
        new (
            "Listings.UploadIntentDoesNotBelongToProperty",
            "An upload intent does not belong to the this property"
        );

    public static readonly Error UploadIntentAlreadyFinalized = 
        new (
            "Listings.UploadIntentAlreadyFinalized",
            "The specified upload intent is already finalized"
        );

    public static readonly Error UploadIntentExpired = 
        new (
            "Listings.UploadIntentExpired",
            "The specified upload intent has expired"
        );

    public static readonly Error UploadAssetNotFound = 
        new (
            "Listings.UploadAssetNotFound",
            "The specified upload asset was not found"
        );

    public static readonly Error InvalidUploadedImage = 
        new (
            "Listings.InvalidUploadedImage",
            "The specified uploaded image is invalid"
        );

    public static readonly Error PropertyImageLimitExceeded = 
        new (
            "Listings.PropertyImageLimitExceeded",
            "The property has exceeded the maximum number of allowed images"
        );

}
