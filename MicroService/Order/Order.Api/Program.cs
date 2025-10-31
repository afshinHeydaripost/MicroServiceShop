using Microsoft.EntityFrameworkCore;
using Order.DataModel.Context;
using Order.Services.Interfaces;
using Order.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//// DbContext
builder.Services.AddDbContext<MicroServiceShopOrderContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


#region Dependency_Injection
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IOrderItemServices, OrderItemServices>();
builder.Services.AddScoped<IProductInfoServices, ProductInfoServices>();
#endregion
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
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
