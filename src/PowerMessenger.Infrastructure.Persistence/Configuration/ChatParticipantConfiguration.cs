using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Configuration;

public class ChatParticipantConfiguration:IEntityTypeConfiguration<ChatParticipant>
{
    public void Configure(EntityTypeBuilder<ChatParticipant> builder)
    {
        builder.Property(p => p.Role).HasMaxLength(50);
        
        builder.HasOne(p => p.Chat)
            .WithMany(p => p.ChatParticipants)
            .HasForeignKey(p => p.ChatId);

        builder.HasOne(p => p.User)
            .WithMany(p => p.ChatParticipants)
            .HasForeignKey(p => p.UserId);

        #region HasData

        builder.HasData(new ChatParticipant
            {
                Id = 1,
                UserId = 1,
                ChatId = 1
            },
            new ChatParticipant
            {
                Id = 2,
                UserId = 2,
                ChatId = 1
            },
            new ChatParticipant
            {
                Id = 3,
                UserId = 1,
                ChatId = 2
            },
            new ChatParticipant
            {
                Id = 4,
                UserId = 2,
                ChatId = 2
            }
        );

        #endregion
    }
}