﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PowerMessenger.Infrastructure.Persistence.Context;

#nullable disable

namespace PowerMessenger.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(EfContext))]
    partial class EfContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PowerMessenger.Domain.Entities.Chat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ChatTypeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Photo")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChatTypeId");

                    b.ToTable("Chats", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ChatTypeId = 2L,
                            DateCreate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Чат для .NET разработчиков и C# программистов.",
                            Name = "Group1",
                            Photo = "ChatsImage/efe4e2f6-d7b2-49f4-80bf-a2b5e8fa7178.jpg"
                        },
                        new
                        {
                            Id = 2L,
                            ChatTypeId = 2L,
                            DateCreate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Стараемся не флудить. Пишем по делу. Правила: https://t.me/professorweb/430450 Для флуда @svoboda_obsh",
                            Name = "DOT.NET Talking",
                            Photo = "ChatsImage/5009efc6-6891-44b6-8d20-68ec9a9199de.jpg"
                        });
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.ChatParticipant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("Role")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatParticipants", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ChatId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            ChatId = 1L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            ChatId = 2L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 4L,
                            ChatId = 2L,
                            UserId = 2L
                        });
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.ChatType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("ChatTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Type = "Personal"
                        },
                        new
                        {
                            Id = 2L,
                            Type = "Group"
                        });
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("ForwardedMessageId")
                        .HasColumnType("bigint");

                    b.Property<long>("MessageTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("ForwardedMessageId")
                        .IsUnique();

                    b.HasIndex("MessageTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ChatId = 1L,
                            Content = "Привет",
                            DateCreate = new DateTime(2022, 1, 20, 23, 30, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            ChatId = 1L,
                            Content = "Дарова",
                            DateCreate = new DateTime(2022, 1, 20, 23, 31, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            ChatId = 1L,
                            DateCreate = new DateTime(2022, 1, 20, 23, 32, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 4L,
                            ChatId = 1L,
                            Content = "Нормально",
                            DateCreate = new DateTime(2022, 1, 20, 23, 33, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 5L,
                            ChatId = 1L,
                            Content = "HelloWorld",
                            DateCreate = new DateTime(2022, 1, 20, 23, 34, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 6L,
                            ChatId = 1L,
                            Content = "Если бы не характер, то природная смекалка, хитрость и отвага, доходящая до авантюризма,\r\nсделали бы его в наше время очень богатым",
                            DateCreate = new DateTime(2022, 1, 20, 23, 35, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 7L,
                            ChatId = 1L,
                            Content = "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.",
                            DateCreate = new DateTime(2022, 1, 20, 23, 36, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 8L,
                            ChatId = 1L,
                            Content = "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.",
                            DateCreate = new DateTime(2022, 1, 20, 23, 37, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 9L,
                            ChatId = 1L,
                            Content = "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.",
                            DateCreate = new DateTime(2022, 1, 20, 23, 38, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 10L,
                            ChatId = 1L,
                            Content = "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.",
                            DateCreate = new DateTime(2022, 1, 20, 23, 39, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 11L,
                            ChatId = 1L,
                            Content = "Cupidatat exercitation et culpa nisi consectetur laborum eu voluptate enim deserunt nostrud.\r\nVoluptate id nulla exercitation enim do qui elit proident ullamco qui pariatur cillum. \r\nPariatur ea eu duis laborum occaecat deserunt.",
                            DateCreate = new DateTime(2022, 1, 20, 23, 40, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 12L,
                            ChatId = 1L,
                            Content = "Eiusmod dolore est id ipsum mollit ex.",
                            DateCreate = new DateTime(2022, 1, 20, 23, 41, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 13L,
                            ChatId = 1L,
                            Content = "Dolore enim ea est incididunt do",
                            DateCreate = new DateTime(2022, 1, 20, 23, 42, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 14L,
                            ChatId = 2L,
                            Content = "Eiusmod dolore est id ipsum mollit ex.",
                            DateCreate = new DateTime(2022, 1, 20, 23, 39, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 15L,
                            ChatId = 2L,
                            Content = "Cupidatat exercitation et culpa nisi consectetur laborum eu voluptate enim deserunt nostrud.\r\nVoluptate id nulla exercitation enim do qui elit proident ullamco qui pariatur cillum. \r\nPariatur ea eu duis laborum occaecat deserunt.",
                            DateCreate = new DateTime(2022, 1, 20, 23, 40, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 16L,
                            ChatId = 2L,
                            Content = "Eiusmod dolore est id ipsum mollit ex.",
                            DateCreate = new DateTime(2022, 1, 20, 23, 41, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 17L,
                            ChatId = 2L,
                            Content = "Dolore enim ea est incididunt do",
                            DateCreate = new DateTime(2022, 1, 20, 23, 42, 0, 0, DateTimeKind.Unspecified),
                            MessageTypeId = 1L,
                            UserId = 2L
                        });
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.MessageStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<long>("MessageId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("UserId");

                    b.ToTable("MessageStatuses", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            IsRead = false,
                            MessageId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            IsRead = false,
                            MessageId = 1L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            IsRead = false,
                            MessageId = 2L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 4L,
                            IsRead = false,
                            MessageId = 2L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 5L,
                            IsRead = false,
                            MessageId = 3L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 6L,
                            IsRead = false,
                            MessageId = 3L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 7L,
                            IsRead = false,
                            MessageId = 4L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 8L,
                            IsRead = false,
                            MessageId = 4L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 9L,
                            IsRead = false,
                            MessageId = 5L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 10L,
                            IsRead = false,
                            MessageId = 5L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 11L,
                            IsRead = false,
                            MessageId = 6L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 12L,
                            IsRead = false,
                            MessageId = 6L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 13L,
                            IsRead = false,
                            MessageId = 7L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 14L,
                            IsRead = false,
                            MessageId = 7L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 15L,
                            IsRead = false,
                            MessageId = 8L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 16L,
                            IsRead = false,
                            MessageId = 8L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 17L,
                            IsRead = false,
                            MessageId = 9L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 18L,
                            IsRead = false,
                            MessageId = 9L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 19L,
                            IsRead = false,
                            MessageId = 10L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 20L,
                            IsRead = false,
                            MessageId = 10L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 21L,
                            IsRead = false,
                            MessageId = 11L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 22L,
                            IsRead = false,
                            MessageId = 11L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 23L,
                            IsRead = false,
                            MessageId = 12L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 24L,
                            IsRead = false,
                            MessageId = 12L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 25L,
                            IsRead = false,
                            MessageId = 13L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 26L,
                            IsRead = false,
                            MessageId = 13L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 27L,
                            IsRead = false,
                            MessageId = 14L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 28L,
                            IsRead = false,
                            MessageId = 14L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 29L,
                            IsRead = false,
                            MessageId = 15L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 30L,
                            IsRead = false,
                            MessageId = 15L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 31L,
                            IsRead = false,
                            MessageId = 16L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 32L,
                            IsRead = false,
                            MessageId = 16L,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 33L,
                            IsRead = false,
                            MessageId = 17L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 34L,
                            IsRead = false,
                            MessageId = 17L,
                            UserId = 2L
                        });
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.MessageType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("MessageTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Type = "Text"
                        },
                        new
                        {
                            Id = 2L,
                            Type = "Image"
                        });
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Theme")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            DateCreated = new DateTime(2023, 4, 19, 20, 58, 53, 755, DateTimeKind.Local).AddTicks(3332),
                            Email = "power@mail.ru",
                            UserId = 1L,
                            UserName = "PowerBlaze"
                        },
                        new
                        {
                            Id = 2L,
                            DateCreated = new DateTime(2023, 4, 19, 20, 58, 53, 755, DateTimeKind.Local).AddTicks(3348),
                            Email = "tower@mail.ru",
                            UserId = 2L,
                            UserName = "TowerBlaze"
                        });
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.Chat", b =>
                {
                    b.HasOne("PowerMessenger.Domain.Entities.ChatType", "ChatType")
                        .WithMany("Chats")
                        .HasForeignKey("ChatTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatType");
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.ChatParticipant", b =>
                {
                    b.HasOne("PowerMessenger.Domain.Entities.Chat", "Chat")
                        .WithMany("ChatParticipants")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PowerMessenger.Domain.Entities.User", "User")
                        .WithMany("ChatParticipants")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.Message", b =>
                {
                    b.HasOne("PowerMessenger.Domain.Entities.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PowerMessenger.Domain.Entities.Message", null)
                        .WithOne("ForwardMessage")
                        .HasForeignKey("PowerMessenger.Domain.Entities.Message", "ForwardedMessageId");

                    b.HasOne("PowerMessenger.Domain.Entities.MessageType", "MessageType")
                        .WithMany("Messages")
                        .HasForeignKey("MessageTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PowerMessenger.Domain.Entities.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("MessageType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.MessageStatus", b =>
                {
                    b.HasOne("PowerMessenger.Domain.Entities.Message", "Message")
                        .WithMany("MessageStatuses")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PowerMessenger.Domain.Entities.User", "User")
                        .WithMany("MessageStatuses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.Chat", b =>
                {
                    b.Navigation("ChatParticipants");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.ChatType", b =>
                {
                    b.Navigation("Chats");
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.Message", b =>
                {
                    b.Navigation("ForwardMessage");

                    b.Navigation("MessageStatuses");
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.MessageType", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("PowerMessenger.Domain.Entities.User", b =>
                {
                    b.Navigation("ChatParticipants");

                    b.Navigation("MessageStatuses");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
