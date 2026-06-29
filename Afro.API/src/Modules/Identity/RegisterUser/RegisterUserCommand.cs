namespace Afro.API.src.Modules.Identity.RegisterUser;

public sealed record RegisterUserCommand(
    string PhoneNumber,
    string Password,
    string ConfirmPassword
);



