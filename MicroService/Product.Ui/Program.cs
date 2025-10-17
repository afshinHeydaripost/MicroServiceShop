using Product.Ui.Class;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation(); 
// ثبت HttpClient
builder.Services.AddHttpClient().AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

// ثبت کلاس ApiService در DI
builder.Services.AddTransient<ApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
