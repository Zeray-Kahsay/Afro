using FluentValidation;

namespace Afro.API.src.Modules.Listings.Properties.UploadPropertyImages;

public sealed class UploadPropertyImagesValidator : AbstractValidator<UploadPropertyImagesCommand>
{
    public UploadPropertyImagesValidator()
    {
        RuleFor(x => x.Images)
            .NotEmpty();
        
        RuleForEach(x => x.Images)
            .ChildRules(image =>
            {
                image.RuleFor(x => x.Url)
                    .NotEmpty()
                    .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    .WithMessage("Invalid image URL");
            });
    }
}
