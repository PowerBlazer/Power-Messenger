using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Configuration;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.Property(p => p.UserName).HasMaxLength(100);
        builder.Property(p => p.Theme).HasMaxLength(50);

        #region HasData

        builder.HasData(
            new User
            {
                Id = 1,
                UserId = 1,
                UserName = "PowerBlaze",
                Email = "power@mail.ru",
                DateCreated = DateTime.Now
            },
            new User
            {
                Id = 2,
                UserId = 2,
                UserName = "TowerBlaze",
                Email = "tower@mail.ru",
                DateCreated = DateTime.Now
            }
        );

        #endregion
    }
}