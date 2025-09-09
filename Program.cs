using WiraSystem_Assessment.Application.Interfaces;
using WiraSystem_Assessment.Infrastructure.ExternalClients;
using WiraSystem_Assessment.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// اضافه کردن HttpClient برای فراخوانی APIها
builder.Services.AddHttpClient<IOpenWeatherMapClient, OpenWeatherMapClient>((serviceProvider, client) =>
{
    client.BaseAddress = new Uri("https://api.openweathermap.org/");
});

// تنظیم Dependency Injection برای Interfaceها و پیاده‌سازی‌ها
// OpenWeatherMapClient رو دیگه نیاز نیست اینجا اضافه کنیم چون بالا اضافه شده
builder.Services.AddScoped<IWeatherService, WeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();