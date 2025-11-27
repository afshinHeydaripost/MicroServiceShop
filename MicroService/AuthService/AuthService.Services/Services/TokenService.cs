using AuthService.DataModel.Models;
using AuthService.Services.Interfaces;
using Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Services;
public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly byte[] _key;

    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]);
    }

    public GeneralResponse<string> GenerateAccessToken(User user, IEnumerable<string> roles)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("email", user.Email ?? string.Empty)
        };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["JwtSettings:AccessTokenExpirationMinutes"])),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToken = tokenHandler.WriteToken(token);
            return GeneralResponse<string>.Success(userToken);
        }
        catch (Exception e)
        {
            return GeneralResponse<string>.Fail(e);
        }
    }

    public RefreshToken GenerateRefreshToken(string ipAddress)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        return new RefreshToken
        {
            Token = token,
            ExpiresDateTime = DateTime.UtcNow.AddDays(double.Parse(_config["JwtSettings:RefreshTokenExpirationDays"])),
            CreateDateTime = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = _config["JwtSettings:Audience"],
            ValidateIssuer = true,
            ValidIssuer = _config["JwtSettings:Issuer"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(_key),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}
