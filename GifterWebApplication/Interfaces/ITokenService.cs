using Shared.Responses;
using System.Security.Claims;

namespace JwtAuthentication.Server.Services
{
    public interface ITokenService
    {
        SingleResponse<string> GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        SingleResponse<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    }
}
