using System;
using Domains.Entities;
using Domains.Exceptions;

namespace Domains.Tests.Entities;

public class UserTest
{
  [Fact]
  public void ThrowIfUsernameExist_ThrowsUsernameExistsException_WhenUsernameMatches()
  {
    var username = "johndoe";
    var user = new User() { UserName = username };

    var exception = Assert.Throws<UsernameExistsException>(() => user.ThrowIfUsernameExist(username));

    // Assert
    // Check that the message matches what you expect
    Assert.Equal("Username johndoe is already registered.", exception.Message);
  }

  [Fact]
  public void ThrowIfUserExist_DoesNotThrow_WhenUsernameIsDifferent()
  {
    // Arrange
    var user = new User { UserName = "johndoe" };

    // Act
    // If an exception is thrown here, the test will automatically fail (which is what we want)
    user.ThrowIfUsernameExist("janedoe");

    // Assert
    // No explicit assert needed; reaching this line means no exception was thrown!
  }

  [Fact]
  public void ThrowIfEmailExist_ThrowsEmailExistsException_WhenEmailMatches()
  {
    var emailToTest = "johndoe@us.com";
    var user = new User() { Email = emailToTest };

    var exception = Assert.Throws<EmailExistsException>(() => user.ThrowIfEmailExist(emailToTest));

    // Assert
    // Check that the message matches what you expect
    Assert.Equal("Email johndoe@us.com is already registered.", exception.Message);
  }

  [Fact]
  public void ThrowIfEmailExist_DoesNotThrow_WhenEmailIsDifferent()
  {
    // Arrange
    var user = new User { Email = "existing@system.com" };

    // Act
    // If an exception is thrown here, the test will automatically fail (which is what we want)
    user.ThrowIfEmailExist("brandnew@system.com");

    // Assert
    // No explicit assert needed; reaching this line means no exception was thrown!
  }
}
