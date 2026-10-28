using FluentValidation;

namespace Afro.API.src.Modules.Listings.Properties.UploadPropertyImages;

public sealed class UploadPropertyImagesValidator : AbstractValidator<UploadPropertyImagesCommand>
{
    private const int MaxImagePerRequest = 30;
    private const int MaxUrlLength = 2048;
    public UploadPropertyImagesValidator()
    {
        RuleFor(x => x.Images)
            .NotEmpty()
            .NotNull()
            .WithMessage("At least one image is required")
            .Must(images => images.Count <= MaxImagePerRequest)
            .WithMessage($"A maximum of {MaxImagePerRequest} images can be uploaded per request");

        // RuleForEach(x => x.Images)
        //     .ChildRules(image =>
        //     {
        //         image.RuleFor(x => x.Url)
        //             .Cascade(CascadeMode.Stop)
        //             .NotEmpty()
        //             .WithMessage("Image URL is required")
        //             .MaximumLength(MaxUrlLength)
        //             .WithMessage($"Image URL cannot exceed {MaxUrlLength} characters")
        //             .Must(IsValidHttpUrl)
        //             .WithMessage("Image URL must be a valid HTTP or HTTPS URL");
        //     });
        
        // RuleFor(x => x.Images)
        //     .Must(images => 
        //         images.Select(i => i.Url.Trim())
        //         .Distinct(StringComparer.OrdinalIgnoreCase)
        //         .Count() == images.Count)
        //         .When(x => x.Images is not null)
        //         .WithMessage("Duplicate image URLs are not allowed in the same request");

    }
    private static bool IsValidHttpUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        return Uri.TryCreate(
            url,
            UriKind.Absolute,
            out var uri
        ) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }
}

