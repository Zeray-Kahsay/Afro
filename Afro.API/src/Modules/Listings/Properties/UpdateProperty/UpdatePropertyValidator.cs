using FluentValidation;

namespace Afro.API.src.Modules.Listings.Properties.UpdateProperty;

public sealed class UpdatePropertyValidator : AbstractValidator<UpdatePropertyCommand>
{
    public UpdatePropertyValidator()
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty();
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(5000);
        
        RuleFor(x => x.Price)
            .GreaterThan(0);
        
        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.AddressLine)
            .MaximumLength(500);


    }
}
