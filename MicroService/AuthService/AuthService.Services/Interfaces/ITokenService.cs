using AuthService.DataModel.Models;

using System.Security.Claims;
using Helper;

namespace AuthService.Services.Interfaces;
public interface ITokenService
{
    GeneralResponse<string> GenerateAccessToken(User user, IEnumerable<string> roles);
    RefreshToken GenerateRefreshToken(int userId,bool rememberMe, string ipAddress);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}

