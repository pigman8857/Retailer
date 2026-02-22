using System;

namespace Domains.Exceptions;

public class EmailExistsException : Exception
{
  public EmailExistsException(string email) : base($"Email {email} is already registered.")
  {

  }
}
