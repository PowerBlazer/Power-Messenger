using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Configuration;

public class MessageConfiguration:IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasOne(p=> p.ForwardMessage)
            .WithMany()
            .HasForeignKey(p => p.ForwardedMessageId)
            .OnDelete(DeleteBehavior.Restrict);
        
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
            },
            new Message
            {
                Id = 33,
                UserId = 2,
                ChatId = 2,
                Content = "Привет! Как дела?",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:08+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 32
            },
            new Message
            {
                Id = 34,
                UserId = 1,
                ChatId = 2,
                Content = "Привет! У меня все хорошо. Как твои дела?",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:09+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 33
            },
            new Message
            {
                Id = 35,
                UserId = 2,
                ChatId = 2,
                Content = "У меня тоже все отлично. Планируешь что-нибудь интересное на выходные?",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:10+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 34
            },
            new Message
            {
                Id = 36,
                UserId = 1,
                ChatId = 2,
                Content = "Да, собираюсь с друзьями в парк на пикник. Будет весело!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:11+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 35
            },
            new Message
            {
                Id = 37,
                UserId = 2,
                ChatId = 2,
                Content = "Отличная идея! Хорошо проведите время.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:12+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 36
            },
            new Message
            {
                Id = 38,
                UserId = 1,
                ChatId = 2,
                Content = "Спасибо! Будем стараться!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:13+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 37
            },
            new Message
            {
                Id = 39,
                UserId = 2,
                ChatId = 2,
                Content = "Какой фильм смотрели на прошлой неделе?",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:14+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 38
            },
            new Message
            {
                Id = 40,
                UserId = 1,
                ChatId = 2,
                Content = "Мы посмотрели комедию 'Август. Восьмого'. Очень смешной фильм!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:15+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 39
            },
            new Message
            {
                Id = 41,
                UserId = 2,
                ChatId = 2,
                Content = "Я слышал о нем! Действительно, забавная комедия.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:16+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 40
            },
            new Message
            {
                Id = 42,
                UserId = 1,
                ChatId = 2,
                Content = "Очень рекомендую посмотреть, если у тебя будет свободное время.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:17+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 41
            },
            new Message
            {
                Id = 43,
                UserId = 2,
                ChatId = 2,
                Content = "Спасибо за рекомендацию! Обязательно добавлю в свой список фильмов.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:18+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 42
            },
            new Message
            {
                Id = 44,
                UserId = 1,
                ChatId = 2,
                Content = "Нет проблем! Уверен, тебе понравится.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:19+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 43
            },
            new Message
            {
                Id = 45,
                UserId = 2,
                ChatId = 2,
                Content = "Что-нибудь интересное произошло у тебя на этой неделе?",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:20+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 44
            },
            new Message
            {
                Id = 46,
                UserId = 1,
                ChatId = 2,
                Content = "Ничего особенного, но я получил повышение на работе! Я очень рад!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:21+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 45
            },
            new Message
            {
                Id = 47,
                UserId = 2,
                ChatId = 2,
                Content = "Поздравляю! Ты действительно заслужил это повышение.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:22+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 46
            },
            new Message
            {
                Id = 48,
                UserId = 1,
                ChatId = 2,
                Content = "Спасибо! Я очень старался и рад, что моя работа оценена.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:23+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 47
            },
            new Message
            {
                Id = 49,
                UserId = 2,
                ChatId = 2,
                Content = "Теперь у тебя будет еще больше возможностей и достижений!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:24+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 48
            },
            new Message
            {
                Id = 50,
                UserId = 1,
                ChatId = 2,
                Content = "Да, я очень мотивирован и готов взяться за новые проекты.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:25+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 49
            },
            new Message
            {
                Id = 51,
                UserId = 2,
                ChatId = 2,
                Content = "Удачи тебе во всем! Ты сможешь добиться большого!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:26+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 50
            },
            new Message
            {
                Id = 52,
                UserId = 1,
                ChatId = 2,
                Content = "Спасибо за поддержку! Я ценю это.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:27+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 51
            },
            new Message
            {
                Id = 53,
                UserId = 2,
                ChatId = 2,
                Content = "Не за что! Всегда готов помочь и поддержать.",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:28+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 52
            },
            new Message
            {
                Id = 54,
                UserId = 1,
                ChatId = 2,
                Content = "Спасибо, друг! Ты лучший!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:29+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 53
            },
            new Message
            {
                Id = 55,
                UserId = 2,
                ChatId = 2,
                Content = "И ты тоже, брат! Любые проблемы - обращайся!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:30+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 54
            },
            new Message
            {
                Id = 56,
                UserId = 1,
                ChatId = 2,
                Content = "Конечно, буду иметь в виду! Спасибо за поддержку и дружбу!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:31+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 55
            },
            new Message
            {
                Id = 57,
                UserId = 2,
                ChatId = 2,
                Content = "Взаимно, братан! Дружба навсегда!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:32+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 56
            },
            new Message
            {
                Id = 58,
                UserId = 1,
                ChatId = 2,
                Content = "Да, навсегда! Будем держаться вместе!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:33+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 57
            },
            new Message
            {
                Id = 59,
                UserId = 2,
                ChatId = 2,
                Content = "Так точно! Ни шагу назад!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:34+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 58
            },
            new Message
            {
                Id = 60,
                UserId = 1,
                ChatId = 2,
                Content = "Вперед, к новым приключениям!",
                DateCreate = DateTimeOffset.Parse("2022-12-20T23:47:35+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 59
            },
            new Message
            {
                Id = 61,
                UserId = 2,
                ChatId = 1,
                Content = "Привет! Как твои дела?",
                DateCreate = DateTimeOffset.Parse("2022-12-21T10:15:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false
            },
            new Message
            {
                Id = 62,
                UserId = 1,
                ChatId = 1,
                Content = "Привет! Всё отлично, спасибо! Как у тебя?",
                DateCreate = DateTimeOffset.Parse("2022-12-21T10:16:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 61
            },
            new Message
            {
                Id = 63,
                UserId = 2,
                ChatId = 1,
                Content = "У меня тоже всё хорошо. Планируешь что-нибудь интересное на выходные?",
                DateCreate = DateTimeOffset.Parse("2022-12-21T10:17:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 62
            },
            new Message
            {
                Id = 64,
                UserId = 1,
                ChatId = 1,
                Content = "Да, у меня есть небольшие планы. Хочу сходить в парк и поиграть в футбол.",
                DateCreate = DateTimeOffset.Parse("2022-12-21T10:18:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 63
            },
            new Message
            {
                Id = 65,
                UserId = 2,
                ChatId = 1,
                Content = "Отличная идея! Погода должна быть хорошей. Удачи в игре!",
                DateCreate = DateTimeOffset.Parse("2022-12-21T10:19:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 63
            },
            new Message
            {
                Id = 66,
                UserId = 1,
                ChatId = 1,
                Content = "Спасибо! Буду стараться показать хороший результат.",
                DateCreate = DateTimeOffset.Parse("2022-12-21T10:20:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 65
            },
            new Message
            {
                Id = 67,
                UserId = 2,
                ChatId = 1,
                Content = "У тебя есть какие-нибудь планы на вечер?",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:30:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 66
            },
            new Message
            {
                Id = 68,
                UserId = 1,
                ChatId = 1,
                Content = "На вечер у меня нет особых планов. Возможно, просто отдохну дома и посмотрю фильм.",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:31:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 67
            },
            new Message
            {
                Id = 69,
                UserId = 2,
                ChatId = 1,
                Content = "Это звучит как хороший вариант. Можешь порекомендовать какой-нибудь фильм?",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:32:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 68
            },
            new Message
            {
                Id = 70,
                UserId = 1,
                ChatId = 1,
                Content = "Конечно! Я недавно посмотрел фильм 'Интерстеллар'. Он очень увлекательный и захватывающий.",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:33:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 69
            },
            new Message
            {
                Id = 71,
                UserId = 2,
                ChatId = 1,
                Content = "Спасибо за рекомендацию! Обязательно посмотрю.",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:34:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 70
            },
            new Message
            {
                Id = 72,
                UserId = 1,
                ChatId = 1,
                Content = "Пожалуйста! Уверен, тебе понравится. Наслаждайся просмотром!",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:35:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 71
            },
            new Message
            {
                Id = 73,
                UserId = 2,
                ChatId = 1,
                Content = "Спасибо! Ты всегда помогаешь выбрать хорошее развлечение.",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:36:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 72
            },
            new Message
            {
                Id = 74,
                UserId = 1,
                ChatId = 1,
                Content = "Рад, что моя помощь пригождается! Если у тебя есть еще вопросы или нужна помощь, обращайся.",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:37:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 73
            },
            new Message
            {
                Id = 75,
                UserId = 2,
                ChatId = 1,
                Content = "Безусловно! Я всегда знаю, кому обратиться, когда нужна помощь или совет.",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:38:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 74
            },
            new Message
            {
                Id = 76,
                UserId = 1,
                ChatId = 1,
                Content = "Спасибо за доверие! Я всегда готов поддержать друзей.",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:39:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 75
            },
            new Message
            {
                Id = 77,
                UserId = 2,
                ChatId = 1,
                Content = "Ты лучший друг! Ценю нашу дружбу.",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:40:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 76
            },
            new Message
            {
                Id = 78,
                UserId = 1,
                ChatId = 1,
                Content = "Спасибо! Я также очень ценю нашу дружбу. Мы всегда поддержим друг друга.",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:41:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 77
            },
            new Message
            {
                Id = 79,
                UserId = 2,
                ChatId = 1,
                Content = "Так точно! Дружба навсегда!",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:42:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 78
            },
            new Message
            {
                Id = 80,
                UserId = 1,
                ChatId = 1,
                Content = "Ни шагу назад! Вместе мы сила!",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:43:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 79
            },
            new Message
            {
                Id = 81,
                UserId = 2,
                ChatId = 1,
                Content = "Точно! Ничто не сможет нас остановить!",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:44:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 80
            },
            new Message
            {
                Id = 82,
                UserId = 1,
                ChatId = 1,
                Content = "Вперед, к новым победам и успехам!",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:45:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 81
            },
            new Message
            {
                Id = 83,
                UserId = 1,
                ChatId = 1,
                Content = "Согласен! Мы не останавливаемся на достигнутом, а стремимся к большему!",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:45:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 82
            },
            new Message
            {
                Id = 84,
                UserId = 2,
                ChatId = 1,
                Content = "Точно! Вместе мы неуязвимы!",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:46:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 83
            },
            new Message
            {
                Id = 85,
                UserId = 1,
                ChatId = 1,
                Content = "Да, мы команда, сплоченная и непобедимая!",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:47:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 84
            },
            new Message
            {
                Id = 86,
                UserId = 2,
                ChatId = 1,
                Content = "Всегда готов поддержать тебя в любых начинаниях!",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:48:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 85
            },
            new Message
            {
                Id = 87,
                UserId = 1,
                ChatId = 1,
                Content = "Спасибо, друг! Твоя поддержка очень важна для меня.",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:49:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 86
            },
            new Message
            {
                Id = 88,
                UserId = 2,
                ChatId = 1,
                Content = "Всегда рад помочь! Мы вместе сила!",
                DateCreate = DateTimeOffset.Parse("2022-12-21T18:50:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 87
            },
            new Message
            {
                Id = 89,
                UserId = 2,
                ChatId = 1,
                Content = "Кстати, слышал о новой выставке искусства? Может быть, пойдем вместе?",
                DateCreate = DateTimeOffset.Parse("2022-12-22T09:30:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = null
            },
            new Message
            {
                Id = 90,
                UserId = 1,
                ChatId = 1,
                Content = "О, это звучит интересно! Когда и где проходит выставка?",
                DateCreate = DateTimeOffset.Parse("2022-12-22T09:31:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 89
            },
            new Message
            {
                Id = 91,
                UserId = 2,
                ChatId = 1,
                Content = "Выставка проходит в местной галерее с 25 декабря по 10 января. Можем сходить в выходные.",
                DateCreate = DateTimeOffset.Parse("2022-12-22T09:32:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 90
            },
            new Message
            {
                Id = 92,
                UserId = 1,
                ChatId = 1,
                Content = "Отлично! Я с удовольствием присоединюсь. Выставки искусства всегда вдохновляют меня.",
                DateCreate = DateTimeOffset.Parse("2022-12-22T09:33:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 91
            },
            new Message
            {
                Id = 93,
                UserId = 2,
                ChatId = 1,
                Content = "Прекрасно! Уверен, мы сможем насладиться множеством талантливых работ.",
                DateCreate = DateTimeOffset.Parse("2022-12-22T09:34:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 92
            },
            new Message
            {
                Id = 94,
                UserId = 1,
                ChatId = 1,
                Content = "Точно! Искусство способно пробудить в нас самые глубокие эмоции и мысли.",
                DateCreate = DateTimeOffset.Parse("2022-12-22T09:35:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 93
            },
            new Message
            {
                Id = 95,
                UserId = 2,
                ChatId = 1,
                Content = "Не терпится увидеть выставку! Будет замечательное время вместе.",
                DateCreate = DateTimeOffset.Parse("2022-12-22T09:36:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 94
            },
            new Message
            {
                Id = 96,
                UserId = 1,
                ChatId = 1,
                Content = "Согласен, это будет отличная возможность провести время вместе и обсудить впечатления.",
                DateCreate = DateTimeOffset.Parse("2022-12-22T09:37:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 95
            },
            new Message
            {
                Id = 97,
                UserId = 2,
                ChatId = 1,
                Content = "Точно! Не могу дождаться выходных. Давай встретимся возле галереи в субботу в 12:00?",
                DateCreate = DateTimeOffset.Parse("2022-12-22T09:38:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 96
            },
            new Message
            {
                Id = 98,
                UserId = 1,
                ChatId = 1,
                Content = "Отлично, договорились! Увидимся в субботу возле галереи в 12:00. Буду ждать с нетерпением!",
                DateCreate = DateTimeOffset.Parse("2022-12-22T09:39:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 97
            },
            new Message
            {
                Id = 99,
                UserId = 2,
                ChatId = 1,
                Content = "Супер! Уже не могу дождаться нашей встречи. До субботы!",
                DateCreate = DateTimeOffset.Parse("2022-12-22T09:40:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 98
            },
            new Message
            {
                Id = 100,
                UserId = 1,
                ChatId = 1,
                Content = "До встречи! Приятного дня и до скорого!",
                DateCreate = DateTimeOffset.Parse("2022-12-22T09:41:00+03:00"),
                MessageTypeId = 1,
                DeletedByAll = false,
                ForwardedMessageId = 99
            }
        );

        #endregion
    }
}