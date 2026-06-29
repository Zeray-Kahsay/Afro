using FluentValidation;

namespace Afro.API.src.Modules.Listings.Properties.CreateProperty;

public class CreatePropertyValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyValidator()
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty();
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(50000);
        
        RuleFor(x => x.Price)
            .GreaterThan(0);
        
        RuleFor(x => x.Country)
            .NotEmpty();
        
        RuleFor(x => x.City)
            .NotEmpty();
    }
}
