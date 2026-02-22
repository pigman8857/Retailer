using System;
using Domains.Exceptions;

namespace Domains.Entities;

public class User
{
  public int Id { get; set; }
  public string Email { get; set; }
  public string UserName { get; set; }
  public string HashedPassword { get; set; }
  public string Salt { get; set; }

  public void ThrowIfEmailExist(string email)
  {
    if (Email == email)
    {
      throw new EmailExistsException(email);
    }
  }

  public void ThrowIfUsernameExist(string username)
  {
    if (UserName == username)
    {
      throw new UsernameExistsException(username);
    }
  }
}
