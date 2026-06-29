namespace Afro.API.src.Modules.Identity.GetCurrentUser;

public sealed record CurrentUserResponse(Guid Id, string PhoneNumber, IReadOnlyCollection<string> Roles);

