using FluentValidation;

namespace Afro.API.src.Modules.Identity.LoginUser;

public sealed class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(a => a.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
        RuleFor(a => a.Password).NotEmpty().WithMessage("Password is required.");
    }
}
