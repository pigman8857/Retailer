using System;
using Domains.Entities;
using Infras.EntityConfigs;
using Microsoft.EntityFrameworkCore;

namespace Infras.Dbcontext;

public class RetailerContext : DbContext
{
  public RetailerContext(DbContextOptions<RetailerContext> options) : base(options)
  {

  }

  public DbSet<User> Users;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // Apply your dedicated configuration class
    modelBuilder.ApplyConfiguration(new UserEntityConfig());

    // Note: If you add many configs later, you can also use:
    // modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }
}
