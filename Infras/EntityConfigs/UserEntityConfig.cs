using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infras.EntityConfigs;

public class UserEntityConfig : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.Id);
    builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
    builder.Property(u => u.UserName).HasColumnName("UserName").IsRequired();
    builder.Property(u => u.HashedPassword).HasColumnName("HashedPassword").IsRequired();
  }
}