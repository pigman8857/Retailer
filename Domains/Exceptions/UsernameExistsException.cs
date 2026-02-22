using System;

namespace Domains.Exceptions;

public class UsernameExistsException : Exception
{
  public UsernameExistsException(string username) : base($"Username {username} is already registered.")
  {

  }
}
