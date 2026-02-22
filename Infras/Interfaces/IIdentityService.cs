using System;

namespace Infras.Interfaces;

public interface IIdentityService
{
  string GenerateToken(string userId, string email, IEnumerable<string> roles);
}
