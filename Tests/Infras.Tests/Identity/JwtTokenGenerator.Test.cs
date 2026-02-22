using System;
using Infras.Identity;
using Microsoft.Extensions.Configuration;
namespace Infras.Tests.Identity;

public class JwtTokenGeneratorTest
{
  private readonly JwtTokenGenerator _jwtToken;

  public JwtTokenGeneratorTest() { }

  [Fact]
  public void GenerateJwtToken_ShouldReturnValidToken_without_roles()
  {
    // Arrange
    var username = "fakeName";
    var email = "fakeEmail";
    var roles = new List<string>();

    // Create an in-memory configuration
    var myConfiguration = new Dictionary<string, string>
    {
        {"Jwt:Key", "ThisIsASecretKeyAtLeast32CharactersLong!"},
        {"Jwt:Issuer", "TestIssuer"},
        {"Jwt:Audience", "TestAudience"}
    };

    var configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(myConfiguration!)
        .Build();

    var jwtToken = new JwtTokenGenerator(configuration);

    // Act
    var token = jwtToken.GenerateToken(username, email, roles);

    // Assert
    Assert.NotNull(token);

    Assert.Contains(".", token); // Basic JWT check
  }
}
