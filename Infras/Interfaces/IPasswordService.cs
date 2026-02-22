using System;

namespace Infras.Interfaces;

public interface IPasswordService
{
  string HashPassword(string password, byte[] salt);
  byte[] GetSalt();
}
