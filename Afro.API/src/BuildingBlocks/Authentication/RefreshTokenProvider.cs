using System.Security.Cryptography;
using Afro.API.src.BuildingBlocks.Authentication;

namespace Afro.API.Modules.BuildingBlocks.Authentication;

public sealed class RefreshTokenProvider : IRefreshTokenProvider
{
    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
