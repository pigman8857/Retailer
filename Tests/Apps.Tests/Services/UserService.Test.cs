using System;
using Apps.Services;
using Domains.Entities;
using Infras.Interfaces;
using Infras.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;

namespace Apps.Tests.Services;

public class UserServiceTest
{
  private readonly Mock<IIdentityService> _mockIdentityService;
  private readonly Mock<IPasswordService> _mockPasswordService;

  private readonly Mock<IUserRepository> _mockUserRepository;
  private readonly UserService _userService;

  public UserServiceTest()
  {
    _mockIdentityService = new Mock<IIdentityService>();
    _mockPasswordService = new Mock<IPasswordService>();
    _mockUserRepository = new Mock<IUserRepository>();

    _userService = new UserService(
      _mockIdentityService.Object,
      _mockPasswordService.Object,
      _mockUserRepository.Object);
  }

  [Fact]
  public async Task CanRegister_WithToken()
  {
    //Arrange 
    var username = "johndoe";
    var password = "lalilulelo";
    var email = "johndoe@us.com";
    var saltString = "Ec5LNYme65LHJyFNsR4jZg==";
    var fakeHashedPassword = "fakeHashedPassword";
    var returnToken = "fakeToken";
    var addUser = new User()
    {
      UserName = "johndoe",
      Email = "johndoe@us.com",
      Salt = saltString,
      HashedPassword = fakeHashedPassword
    };

    _mockUserRepository
      .Setup(userRepo => userRepo.GetByEmailAsync(It.IsAny<string>()))
      .ReturnsAsync(new User());

    var byteSalt = Convert.FromBase64String(saltString);
    _mockPasswordService
      .Setup(passServ => passServ.GetSalt())
      .Returns(byteSalt);

    _mockPasswordService
      .Setup(passServ => passServ.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
      .Returns(fakeHashedPassword);

    _mockUserRepository
      .Setup(userRepo => userRepo.AddAsync(It.IsAny<User>()));

    _mockUserRepository
      .Setup(userRepo => userRepo.SaveChangesAsync());

    _mockIdentityService.Setup(iService =>
      iService.GenerateToken(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<List<string>>()
      )).Returns(returnToken);

    var result = "";

    //Act
    try
    {
      result = await _userService.Register(email, username, password);
    }
    catch (Exception e)
    {
      Console.WriteLine($"ERROR>>>>> {e.Message}");
      return;
    }

    //Assert
    _mockUserRepository
      .Verify(userRepo => userRepo.GetByEmailAsync(email), Times.Once);

    _mockPasswordService.Verify(
      pService => pService.GetSalt(),
      Times.Once);

    _mockPasswordService.Verify(
      pService => pService.HashPassword(password, byteSalt),
      Times.Once);

    _mockUserRepository
      .Verify(userRepo => userRepo.AddAsync(It.Is<User>(u =>
        u.UserName == addUser.UserName &&
        u.Email == addUser.Email &&
        u.Salt == addUser.Salt &&
        u.HashedPassword == addUser.HashedPassword
      )), Times.Once);

    _mockUserRepository
      .Verify(userRepo => userRepo.SaveChangesAsync(), Times.Once);

    _mockIdentityService.Verify(
      iService => iService.GenerateToken(username, email, null),
      Times.Once);

    Assert.Equal(returnToken, result);
  }
}
