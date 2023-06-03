using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerMessenger.Infrastructure.Identity.Entities;

namespace PowerMessenger.Infrastructure.Identity.Configuration;

public class IdentityTokenConfiguration: IEntityTypeConfiguration<IdentityToken>
{
    public void Configure(EntityTypeBuilder<IdentityToken> builder)
    {
        builder.HasIndex(p => p.Token).IsUnique();
        builder.Property(p => p.Token).IsRequired();

        builder
            .HasOne(p => p.User)
            .WithOne(p => p.IdentityToken)
            .HasForeignKey<IdentityToken>(p => p.UserId);

        builder.HasData(new IdentityToken
        {
            Id = 1,
            UserId = 1,
            Token = "121212121212121",
            Expiration = DateTime.Now.AddDays(7)
        });
    }
}