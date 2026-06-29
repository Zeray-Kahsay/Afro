using FluentValidation;

namespace Afro.API.src.Modules.Identity.RegisterUser;

public sealed class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(a => a.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Phone number must be in E.164 format.");

        RuleFor(a => a.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]")
            .WithMessage("Password must contain an uppercase letter.")
            .Matches("[a-z]")
            .WithMessage("Password must contain a lowercase letter.")
            .Matches("[0-9]")
            .WithMessage("Password must contain a number.")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("Password must contain a special character.");
        
        RuleFor(a => a.ConfirmPassword)
            .NotEmpty()
            .Equal(a => a.Password)
            .WithMessage("Passwords do not match.");
    }
}
