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
                Id = 1,
                Email = "yak.ainur@yandex.ru",
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                DateCreated = DateTime.Now,
                PasswordHash = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3"
            }
        );

        #endregion
    }
}