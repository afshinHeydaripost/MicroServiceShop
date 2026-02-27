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
                    new Claim("email", user.Email ?? string.Empty),
                    new Claim("UserCode", user.UserCode ?? string.Empty),
                    new Claim("FirstName", user.FirstName ?? string.Empty),
                    new Claim("LastName", user.LastName ?? string.Empty),
                    new Claim("UserFullName", user.FirstName +" "+user.LastName),
                    new Claim("UserId", user.Id.ToString())
                };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));



            var key = new SymmetricSecurityKey(_key);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_config["JwtSettings:AccessTokenExpirationMinutes"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["JwtSettings:AccessTokenExpirationMinutes"])),
            //    Issuer = _config["JwtSettings:Issuer"],
            //    Audience = _config["JwtSettings:Audience"],
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
            //};

            ////var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToken = tokenHandler.WriteToken(token);
            return GeneralResponse<string>.Success(userToken);
        }
        catch (Exception e)
        {
            return GeneralResponse<string>.Fail(e);
        }
    }

    public RefreshToken GenerateRefreshToken(int userId, bool rememberMe, string ipAddress)
    {
        return new RefreshToken
        {
            UserId = userId,
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            ExpiresDateTime = DateTime.UtcNow.AddDays(
                int.Parse(_config["JwtSettings:RefreshTokenExpirationDays"])),
            CreateDateTime = DateTime.Now,
            CreatedByIp = ipAddress,
            RememberMe = rememberMe,
            Revoked = false
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
