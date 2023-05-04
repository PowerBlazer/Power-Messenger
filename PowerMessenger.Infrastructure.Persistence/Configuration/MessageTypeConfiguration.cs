using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Configuration;

public class MessageTypeConfiguration: IEntityTypeConfiguration<MessageType>
{
    public void Configure(EntityTypeBuilder<MessageType> builder)
    {
        builder.Property(p => p.Type).HasMaxLength(100);

        #region HasData
        builder.HasData(
            new MessageType
            {
                Id = 1,
                Type = "Text"
            },
            new MessageType
            {
                Id = 2,
                Type = "Image"
            }
        );
        #endregion
        
    }
}