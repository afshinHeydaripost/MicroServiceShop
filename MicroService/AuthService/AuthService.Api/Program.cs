using AuthService.Api.Class;
using AuthService.DataModel.Context;
using AuthService.DataModel.Models;
using AuthService.Services;
using AuthService.Services.Interfaces;
using Helper;
using Helper.VieModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --------------------- DbContext ---------------------
builder.Services.AddDbContext<MicroServiceShopAuthServiceContext>(opt =>
	opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#region  Dependency Injection
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRolService, UserRolService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<IRabbitMQ, RabbitMQProducer>();
#endregion

builder.Services.AddControllers();

#region JWT 
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettingsViewModel>();
var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = jwtSettings.Issuer,
			ValidateAudience = true,
			ValidAudience = jwtSettings.Audience,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
			ValidateLifetime = true,
			ClockSkew = TimeSpan.FromSeconds(30)
		};
	});

builder.Services.AddAuthorization();
#endregion
var app = builder.Build();

app.UseRouting();        
app.UseAuthentication();     
app.UseAuthorization();

app.MapControllers();


app.MapGet("/weatherforecast", () => "AuthService Running");

app.Run();