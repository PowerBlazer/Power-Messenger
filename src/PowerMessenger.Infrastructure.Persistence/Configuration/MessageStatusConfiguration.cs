using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Configuration;

public class MessageStatusConfiguration:IEntityTypeConfiguration<MessageStatus>
{
    public void Configure(EntityTypeBuilder<MessageStatus> builder)
    {
        builder.HasOne(p => p.Message)
            .WithMany(p => p.MessageStatuses)
            .HasForeignKey(p => p.LastMessageReadId);

        builder.HasOne(p => p.User)
            .WithMany(p => p.MessageStatuses)
            .HasForeignKey(p => p.UserId);

        builder.HasOne(p => p.Chat)
            .WithMany(p => p.MessageStatuses)
            .HasForeignKey(p => p.ChatId);

        #region HasData
        builder.HasData(
            new MessageStatus
            {
                Id = 1,
                ChatId = 1,
                UserId = 1,
                LastMessageReadId = 12
            },
            new MessageStatus
            {
                Id = 2,
                ChatId = 1,
                UserId = 2,
                LastMessageReadId = 11
            },
            new MessageStatus
            {
                Id = 3,
                ChatId = 2,
                UserId = 1,
                LastMessageReadId = 16
            },
            new MessageStatus
            {
                Id = 4,
                ChatId = 2,
                UserId = 2,
                LastMessageReadId = 14
            }
        );
        
        
        
        // builder.HasData(
        //     new MessageStatus
        //     {
        //         Id = 1,
        //         MessageId = 1,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 2,
        //         MessageId = 1,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 3,
        //         MessageId = 2,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 4,
        //         MessageId = 2,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 5,
        //         MessageId = 3,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 6,
        //         MessageId = 3,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 7,
        //         MessageId = 4,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 8,
        //         MessageId = 4,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 9,
        //         MessageId = 5,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 10,
        //         MessageId = 5,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 11,
        //         MessageId = 6,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 12,
        //         MessageId = 6,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 13,
        //         MessageId = 7,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 14,
        //         MessageId = 7,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 15,
        //         MessageId = 8,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 16,
        //         MessageId = 8,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 17,
        //         MessageId = 9,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 18,
        //         MessageId = 9,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 19,
        //         MessageId = 10,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 20,
        //         MessageId = 10,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 21,
        //         MessageId = 11,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 22,
        //         MessageId = 11,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 23,
        //         MessageId = 12,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 24,
        //         MessageId = 12,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 25,
        //         MessageId = 13,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 26,
        //         MessageId = 13,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 27,
        //         MessageId = 14,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 28,
        //         MessageId = 14,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 29,
        //         MessageId = 15,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 30,
        //         MessageId = 15,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 31,
        //         MessageId = 16,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 32,
        //         MessageId = 16,
        //         UserId = 2,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 33,
        //         MessageId = 17,
        //         UserId = 1,
        //         IsRead = false
        //     },
        //     new MessageStatus
        //     {
        //         Id = 34,
        //         MessageId = 17,
        //         UserId = 2,
        //         IsRead = false
        //     }
        // );
        #endregion
    }
}