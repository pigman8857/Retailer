

using Apis.Dtos;
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


    app.MapPost("/user/login", (LoginRequestDto loginRequest, IUserService userService) =>
    {
      userService.Authen(loginRequest.Username, loginRequest.Password);
      // Console.WriteLine(token);

      // return token;
    })
    .WithName("LoginUser");
  }


}