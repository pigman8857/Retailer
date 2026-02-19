using Apps.Interfaces;

namespace Apps.Services;

public class TestService : ITestService
{
  public TestService()
  {

  }

  public void SaySomething()
  {
    Console.WriteLine("#### TestService says Hi");
  }
}