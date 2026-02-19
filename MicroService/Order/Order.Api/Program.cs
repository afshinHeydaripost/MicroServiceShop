using Helper.VieModels;
using Microsoft.EntityFrameworkCore;
using Order.DataModel.Context;
using Order.Services.Interfaces;
using Order.Services.Services;
using OrderService.Messaging;
using System.Text.Json;

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
builder.Services.AddScoped<IUserServices, UserServices>();
#endregion
var app = builder.Build();
#region RabbitMqListener
// ایجاد Dictionary صف‌ها و callback‌ها
var queueCallbacks = new Dictionary<string, Func<string, Task>>
{
    ["productCreateQueue"] = async json =>
    {
        var product = JsonSerializer.Deserialize<ProductInfoViewModel>(json);
        if (product != null)
        {
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IProductInfoServices>();
            await service.Create(product);
        }
    },   
    ["UserCreated"] = async json =>
    {
        var user = JsonSerializer.Deserialize<UserViewModel>(json);
        if (user != null)
        {
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IUserServices>();
            await service.Create(user);
        }
    },
    ["productUpdateQueue"] = async json =>
    {
        var product = JsonSerializer.Deserialize<ProductInfoViewModel>(json);
        if (product != null)
        {
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IProductInfoServices>();
            await service.Update(product);
        }
    },
    ["productDeleteQueue"] = async json =>
    {
        var id = JsonSerializer.Deserialize<int>(json);
        if (id != null && id > 0)
        {
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IProductInfoServices>();
            await service.Delete(id);
        }
    },
    ["productModelAmountQueue"] = async json =>
    {
        var item = JsonSerializer.Deserialize<ProductModelViewMode>(json);
        if (item != null)
        {
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IProductInfoServices>();
            await service.UpdateProductStock(item);
        }
    },
};
// ثبت HostedService
var multiQueueListener = new MultiQueueRabbitMqListenerService(
    app.Services.GetRequiredService<ILogger<MultiQueueRabbitMqListenerService>>(),
    queueCallbacks,
    app.Services.GetRequiredService<IConfiguration>()
);
_ = multiQueueListener.StartAsync(default); // fire and forget

#endregion
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
