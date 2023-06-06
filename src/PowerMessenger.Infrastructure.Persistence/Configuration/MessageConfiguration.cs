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
            .HasPrincipalKey(p=>p.UserId)
            .HasForeignKey(p => p.UserId);
        
        builder.HasOne(p => p.DeletedByUser)
            .WithMany(p => p.DeletedMessages)
            .HasPrincipalKey(p=>p.UserId)
            .HasForeignKey(p => p.DeletedByUserId);
        
        #region HasData

        builder.HasData(new Message
            {
                Id = 1,
                UserId = 1,
                ChatId = 1,
                Content = "Привет",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:30:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 2,
                UserId = 2,
                ChatId = 1,
                Content = "Дарова",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:31:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 3,
                UserId = 1,
                ChatId = 1,
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:32:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 4,
                UserId = 2,
                ChatId = 1,
                Content = "Нормально",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:33:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 5,
                UserId = 1,
                ChatId = 1,
                Content = "HelloWorld",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:34:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                
            },
            new Message
            {
                Id = 6,
                UserId = 2,
                ChatId = 1,
                Content = @"Если бы не характер, то природная смекалка, хитрость и отвага, доходящая до авантюризма,
сделали бы его в наше время очень богатым",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:35:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 7,
                UserId = 1,
                ChatId = 1,
                Content = @"Eiusmod id pariatur reprehenderit minim ea est laboris. 
Do consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. 
Aliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:36:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 8,
                UserId = 2,
                ChatId = 1,
                Content = @"Eiusmod id pariatur reprehenderit minim ea est laboris. 
Do consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. 
Aliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:37:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 9,
                UserId = 1,
                ChatId = 1,
                Content = @"Eiusmod id pariatur reprehenderit minim ea est laboris. 
Do consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. 
Aliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:38:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 10,
                UserId = 2,
                ChatId = 1,
                Content = @"Eiusmod id pariatur reprehenderit minim ea est laboris. 
Do consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. 
Aliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:39:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 11,
                UserId = 1,
                ChatId = 1,
                Content = @"Cupidatat exercitation et culpa nisi consectetur laborum eu voluptate enim deserunt nostrud.
Voluptate id nulla exercitation enim do qui elit proident ullamco qui pariatur cillum. 
Pariatur ea eu duis laborum occaecat deserunt.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:40:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 12,
                UserId = 2,
                ChatId = 1,
                Content = "Eiusmod dolore est id ipsum mollit ex.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:41:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 13,
                UserId = 1,
                ChatId = 1,
                Content = "Dolore enim ea est incididunt do",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:42:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 14,
                UserId = 1,
                ChatId = 2,
                Content = "Eiusmod dolore est id ipsum mollit ex.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:43:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 15,
                UserId = 2,
                ChatId = 2,
                Content = @"Cupidatat exercitation et culpa nisi consectetur laborum eu voluptate enim deserunt nostrud.
Voluptate id nulla exercitation enim do qui elit proident ullamco qui pariatur cillum. 
Pariatur ea eu duis laborum occaecat deserunt.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:44:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 16,
                UserId = 2,
                ChatId = 2,
                Content = "Eiusmod dolore est id ipsum mollit ex.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:45:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 17,
                UserId = 2,
                ChatId = 2,
                Content = "Dolore enim ea est incididunt do",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 18,
                UserId = 1,
                ChatId = 2,
                Content = "Привет!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:01+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = null
            },
            new Message
            {
                Id = 19,
                UserId = 2,
                ChatId = 2,
                Content = "Как дела?",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:02+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 18
            },
            new Message
            {
                Id = 20,
                UserId = 1,
                ChatId = 2,
                Content = "Отлично!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:03+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 19
            },
            new Message
            {
                Id = 21,
                UserId = 2,
                ChatId = 2,
                Content = "Что делаешь?",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:04+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 20
            },
            new Message
            {
                Id = 22,
                UserId = 1,
                ChatId = 2,
                Content = "Планирую поход в кино.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:05+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 21
            },
            new Message
            {
                Id = 23,
                UserId = 2,
                ChatId = 2,
                Content = "Какой фильм собираешься смотреть?",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:06+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 22
            },
            new Message
            {
                Id = 24,
                UserId = 1,
                ChatId = 2,
                Content = "Думаю посмотреть новый боевик.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:07+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 23
            },
            new Message
            {
                Id = 25,
                UserId = 2,
                ChatId = 2,
                Content = "Звучит интересно. Какие еще планы на выходные?",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:08+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 24
            },
            new Message
            {
                Id = 26,
                UserId = 1,
                ChatId = 2,
                Content = "Надеюсь провести время с семьей и отдохнуть.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:09+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 25
            },
            new Message
            {
                Id = 27,
                UserId = 2,
                ChatId = 2,
                Content = "Отличные планы! Удачи вам!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:10+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 26
            },
            new Message
            {
                Id = 28,
                UserId = 1,
                ChatId = 2,
                Content = "Спасибо! Буду стараться!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:11+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 27
            },
            new Message
            {
                Id = 29,
                UserId = 2,
                ChatId = 2,
                Content = "Как прошел поход в кино?",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:12+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 28
            },
            new Message
            {
                Id = 30,
                UserId = 1,
                ChatId = 2,
                Content = "Было здорово! Фильм понравился.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:13+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 29
            },
            new Message
            {
                Id = 31,
                UserId = 2,
                ChatId = 2,
                Content = "Рад, что вам понравилось!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:14+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 30
            },
            new Message
            {
                Id = 32,
                UserId = 1,
                ChatId = 2,
                Content = "Спасибо за рекомендацию фильма.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:46:15+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 31
            }
        );

        #endregion
    }
}