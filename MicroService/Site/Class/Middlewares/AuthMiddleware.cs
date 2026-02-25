using Helper;
using Helper.VieModels;
using System.Security.Claims;
using System.Text;
//using System.IdentityModel.Tokens.Jwt;  // JwtSecurityTokenHandler
//using Microsoft.IdentityModel.Tokens;  // TokenValidationParameters, SymmetricSecurityKey
using System.Security.Claims;          // Claim
using System.Text;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;

    public AuthMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower();

        var allowedPaths = new[]
        {
            "/Login",
            "/account/loginuser"
        };

        if (allowedPaths.Any(p => path!.StartsWith(p)))
        {
            await _next(context);
            return;
        }

        var accessToken = context.Request.GetCookiesValue("userToken");

        if (!string.IsNullOrEmpty(accessToken))
        {
            await _next(context);
            return;
            //var principal = ValidateJwt(accessToken);
            //if (principal != null)
            //{
            //    context.User = principal;
            //    await _next(context);
            //    return;
            //}
        }

        // AccessToken نامعتبر یا وجود ندارد → استفاده از RefreshToken
        var refreshToken = context.Request.GetCookiesValue("refreshToken");
        if (string.IsNullOrEmpty(refreshToken))
        {
            context.Response.Redirect("/account/login");
            return;
        }

        var refreshed = await RefreshToken(context, refreshToken);
        if (!refreshed.isSuccess)
        {
            context.Response.Redirect("/account/login");
            return;
        }

        await _next(context);
    }

    //private ClaimsPrincipal? ValidateJwt(string token)
    //{
    //    try
    //    {
    //        var tokenHandler = new JwtSecurityTokenHandler();
    //        var key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]);

    //        return tokenHandler.ValidateToken(token, new TokenValidationParameters
    //        {
    //            ValidateIssuer = true,
    //            ValidateAudience = true,
    //            ValidateLifetime = true,
    //            ValidateIssuerSigningKey = true,
    //            ValidIssuer = _config["JwtSettings:Issuer"],
    //            ValidAudience = _config["JwtSettings:Audience"],
    //            IssuerSigningKey = new SymmetricSecurityKey(key),
    //            ClockSkew = TimeSpan.Zero
    //        }, out _);
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //}

    private async Task<GeneralResponse> RefreshToken(HttpContext context, string refreshToken)
    {
        var url = _config["ApiUrl:AuthService"] + "authService/User/refresh";
        var result = url.PostData<LoginRequestViewModel, GeneralResponse<UserViewModel>>(new LoginRequestViewModel { Token = refreshToken });
        if (result == null || !result.isSuccess)
            return GeneralResponse.Fail();

        // زمان expire Cookie بر اساس RememberMe مستقیم از response
        var rememberMe = result.obj.RememberMe;
        var accessExpire = rememberMe ? DateTime.UtcNow.AddDays(2) : DateTime.UtcNow.AddMinutes(15);
        var refreshExpire = rememberMe ? DateTime.UtcNow.AddDays(2) : DateTime.UtcNow.AddDays(1);

        context.Response.CreateCookies("userToken", result.obj.Token, accessExpire);
        context.Response.CreateCookies("refreshToken", result.obj.RefreshToken, refreshExpire);
        return GeneralResponse.Success();
    }
}

