using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Helper.VieModels;
using Microsoft.AspNetCore.Identity.Data;
using Helper;
using Microsoft.EntityFrameworkCore;
using Products.DataModel.Context;
using Products.Services.Interfaces;
using Products.Services;
using ProductService.Api.Class;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<MicroServiceShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#region Dependency_Injection
builder.Services.AddScoped<IProductsServices, ProductsServices>();
builder.Services.AddScoped<IBrandServices, BrandServices>();
builder.Services.AddScoped<IProductCategoryServices, ProductCategoryServices>();
builder.Services.AddScoped<IProductModelServices, ProductModelServices>();
builder.Services.AddScoped<IProductColorServices, ProductColorServices>();
builder.Services.AddScoped<IProductStockServices, ProductStockServices>();
builder.Services.AddScoped<IDiscountServices, DiscountServices>();

builder.Services.AddSingleton<IRabbitMQ, RabbitMQProducer>();
#endregion

#region JWT
var jwtSection = builder.Configuration.GetSection("JwtSettings");
var jwtSettings = jwtSection.Get<JwtSettingsViewModel>();

var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
        ClockSkew = TimeSpan.FromSeconds(30) // مقداری تلرانس
    };
});
#endregion




var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


var summaries = new[]
{
    "Product"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
