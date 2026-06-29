using FluentValidation;

namespace Afro.API.src.Modules.Listings.Owners.CreateOwner;

public sealed class CreateOwnerValidator : AbstractValidator<CreateOwnerCommand>
{
    public CreateOwnerValidator()
    {
        RuleFor(o => o.FullName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(o => o.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+[1-9]\d{1,14}$");

        RuleFor(o => o.Email)
            .EmailAddress()
            .When(o => !string.IsNullOrWhiteSpace(o.Email));
    }
}
