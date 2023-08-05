using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PowerMessenger.Domain.Common;
using PowerMessenger.Infrastructure.Persistence.NpgSetting;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PowerMessenger.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PersistenceMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chat_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "message_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    user_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    avatar = table.Column<string>(type: "text", nullable: true),
                    date_of_birth = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    theme = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.UniqueConstraint("ak_users_user_id", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    date_create = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    photo = table.Column<string>(type: "text", nullable: true),
                    chat_type_id = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chats", x => x.id);
                    table.ForeignKey(
                        name: "fk_chats_chat_types_chat_type_id",
                        column: x => x.chat_type_id,
                        principalTable: "chat_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_participants",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    chat_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_participants", x => x.id);
                    table.ForeignKey(
                        name: "fk_chat_participants_chats_chat_id",
                        column: x => x.chat_id,
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chat_participants_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    chat_id = table.Column<long>(type: "bigint", nullable: false),
                    date_create = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    message_type_id = table.Column<long>(type: "bigint", nullable: false),
                    forwarded_message_id = table.Column<long>(type: "bigint", nullable: true),
                    source = table.Column<string>(type: "text", nullable: true),
                    deleted_by_user_id = table.Column<long>(type: "bigint", nullable: true),
                    deleted_by_all = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_messages_chats_chat_id",
                        column: x => x.chat_id,
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_messages_message_types_message_type_id",
                        column: x => x.message_type_id,
                        principalTable: "message_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_messages_messages_forward_message_id",
                        column: x => x.forwarded_message_id,
                        principalTable: "messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_messages_users_deleted_by_user_id",
                        column: x => x.deleted_by_user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_messages_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "message_statuses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    chat_id = table.Column<long>(type: "bigint", nullable: false),
                    last_message_read_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_statuses", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_statuses_chats_chat_id",
                        column: x => x.chat_id,
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_statuses_messages_message_id",
                        column: x => x.last_message_read_id,
                        principalTable: "messages",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_message_statuses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "chat_types",
                columns: new[] { "id", "type" },
                values: new object[,]
                {
                    { 1L, "Personal" },
                    { 2L, "Group" }
                });

            migrationBuilder.InsertData(
                table: "message_types",
                columns: new[] { "id", "type" },
                values: new object[,]
                {
                    { 1L, "Text" },
                    { 2L, "Image" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "date_of_birth", "theme", "user_id", "user_name" },
                values: new object[,]
                {
                    { 1L, null, null, null, 1L, "PowerBlaze" },
                    { 2L, null, null, null, 2L, "TowerBlaze" }
                });

            migrationBuilder.InsertData(
                table: "chats",
                columns: new[] { "id", "chat_type_id", "date_create", "description", "name", "photo" },
                values: new object[,]
                {
                    { 1L, 2L, new DateTimeOffset(new DateTime(2023, 6, 20, 16, 7, 4, 359, DateTimeKind.Unspecified).AddTicks(3441), new TimeSpan(0, 0, 0, 0, 0)), "Чат для .NET разработчиков и C# программистов.", "Group1", "ChatsImage/efe4e2f6-d7b2-49f4-80bf-a2b5e8fa7178.jpg" },
                    { 2L, 2L, new DateTimeOffset(new DateTime(2023, 6, 20, 16, 7, 4, 359, DateTimeKind.Unspecified).AddTicks(3445), new TimeSpan(0, 0, 0, 0, 0)), "Стараемся не флудить. Пишем по делу. Правила: https://t.me/professorweb/430450 Для флуда @svoboda_obsh", "DOT.NET Talking", "ChatsImage/5009efc6-6891-44b6-8d20-68ec9a9199de.jpg" }
                });

            migrationBuilder.InsertData(
                table: "chat_participants",
                columns: new[] { "id", "chat_id", "role", "user_id" },
                values: new object[,]
                {
                    { 1L, 1L, null, 1L },
                    { 2L, 1L, null, 2L },
                    { 3L, 2L, null, 1L },
                    { 4L, 2L, null, 2L }
                });

            migrationBuilder.InsertData(
                table: "messages",
                columns: new[] { "id", "chat_id", "content", "date_create", "deleted_by_all", "deleted_by_user_id", "forwarded_message_id", "message_type_id", "source", "user_id" },
                values: new object[,]
                {
                    { 1L, 1L, "Привет", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 1L },
                    { 2L, 1L, "Дарова", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 31, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 2L },
                    { 3L, 1L, null, new DateTimeOffset(new DateTime(2022, 12, 20, 23, 32, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 1L },
                    { 4L, 1L, "Нормально", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 33, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 2L },
                    { 5L, 1L, "HelloWorld", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 34, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 1L },
                    { 6L, 1L, "Если бы не характер, то природная смекалка, хитрость и отвага, доходящая до авантюризма,\r\nсделали бы его в наше время очень богатым", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 35, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 2L },
                    { 7L, 1L, "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 36, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 1L },
                    { 8L, 1L, "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 37, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 2L },
                    { 9L, 1L, "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 38, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 1L },
                    { 10L, 1L, "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 39, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 2L },
                    { 11L, 1L, "Cupidatat exercitation et culpa nisi consectetur laborum eu voluptate enim deserunt nostrud.\r\nVoluptate id nulla exercitation enim do qui elit proident ullamco qui pariatur cillum. \r\nPariatur ea eu duis laborum occaecat deserunt.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 1L },
                    { 12L, 1L, "Eiusmod dolore est id ipsum mollit ex.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 41, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 2L },
                    { 13L, 1L, "Dolore enim ea est incididunt do", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 42, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 1L },
                    { 14L, 2L, "Eiusmod dolore est id ipsum mollit ex.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 43, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 1L },
                    { 15L, 2L, "Cupidatat exercitation et culpa nisi consectetur laborum eu voluptate enim deserunt nostrud.\r\nVoluptate id nulla exercitation enim do qui elit proident ullamco qui pariatur cillum. \r\nPariatur ea eu duis laborum occaecat deserunt.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 44, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 2L },
                    { 16L, 2L, "Eiusmod dolore est id ipsum mollit ex.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 2L },
                    { 17L, 2L, "Dolore enim ea est incididunt do", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 2L },
                    { 18L, 2L, "Привет!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 1L },
                    { 61L, 1L, "Привет! Как твои дела?", new DateTimeOffset(new DateTime(2022, 12, 21, 10, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 2L },
                    { 89L, 1L, "Кстати, слышал о новой выставке искусства? Может быть, пойдем вместе?", new DateTimeOffset(new DateTime(2022, 12, 22, 9, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 2L }
                });

            migrationBuilder.InsertData(
                table: "message_statuses",
                columns: new[] { "id", "chat_id", "last_message_read_id", "user_id" },
                values: new object[,]
                {
                    { 1L, 1L, 12L, 1L },
                    { 2L, 1L, 11L, 2L },
                    { 3L, 2L, 16L, 1L },
                    { 4L, 2L, 14L, 2L }
                });

            migrationBuilder.InsertData(
                table: "messages",
                columns: new[] { "id", "chat_id", "content", "date_create", "deleted_by_all", "deleted_by_user_id", "forwarded_message_id", "message_type_id", "source", "user_id" },
                values: new object[,]
                {
                    { 19L, 2L, "Как дела?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 2, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 18L, 1L, null, 2L },
                    { 62L, 1L, "Привет! Всё отлично, спасибо! Как у тебя?", new DateTimeOffset(new DateTime(2022, 12, 21, 10, 16, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 61L, 1L, null, 1L },
                    { 90L, 1L, "О, это звучит интересно! Когда и где проходит выставка?", new DateTimeOffset(new DateTime(2022, 12, 22, 9, 31, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 89L, 1L, null, 1L },
                    { 20L, 2L, "Отлично!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 3, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 19L, 1L, null, 1L },
                    { 63L, 1L, "У меня тоже всё хорошо. Планируешь что-нибудь интересное на выходные?", new DateTimeOffset(new DateTime(2022, 12, 21, 10, 17, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 62L, 1L, null, 2L },
                    { 91L, 1L, "Выставка проходит в местной галерее с 25 декабря по 10 января. Можем сходить в выходные.", new DateTimeOffset(new DateTime(2022, 12, 22, 9, 32, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 90L, 1L, null, 2L },
                    { 21L, 2L, "Что делаешь?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 4, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 20L, 1L, null, 2L },
                    { 64L, 1L, "Да, у меня есть небольшие планы. Хочу сходить в парк и поиграть в футбол.", new DateTimeOffset(new DateTime(2022, 12, 21, 10, 18, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 63L, 1L, null, 1L },
                    { 65L, 1L, "Отличная идея! Погода должна быть хорошей. Удачи в игре!", new DateTimeOffset(new DateTime(2022, 12, 21, 10, 19, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 63L, 1L, null, 2L },
                    { 92L, 1L, "Отлично! Я с удовольствием присоединюсь. Выставки искусства всегда вдохновляют меня.", new DateTimeOffset(new DateTime(2022, 12, 22, 9, 33, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 91L, 1L, null, 1L },
                    { 22L, 2L, "Планирую поход в кино.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 5, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 21L, 1L, null, 1L },
                    { 66L, 1L, "Спасибо! Буду стараться показать хороший результат.", new DateTimeOffset(new DateTime(2022, 12, 21, 10, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 65L, 1L, null, 1L },
                    { 93L, 1L, "Прекрасно! Уверен, мы сможем насладиться множеством талантливых работ.", new DateTimeOffset(new DateTime(2022, 12, 22, 9, 34, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 92L, 1L, null, 2L },
                    { 23L, 2L, "Какой фильм собираешься смотреть?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 6, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 22L, 1L, null, 2L },
                    { 67L, 1L, "У тебя есть какие-нибудь планы на вечер?", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 66L, 1L, null, 2L },
                    { 94L, 1L, "Точно! Искусство способно пробудить в нас самые глубокие эмоции и мысли.", new DateTimeOffset(new DateTime(2022, 12, 22, 9, 35, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 93L, 1L, null, 1L },
                    { 24L, 2L, "Думаю посмотреть новый боевик.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 7, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 23L, 1L, null, 1L },
                    { 68L, 1L, "На вечер у меня нет особых планов. Возможно, просто отдохну дома и посмотрю фильм.", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 31, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 67L, 1L, null, 1L },
                    { 95L, 1L, "Не терпится увидеть выставку! Будет замечательное время вместе.", new DateTimeOffset(new DateTime(2022, 12, 22, 9, 36, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 94L, 1L, null, 2L },
                    { 25L, 2L, "Звучит интересно. Какие еще планы на выходные?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 8, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 24L, 1L, null, 2L },
                    { 69L, 1L, "Это звучит как хороший вариант. Можешь порекомендовать какой-нибудь фильм?", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 32, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 68L, 1L, null, 2L },
                    { 96L, 1L, "Согласен, это будет отличная возможность провести время вместе и обсудить впечатления.", new DateTimeOffset(new DateTime(2022, 12, 22, 9, 37, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 95L, 1L, null, 1L },
                    { 26L, 2L, "Надеюсь провести время с семьей и отдохнуть.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 9, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 25L, 1L, null, 1L },
                    { 70L, 1L, "Конечно! Я недавно посмотрел фильм 'Интерстеллар'. Он очень увлекательный и захватывающий.", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 33, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 69L, 1L, null, 1L },
                    { 97L, 1L, "Точно! Не могу дождаться выходных. Давай встретимся возле галереи в субботу в 12:00?", new DateTimeOffset(new DateTime(2022, 12, 22, 9, 38, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 96L, 1L, null, 2L },
                    { 27L, 2L, "Отличные планы! Удачи вам!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 26L, 1L, null, 2L },
                    { 71L, 1L, "Спасибо за рекомендацию! Обязательно посмотрю.", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 34, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 70L, 1L, null, 2L },
                    { 98L, 1L, "Отлично, договорились! Увидимся в субботу возле галереи в 12:00. Буду ждать с нетерпением!", new DateTimeOffset(new DateTime(2022, 12, 22, 9, 39, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 97L, 1L, null, 1L },
                    { 28L, 2L, "Спасибо! Буду стараться!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 11, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 27L, 1L, null, 1L },
                    { 72L, 1L, "Пожалуйста! Уверен, тебе понравится. Наслаждайся просмотром!", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 35, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 71L, 1L, null, 1L },
                    { 99L, 1L, "Супер! Уже не могу дождаться нашей встречи. До субботы!", new DateTimeOffset(new DateTime(2022, 12, 22, 9, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 98L, 1L, null, 2L },
                    { 29L, 2L, "Как прошел поход в кино?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 28L, 1L, null, 2L },
                    { 73L, 1L, "Спасибо! Ты всегда помогаешь выбрать хорошее развлечение.", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 36, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 72L, 1L, null, 2L },
                    { 100L, 1L, "До встречи! Приятного дня и до скорого!", new DateTimeOffset(new DateTime(2022, 12, 22, 9, 41, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 99L, 1L, null, 1L },
                    { 30L, 2L, "Было здорово! Фильм понравился.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 13, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 29L, 1L, null, 1L },
                    { 74L, 1L, "Рад, что моя помощь пригождается! Если у тебя есть еще вопросы или нужна помощь, обращайся.", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 37, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 73L, 1L, null, 1L },
                    { 31L, 2L, "Рад, что вам понравилось!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 14, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 30L, 1L, null, 2L },
                    { 75L, 1L, "Безусловно! Я всегда знаю, кому обратиться, когда нужна помощь или совет.", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 38, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 74L, 1L, null, 2L },
                    { 32L, 2L, "Спасибо за рекомендацию фильма.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 15, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 31L, 1L, null, 1L },
                    { 76L, 1L, "Спасибо за доверие! Я всегда готов поддержать друзей.", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 39, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 75L, 1L, null, 1L },
                    { 33L, 2L, "Привет! Как дела?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 8, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 32L, 1L, null, 2L },
                    { 77L, 1L, "Ты лучший друг! Ценю нашу дружбу.", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 76L, 1L, null, 2L },
                    { 34L, 2L, "Привет! У меня все хорошо. Как твои дела?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 9, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 33L, 1L, null, 1L },
                    { 78L, 1L, "Спасибо! Я также очень ценю нашу дружбу. Мы всегда поддержим друг друга.", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 41, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 77L, 1L, null, 1L },
                    { 35L, 2L, "У меня тоже все отлично. Планируешь что-нибудь интересное на выходные?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 34L, 1L, null, 2L },
                    { 79L, 1L, "Так точно! Дружба навсегда!", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 42, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 78L, 1L, null, 2L },
                    { 36L, 2L, "Да, собираюсь с друзьями в парк на пикник. Будет весело!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 11, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 35L, 1L, null, 1L },
                    { 80L, 1L, "Ни шагу назад! Вместе мы сила!", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 43, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 79L, 1L, null, 1L },
                    { 37L, 2L, "Отличная идея! Хорошо проведите время.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 36L, 1L, null, 2L },
                    { 81L, 1L, "Точно! Ничто не сможет нас остановить!", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 44, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 80L, 1L, null, 2L },
                    { 38L, 2L, "Спасибо! Будем стараться!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 13, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 37L, 1L, null, 1L },
                    { 82L, 1L, "Вперед, к новым победам и успехам!", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 81L, 1L, null, 1L },
                    { 39L, 2L, "Какой фильм смотрели на прошлой неделе?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 14, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 38L, 1L, null, 2L },
                    { 83L, 1L, "Согласен! Мы не останавливаемся на достигнутом, а стремимся к большему!", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 82L, 1L, null, 1L },
                    { 40L, 2L, "Мы посмотрели комедию 'Август. Восьмого'. Очень смешной фильм!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 15, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 39L, 1L, null, 1L },
                    { 84L, 1L, "Точно! Вместе мы неуязвимы!", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 46, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 83L, 1L, null, 2L },
                    { 41L, 2L, "Я слышал о нем! Действительно, забавная комедия.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 16, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 40L, 1L, null, 2L },
                    { 85L, 1L, "Да, мы команда, сплоченная и непобедимая!", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 47, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 84L, 1L, null, 1L },
                    { 42L, 2L, "Очень рекомендую посмотреть, если у тебя будет свободное время.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 17, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 41L, 1L, null, 1L },
                    { 86L, 1L, "Всегда готов поддержать тебя в любых начинаниях!", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 48, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 85L, 1L, null, 2L },
                    { 43L, 2L, "Спасибо за рекомендацию! Обязательно добавлю в свой список фильмов.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 18, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 42L, 1L, null, 2L },
                    { 87L, 1L, "Спасибо, друг! Твоя поддержка очень важна для меня.", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 49, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 86L, 1L, null, 1L },
                    { 44L, 2L, "Нет проблем! Уверен, тебе понравится.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 19, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 43L, 1L, null, 1L },
                    { 88L, 1L, "Всегда рад помочь! Мы вместе сила!", new DateTimeOffset(new DateTime(2022, 12, 21, 18, 50, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 87L, 1L, null, 2L },
                    { 45L, 2L, "Что-нибудь интересное произошло у тебя на этой неделе?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 20, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 44L, 1L, null, 2L },
                    { 46L, 2L, "Ничего особенного, но я получил повышение на работе! Я очень рад!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 21, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 45L, 1L, null, 1L },
                    { 47L, 2L, "Поздравляю! Ты действительно заслужил это повышение.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 22, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 46L, 1L, null, 2L },
                    { 48L, 2L, "Спасибо! Я очень старался и рад, что моя работа оценена.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 23, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 47L, 1L, null, 1L },
                    { 49L, 2L, "Теперь у тебя будет еще больше возможностей и достижений!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 24, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 48L, 1L, null, 2L },
                    { 50L, 2L, "Да, я очень мотивирован и готов взяться за новые проекты.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 25, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 49L, 1L, null, 1L },
                    { 51L, 2L, "Удачи тебе во всем! Ты сможешь добиться большого!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 26, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 50L, 1L, null, 2L },
                    { 52L, 2L, "Спасибо за поддержку! Я ценю это.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 27, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 51L, 1L, null, 1L },
                    { 53L, 2L, "Не за что! Всегда готов помочь и поддержать.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 28, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 52L, 1L, null, 2L },
                    { 54L, 2L, "Спасибо, друг! Ты лучший!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 29, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 53L, 1L, null, 1L },
                    { 55L, 2L, "И ты тоже, брат! Любые проблемы - обращайся!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 54L, 1L, null, 2L },
                    { 56L, 2L, "Конечно, буду иметь в виду! Спасибо за поддержку и дружбу!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 31, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 55L, 1L, null, 1L },
                    { 57L, 2L, "Взаимно, братан! Дружба навсегда!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 32, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 56L, 1L, null, 2L },
                    { 58L, 2L, "Да, навсегда! Будем держаться вместе!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 33, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 57L, 1L, null, 1L },
                    { 59L, 2L, "Так точно! Ни шагу назад!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 34, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 58L, 1L, null, 2L },
                    { 60L, 2L, "Вперед, к новым приключениям!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 47, 35, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 59L, 1L, null, 1L }
                });

            migrationBuilder.CreateIndex(
                name: "ix_chat_participants_chat_id",
                table: "chat_participants",
                column: "chat_id");

            migrationBuilder.CreateIndex(
                name: "ix_chat_participants_user_id",
                table: "chat_participants",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_chats_chat_type_id",
                table: "chats",
                column: "chat_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_statuses_chat_id",
                table: "message_statuses",
                column: "chat_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_statuses_last_message_read_id",
                table: "message_statuses",
                column: "last_message_read_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_statuses_user_id",
                table: "message_statuses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_chat_id",
                table: "messages",
                column: "chat_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_deleted_by_user_id",
                table: "messages",
                column: "deleted_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_forwarded_message_id",
                table: "messages",
                column: "forwarded_message_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_message_type_id",
                table: "messages",
                column: "message_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_user_id",
                table: "messages",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_user_id",
                table: "users",
                column: "user_id",
                unique: true);

            if (migrationBuilder.IsNpgsql())
            {
                foreach (var function in NpgFunctionsMigration.CreateFunctions)
                {
                    migrationBuilder.Sql(function);
                }
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat_participants");

            migrationBuilder.DropTable(
                name: "message_statuses");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.DropTable(
                name: "message_types");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "chat_types");
        }
    }
}
