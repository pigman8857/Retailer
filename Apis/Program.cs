using System.Text;
using Apps.Interfaces;
using Apps.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Apis.BuilderSetup;
using Apis.Utils;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddSingleton<Utils>();
// Add Authentication Services by calling your custom extension method. If not then Program.cs will broated
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


// Enable Middleware for Authentication (Order matters!)
app.UseAuthentication();
app.UseAuthorization();

app.StartUserEndpoint();
// 3. Secure your endpoint
app.MapGet("/secure-data", () => "This is protected!")
   .RequireAuthorization();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
