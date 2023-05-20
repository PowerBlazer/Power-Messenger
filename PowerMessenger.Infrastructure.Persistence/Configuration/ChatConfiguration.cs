using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Configuration;

public class ChatConfiguration:IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        /*builder.HasOne(p => p.ChatType)
            .WithMany(p => p.Chats)
            .HasForeignKey(p => p.ChatTypeId);*/

        #region HasData
        builder.HasData(
            new Chat
            {
                Id = 1,
                Name = "Group1",
                Photo = "ChatsImage/efe4e2f6-d7b2-49f4-80bf-a2b5e8fa7178.jpg",
                Description = @"Чат для .NET разработчиков и C# программистов.",
                ChatTypeId = 2
                
            },
            new Chat
            {
                Id = 2,
                Name = "DOT.NET Talking",
                Photo = "ChatsImage/5009efc6-6891-44b6-8d20-68ec9a9199de.jpg",
                Description = @"Стараемся не флудить. Пишем по делу. Правила: https://t.me/professorweb/430450 Для флуда @svoboda_obsh",
                ChatTypeId = 2
            }
        );
        #endregion
    }
}