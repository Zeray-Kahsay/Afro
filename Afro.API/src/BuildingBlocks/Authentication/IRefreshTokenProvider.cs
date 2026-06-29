namespace Afro.API.src.BuildingBlocks.Authentication;

public interface IRefreshTokenProvider
{
    string GenerateRefreshToken();
}
