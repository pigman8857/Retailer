using System;
using Domains.Entities;
using Infras.Dbcontext;
using Infras.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infras.Repositories;

public class UserRepository : IUserRepository
{
  private readonly RetailerContext _context;
  public UserRepository(RetailerContext context)
  {
    _context = context;
  }

  public async Task<User> GetByIdAsync(int id)
  {
    var user = await _context.Users.FindAsync(id);
    // If 'user' is null, it returns a new User instance instead
    return user ?? new User();
  }

  public async Task<User> GetByUsernameAsync(string username)
  {
    var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
    // If 'user' is null, it returns a new User instance instead
    return user ?? new User();
  }

  public async Task<User> GetByEmailAsync(string email)
  {
    var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
    // If 'user' is null, it returns a new User instance instead
    return user ?? new User();
  }

  public async Task AddAsync(User user)
  {
    await _context.Users.AddAsync(user);
  }

  public async Task SaveChangesAsync()
  {
    await _context.SaveChangesAsync();
  }
}
