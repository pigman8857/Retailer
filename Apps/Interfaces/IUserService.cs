using System;
using Domains.Entities;

namespace Apps.Interfaces;

public interface IUserService
{
  Task<string> Register(string email, string username, string password);
  void Authen(string username, string password);
}
