using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Configuration;

public class MessageConfiguration:IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasOne<Message>()
            .WithOne(p=>p.ForwardMessage)
            .HasForeignKey<Message>(p=>p.ForwardedMessageId);
        
        builder.HasOne(p => p.MessageType)
            .WithMany(p => p.Messages)
            .HasForeignKey(p => p.MessageTypeId);

        builder.HasOne(p => p.Chat)
            .WithMany(p => p.Messages)
            .HasForeignKey(p => p.ChatId);

        builder.HasOne(p => p.User)
            .WithMany(p => p.Messages)
            .HasForeignKey(p => p.UserId);

 

        #region HasData

        builder.HasData(new Message
            {
                Id = 1,
                UserId = 1,
                ChatId = 1,
                Content = "Привет",
                DateCreate = DateTime.Parse("2022-01-20 23:30:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 2,
                UserId = 2,
                ChatId = 1,
                Content = "Дарова",
                DateCreate = DateTime.Parse("2022-01-20 23:31:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 3,
                UserId = 1,
                ChatId = 1,
                DateCreate = DateTime.Parse("2022-01-20 23:32:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 4,
                UserId = 2,
                ChatId = 1,
                Content = "Нормально",
                DateCreate = DateTime.Parse("2022-01-20 23:33:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 5,
                UserId = 1,
                ChatId = 1,
                Content = "HelloWorld",
                DateCreate = DateTime.Parse("2022-01-20 23:34:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 6,
                UserId = 2,
                ChatId = 1,
                Content = @"Если бы не характер, то природная смекалка, хитрость и отвага, доходящая до авантюризма,
сделали бы его в наше время очень богатым",
                DateCreate = DateTime.Parse("2022-01-20 23:35:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 7,
                UserId = 1,
                ChatId = 1,
                Content = @"Eiusmod id pariatur reprehenderit minim ea est laboris. 
Do consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. 
Aliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.",
                DateCreate = DateTime.Parse("2022-01-20 23:36:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 8,
                UserId = 2,
                ChatId = 1,
                Content = @"Eiusmod id pariatur reprehenderit minim ea est laboris. 
Do consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. 
Aliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.",
                DateCreate = DateTime.Parse("2022-01-20 23:37:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 9,
                UserId = 1,
                ChatId = 1,
                Content = @"Eiusmod id pariatur reprehenderit minim ea est laboris. 
Do consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. 
Aliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.",
                DateCreate = DateTime.Parse("2022-01-20 23:38:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 10,
                UserId = 2,
                ChatId = 1,
                Content = @"Eiusmod id pariatur reprehenderit minim ea est laboris. 
Do consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. 
Aliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.",
                DateCreate = DateTime.Parse("2022-01-20 23:39:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 11,
                UserId = 1,
                ChatId = 1,
                Content = @"Cupidatat exercitation et culpa nisi consectetur laborum eu voluptate enim deserunt nostrud.
Voluptate id nulla exercitation enim do qui elit proident ullamco qui pariatur cillum. 
Pariatur ea eu duis laborum occaecat deserunt.",
                DateCreate = DateTime.Parse("2022-01-20 23:40:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 12,
                UserId = 2,
                ChatId = 1,
                Content = "Eiusmod dolore est id ipsum mollit ex.",
                DateCreate = DateTime.Parse("2022-01-20 23:41:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 13,
                UserId = 1,
                ChatId = 1,
                Content = "Dolore enim ea est incididunt do",
                DateCreate = DateTime.Parse("2022-01-20 23:42:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 14,
                UserId = 1,
                ChatId = 2,
                Content = "Eiusmod dolore est id ipsum mollit ex.",
                DateCreate = DateTime.Parse("2022-01-20 23:39:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 15,
                UserId = 2,
                ChatId = 2,
                Content = @"Cupidatat exercitation et culpa nisi consectetur laborum eu voluptate enim deserunt nostrud.
Voluptate id nulla exercitation enim do qui elit proident ullamco qui pariatur cillum. 
Pariatur ea eu duis laborum occaecat deserunt.",
                DateCreate = DateTime.Parse("2022-01-20 23:40:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 16,
                UserId = 2,
                ChatId = 2,
                Content = "Eiusmod dolore est id ipsum mollit ex.",
                DateCreate = DateTime.Parse("2022-01-20 23:41:00"),
                MessageTypeId = 1
            },
            new Message
            {
                Id = 17,
                UserId = 2,
                ChatId = 2,
                Content = "Dolore enim ea est incididunt do",
                DateCreate = DateTime.Parse("2022-01-20 23:42:00"),
                MessageTypeId = 1
            }
        );

        #endregion



    }
}