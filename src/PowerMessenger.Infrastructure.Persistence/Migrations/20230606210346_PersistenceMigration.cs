using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                        principalColumn: "id");
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
                    { 1L, 2L, new DateTimeOffset(new DateTime(2023, 6, 6, 21, 3, 45, 984, DateTimeKind.Unspecified).AddTicks(8556), new TimeSpan(0, 0, 0, 0, 0)), "Чат для .NET разработчиков и C# программистов.", "Group1", "ChatsImage/efe4e2f6-d7b2-49f4-80bf-a2b5e8fa7178.jpg" },
                    { 2L, 2L, new DateTimeOffset(new DateTime(2023, 6, 6, 21, 3, 45, 984, DateTimeKind.Unspecified).AddTicks(8561), new TimeSpan(0, 0, 0, 0, 0)), "Стараемся не флудить. Пишем по делу. Правила: https://t.me/professorweb/430450 Для флуда @svoboda_obsh", "DOT.NET Talking", "ChatsImage/5009efc6-6891-44b6-8d20-68ec9a9199de.jpg" }
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
                    { 18L, 2L, "Привет!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, null, 1L, null, 1L }
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
                    { 20L, 2L, "Отлично!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 3, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 19L, 1L, null, 1L },
                    { 21L, 2L, "Что делаешь?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 4, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 20L, 1L, null, 2L },
                    { 22L, 2L, "Планирую поход в кино.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 5, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 21L, 1L, null, 1L },
                    { 23L, 2L, "Какой фильм собираешься смотреть?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 6, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 22L, 1L, null, 2L },
                    { 24L, 2L, "Думаю посмотреть новый боевик.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 7, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 23L, 1L, null, 1L },
                    { 25L, 2L, "Звучит интересно. Какие еще планы на выходные?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 8, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 24L, 1L, null, 2L },
                    { 26L, 2L, "Надеюсь провести время с семьей и отдохнуть.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 9, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 25L, 1L, null, 1L },
                    { 27L, 2L, "Отличные планы! Удачи вам!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 26L, 1L, null, 2L },
                    { 28L, 2L, "Спасибо! Буду стараться!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 11, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 27L, 1L, null, 1L },
                    { 29L, 2L, "Как прошел поход в кино?", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 28L, 1L, null, 2L },
                    { 30L, 2L, "Было здорово! Фильм понравился.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 13, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 29L, 1L, null, 1L },
                    { 31L, 2L, "Рад, что вам понравилось!", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 14, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 30L, 1L, null, 2L },
                    { 32L, 2L, "Спасибо за рекомендацию фильма.", new DateTimeOffset(new DateTime(2022, 12, 20, 23, 46, 15, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, null, 31L, 1L, null, 1L }
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
                column: "forwarded_message_id",
                unique: true);

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
                #region GetUnreadMessagesCount
                migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION get_unread_message_count(p_user_id bigint, p_chat_id bigint)
                RETURNS integer AS $$
                BEGIN
                    RETURN (
                        SELECT COUNT(*)::integer
                        FROM messages
                        WHERE messages.date_create > (
                            SELECT status_messages.date_create
                            FROM message_statuses
                            INNER JOIN messages AS status_messages ON message_statuses.last_message_read_id = status_messages.id
                            WHERE message_statuses.user_id = p_user_id AND message_statuses.chat_id = p_chat_id
                        )
                        AND messages.chat_id = p_chat_id
                    );
                END;
                $$ LANGUAGE plpgsql;
");


                #endregion
                #region GetChatsByUser
                migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION public.get_chats_by_user(IN p_user_id bigint)
RETURNS TABLE(
	id bigint, 
	Name character varying, 
	datecreate timestamp with time zone, 
	photo text, 
	description text,
	type character varying, 
	countparticipants integer,
	countunreadmessages integer, 
	countmessages integer, 
	lastmessagecontent text, 
	lastmessagetype character varying, 
	lastmessagedatecreate timestamp with time zone)
LANGUAGE 'plpgsql'
VOLATILE
PARALLEL UNSAFE
COST 100 ROWS 1000                 
AS $BODY$
   BEGIN
   RETURN QUERY
   		SELECT 
        chats.id,
        chats.name,
        chats.date_create,
        chats.photo,
        chats.description,
        chat_types.type,
        (SELECT count(*)::integer FROM public.chat_participants WHERE chat_participants.chat_id = chats.id),
        (SELECT COUNT(*)::integer FROM messages WHERE messages.date_create > 
			(SELECT status_messages.date_create FROM message_statuses
				INNER JOIN messages as status_messages ON message_statuses.last_message_read_id = status_messages.id
			WHERE message_statuses.user_id = p_user_id AND message_statuses.chat_id = chats.id) 
		 AND messages.chat_id = chats.id),
		 
        (SELECT count(*)::integer FROM public.messages WHERE messages.chat_id = chats.id),
        (SELECT content FROM public.messages WHERE messages.chat_id = chats.id 
                ORDER BY messages.date_create DESC LIMIT 1),
        (SELECT message_types.type FROM public.messages
        	INNER JOIN public.message_types ON messages.message_type_id = message_types.id
        WHERE messages.chat_id = chats.id 
        ORDER BY messages.date_create DESC LIMIT 1),
		
        (SELECT date_create FROM public.messages WHERE messages.chat_id = chats.id ORDER BY messages.date_create DESC LIMIT 1)
		
        FROM public.chats
        	INNER JOIN public.chat_types ON chat_type_id = chat_types.id
            INNER JOIN public.chat_participants ON chats.id = chat_participants.chat_id AND chat_participants.user_id = p_user_id
        ORDER BY
        	chats.name,
            chats.date_create;
END
$BODY$;");


                #endregion
                #region GetMessageGroupChatByUser
                migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION get_messages_group_chat_by_user(IN p_chat_id bigint,IN p_user_id bigint,IN p_next integer, IN p_prev integer) 
RETURNS TABLE(
	id bigint,
	content text,
	source text,
	date_create timestamp with time zone,
	type character varying,
	is_owner boolean,
	is_read boolean,
    message_user_id bigint,
	message_user_name character varying,
	message_user_avatar text,
	forwarded_message_id bigint,
	forwarded_message_content text,
	forwarded_message_user_name character varying,
	forwarded_message_type character varying
) 
	LANGUAGE 'plpgsql'
	VOLATILE
	PARALLEL UNSAFE
	COST 100 ROWS 1000 
AS $BODY$
DECLARE
	firstUnReadMessageDate timestamp with time zone;
BEGIN
	SELECT public.messages.date_create INTO firstUnReadMessageDate FROM public.message_statuses
		INNER JOIN public.messages ON public.messages.id = message_statuses.last_message_read_id
	WHERE message_statuses.user_id = p_user_id AND message_statuses.chat_id = p_chat_id LIMIT 1;
			
			
	IF firstUnReadMessageDate IS NULL THEN
		SELECT messages.date_create INTO firstUnReadMessageDate FROM public.messages 
				WHERE public.messages.user_id = p_user_id 
					AND public.messages.chat_id = p_chat_id
				ORDER BY date_create DESC LIMIT 1;
	END IF;
	
	RETURN QUERY 
		WITH unread_messages AS (
			SELECT messages.id
			FROM messages
			WHERE messages.chat_id = p_chat_id
				AND messages.date_create > firstUnReadMessageDate
			LIMIT p_next
		), 
		read_messages AS (
			SELECT messages.id
			FROM messages
			WHERE messages.chat_id = p_chat_id
				AND messages.date_create <= firstUnReadMessageDate
			ORDER BY messages.date_create DESC
			LIMIT p_prev
		)
		SELECT messages.id,
			messages.content,
			messages.source,
			messages.date_create,
			message_types.type,
			CASE WHEN messages.user_id = p_user_id THEN true ELSE false END as isOwner,
			CASE WHEN messages.id IN (SELECT read_messages.id FROM read_messages) THEN true ELSE false END as isRead,
            users.user_id,
			users.user_name,
			users.avatar,
			forwarded_messages.id,
			forwarded_messages.content,
			forwarded_users.user_name,
			forwarded_types.type
		FROM public.messages 
		INNER JOIN users ON messages.user_id = users.user_id
		INNER JOIN message_types ON messages.message_type_id = message_types.id
		LEFT JOIN messages as forwarded_messages ON messages.forwarded_message_id = forwarded_messages.id
		LEFT JOIN users as forwarded_users ON forwarded_messages.user_id = forwarded_users.user_id
		LEFT JOIN message_types as forwarded_types ON forwarded_messages.message_type_id = forwarded_types.id
		WHERE (messages.deleted_by_user_id != p_user_id OR messages.deleted_by_all != true)
			AND messages.chat_id = p_chat_id
			AND (messages.id IN (SELECT unread_messages.id FROM unread_messages) 
				OR messages.id IN (SELECT read_messages.id FROM read_messages))
		ORDER BY messages.date_create;
END;
$BODY$;");


                #endregion
                #region GetNextMessagesGroupChatByUser

                migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION public.get_next_messages_group_chat_by_user(
	p_chat_id bigint,
	p_user_id bigint,
	p_message_id bigint,
	p_count integer)
    RETURNS TABLE(id bigint, content text, source text, date_create timestamp with time zone, type character varying, is_owner boolean, is_read boolean, message_user_id bigint, message_user_name character varying, message_user_avatar text, forwarded_message_id bigint, forwarded_message_content text, forwarded_message_user_name character varying, forwarded_message_type character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

DECLARE
	messageDate timestamp with time zone;
BEGIN
	SELECT public.messages.date_create INTO messageDate FROM public.messages 
	WHERE messages.id = p_message_id LIMIT 1;
		
	RETURN QUERY 
		SELECT messages.id,
			messages.content,
			messages.source,
			messages.date_create,
			message_types.type,
			CASE WHEN messages.user_id = p_user_id THEN true ELSE false END as isOwner,
			(SELECT true) as isRead,
            users.user_id,
			users.user_name,
			users.avatar,
			forwarded_messages.id,
			forwarded_messages.content,
			forwarded_users.user_name,
			forwarded_types.type
		FROM public.messages 
		INNER JOIN users ON messages.user_id = users.user_id
		INNER JOIN message_types ON messages.message_type_id = message_types.id
		LEFT JOIN messages as forwarded_messages ON messages.forwarded_message_id = forwarded_messages.id
		LEFT JOIN users as forwarded_users ON forwarded_messages.user_id = forwarded_users.user_id
		LEFT JOIN message_types as forwarded_types ON forwarded_messages.message_type_id = forwarded_types.id
		WHERE (messages.deleted_by_user_id != p_user_id OR messages.deleted_by_all != true)
			AND messages.chat_id = p_chat_id
			AND messages.date_create > messageDate
		ORDER BY messages.date_create LIMIT p_count;
END;
$BODY$;");


                #endregion
                #region GetPrevMessagesGroupChatByUser

                migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION public.get_prev_messages_group_chat_by_user(
	p_chat_id bigint,
	p_user_id bigint,
	p_message_id bigint,
	p_count integer)
    RETURNS TABLE(id bigint, content text, source text, date_create timestamp with time zone, type character varying, is_owner boolean, is_read boolean, message_user_id bigint, message_user_name character varying, message_user_avatar text, forwarded_message_id bigint, forwarded_message_content text, forwarded_message_user_name character varying, forwarded_message_type character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$

DECLARE
	messageDate timestamp with time zone;
BEGIN
	SELECT public.messages.date_create INTO messageDate FROM public.messages 
	WHERE messages.id = p_message_id LIMIT 1;
		
	RETURN QUERY 
		WITH unSortedMessages AS (SELECT messages.id,
			messages.content,
			messages.source,
			messages.date_create,
			message_types.type,
			CASE WHEN messages.user_id = p_user_id THEN true ELSE false END as isOwner,
			(SELECT true) as isRead,
            users.user_id,
			users.user_name,
			users.avatar,
			forwarded_messages.id,
			forwarded_messages.content,
			forwarded_users.user_name,
			forwarded_types.type
		FROM public.messages 
		INNER JOIN users ON messages.user_id = users.user_id
		INNER JOIN message_types ON messages.message_type_id = message_types.id
		LEFT JOIN messages as forwarded_messages ON messages.forwarded_message_id = forwarded_messages.id
		LEFT JOIN users as forwarded_users ON forwarded_messages.user_id = forwarded_users.user_id
		LEFT JOIN message_types as forwarded_types ON forwarded_messages.message_type_id = forwarded_types.id
		WHERE (messages.deleted_by_user_id != p_user_id OR messages.deleted_by_all != true)
			AND messages.chat_id = p_chat_id
			AND messages.date_create < messageDate
		ORDER BY messages.date_create DESC LIMIT p_count) SELECT * FROM unSortedMessages ORDER BY unSortedMessages.date_create;
END;
$BODY$;");


                #endregion
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

            if (migrationBuilder.IsNpgsql())
            {
                migrationBuilder.Sql("DROP FUNCTION get_unread_message_count");
                migrationBuilder.Sql("DROP FUNCTION get_chats_by_user");
                migrationBuilder.Sql("DROP FUNCTION get_messages_group_chat_by_user");
                migrationBuilder.Sql("DROP FUNCTION get_next_messages_group_chat_by_user");
                migrationBuilder.Sql("DROP FUNCTION get_prev_messages_group_chat_by_user");
            }
        }
    }
}
