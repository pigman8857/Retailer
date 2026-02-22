using System;
using Apps.Interfaces;
using Domains.Entities;
using Infras.Interfaces;
using Infras.Repositories.Interfaces;

namespace Apps.Services;

public class UserService : IUserService
{
  private readonly IIdentityService _identityService;
  private readonly IPasswordService _passwordService;

  private readonly IUserRepository _userRepository;

  public UserService(
    IIdentityService identityService,
    IPasswordService passwordService,
    IUserRepository userRepository)
  {
    _identityService = identityService;
    _passwordService = passwordService;
    _userRepository = userRepository;
  }


  public async Task<string> Register(string email, string username, string password)
  {
    var user = await _userRepository.GetByEmailAsync(email);

    user!.ThrowIfEmailExist(email);
    user!.ThrowIfUsernameExist(username);

    var salt = _passwordService.GetSalt();
    var hashedPassword = _passwordService.HashPassword(password, salt);

    await _userRepository.AddAsync(new User()
    {
      UserName = username,
      Email = email,
      Salt = Convert.ToBase64String(salt),
      HashedPassword = hashedPassword,
    });

    await _userRepository.SaveChangesAsync();

    var token = _identityService.GenerateToken(username, email, null);

    return token;
  }

  public void Authen(string username, string password)
  {
    var email = "";
    var token = _identityService.GenerateToken(username, email, null);
  }
}
