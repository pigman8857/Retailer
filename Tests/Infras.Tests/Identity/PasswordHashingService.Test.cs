using System;
using Infras.Identity;

namespace Infras.Tests.Identity;

public class PasswordHashingServiceTest
{
  private readonly PasswordHashingService _passwordHashingService;
  public PasswordHashingServiceTest()
  {
    _passwordHashingService = new PasswordHashingService();
  }

  [Fact]
  public void GetSalt_Success()
  {
    var salt = _passwordHashingService.GetSalt();

    Assert.NotEmpty(salt);
  }

  [Fact]
  public void HashPassword_Success()
  {
    //Arrange
    var password = "9h,pewdj";
    var salt = Convert.FromBase64String("Ec5LNYme65LHJyFNsR4jZg==");

    //Act
    var hashed = _passwordHashingService.HashPassword(password, salt);

    //Assert
    Assert.Equal("4IoJnozCjq/68shONfoOLaPFrp9dmdRPXg9w3Lg9QOo=", hashed);
  }


}
