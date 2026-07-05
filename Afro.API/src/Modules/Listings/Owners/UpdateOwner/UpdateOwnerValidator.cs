using FluentValidation;

namespace Afro.API.src.Modules.Listings.Owners.UpdateOwner;

public class UpdateOwnerValidator : AbstractValidator<UpdateOwnerCommand>
{
    public UpdateOwnerValidator()
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty();

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Address)
            .MaximumLength(500);

        RuleFor(x => x.Notes)
            .MaximumLength(1000);
        
    }
}
