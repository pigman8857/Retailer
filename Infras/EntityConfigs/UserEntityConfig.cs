using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infras.EntityConfigs;

public class UserEntityConfig : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.Id);

    builder
      .Property(u => u.Email)
      .HasColumnName("Email")
      .IsRequired()
      .HasMaxLength(256);

    builder
      .Property(u => u.UserName)
      .HasColumnName("UserName")
      .IsRequired()
      .HasMaxLength(100);

    builder
      .Property(u => u.HashedPassword)
      .HasColumnName("HashedPassword")
      .IsRequired();

    builder
      .Property(u => u.Salt)
      .HasColumnName("Salt")
      .IsRequired();

    builder
      .HasIndex(u => u.Email)
      .IsUnique()
      .HasDatabaseName("IX_Users_Email");

    builder
      .HasIndex(u => u.UserName)
      .IsUnique()
      .HasDatabaseName("IX_Users_UserName");
  }
}