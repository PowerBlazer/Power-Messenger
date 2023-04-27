using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Configuration;

public class ChatTypeConfiguration:IEntityTypeConfiguration<ChatType>
{
    public void Configure(EntityTypeBuilder<ChatType> builder)
    {
        builder.ToTable("ChatTypes");
        
        builder.Property(p => p.Type).HasMaxLength(100);

        #region HasData
        builder.HasData(
            new ChatType
            {
                Id = 1,
                Type = "Personal"
            },
            new ChatType
            {
                Id = 2,
                Type = "Group"
            }
        );
        #endregion
    }
}