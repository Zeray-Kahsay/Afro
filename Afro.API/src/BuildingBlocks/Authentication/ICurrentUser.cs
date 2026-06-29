namespace Afro.API.src.BuildingBlocks.Authentication;

public interface ICurrentUser
{
    Guid UserId { get; }
    bool IsAuthenticated { get; }
    string? PhoneNumber { get; }
}
