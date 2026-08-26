using FluentValidation;

namespace Afro.API.src.Modules.Listings.Owners.ArchiveOwner;

public sealed class ArchiveOwnerValidator : AbstractValidator<ArchiveOwnerCommand>
{
    public ArchiveOwnerValidator()
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty();
    }
}
