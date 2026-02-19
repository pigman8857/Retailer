

using Apps.Interfaces;

public static class WeatherForcastEndpoints

{
  public static void StartWeatherForcast(this WebApplication app)
  {
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    app.MapGet("/weatherforecast", (ITestService testService) =>
    {
      testService.SaySomething();
      var forecast = Enumerable.Range(1, 5).Select(index =>
          new WeatherForecast
          (
              DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
              Random.Shared.Next(-20, 55),
              summaries[Random.Shared.Next(summaries.Length)]
          ))
          .ToArray();
      return forecast;
    })
    .WithName("GetWeatherForecast");
  }
}