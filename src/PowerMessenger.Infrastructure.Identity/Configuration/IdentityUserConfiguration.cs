using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerMessenger.Infrastructure.Identity.Entities;

namespace PowerMessenger.Infrastructure.Identity.Configuration;

public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.HasIndex(p => p.Email).IsUnique();
        builder.Property(p => p.Email).IsRequired();

        builder.Property(p => p.PasswordHash).IsRequired();
        
        builder.Property(p => p.PhoneNumber).HasMaxLength(15);
        

        #region HasData

        builder.HasData(
            new IdentityUser
            {
                Id = 2,
                Email = "test@yandex.ru",
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                DateCreated = DateTime.Now,
                PasswordHash = ""
            }
        );

        #endregion
    }
}