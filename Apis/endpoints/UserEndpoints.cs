

using Apis.Utils;
using Apps.Interfaces;

public static class UserEndpoints

{
  public static void StartUserEndpoint(this WebApplication app)
  {
    app.MapGet("/user", (ITestService testService) =>
    {
      testService.SaySomething();

      return "this is get user ";
    })
    .WithName("GetUser");


    app.MapPost("/user/login", (Utils util, IConfiguration config, ITestService testService) =>
    {
      testService.SaySomething();

      string token = util.GenerateToken(config);
      Console.WriteLine(token);

      return token;
    })
    .WithName("LoginUser");
  }


}