using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PowerMessenger.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Theme = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: true),
                    ChatTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_ChatTypes_ChatTypeId",
                        column: x => x.ChatTypeId,
                        principalTable: "ChatTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatParticipants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatParticipants_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatParticipants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    ForwardedMessageId = table.Column<long>(type: "bigint", nullable: true),
                    MessageTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_MessageTypes_MessageTypeId",
                        column: x => x.MessageTypeId,
                        principalTable: "MessageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Messages_ForwardedMessageId",
                        column: x => x.ForwardedMessageId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    MessageId = table.Column<long>(type: "bigint", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageStatuses_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageStatuses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChatTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1L, "Personal" },
                    { 2L, "Group" }
                });

            migrationBuilder.InsertData(
                table: "MessageTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1L, "Text" },
                    { 2L, "Image" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "DateCreated", "DateOfBirth", "Email", "Theme", "UserId", "UserName" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2023, 4, 19, 20, 58, 53, 755, DateTimeKind.Local).AddTicks(3332), null, "power@mail.ru", null, 1L, "PowerBlaze" },
                    { 2L, null, new DateTime(2023, 4, 19, 20, 58, 53, 755, DateTimeKind.Local).AddTicks(3348), null, "tower@mail.ru", null, 2L, "TowerBlaze" }
                });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "ChatTypeId", "DateCreate", "Description", "Name", "Photo" },
                values: new object[,]
                {
                    { 1L, 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Чат для .NET разработчиков и C# программистов.", "Group1", "ChatsImage/efe4e2f6-d7b2-49f4-80bf-a2b5e8fa7178.jpg" },
                    { 2L, 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Стараемся не флудить. Пишем по делу. Правила: https://t.me/professorweb/430450 Для флуда @svoboda_obsh", "DOT.NET Talking", "ChatsImage/5009efc6-6891-44b6-8d20-68ec9a9199de.jpg" }
                });

            migrationBuilder.InsertData(
                table: "ChatParticipants",
                columns: new[] { "Id", "ChatId", "Role", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L, null, 1L },
                    { 2L, 1L, null, 2L },
                    { 3L, 2L, null, 1L },
                    { 4L, 2L, null, 2L }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "ChatId", "Content", "DateCreate", "ForwardedMessageId", "MessageTypeId", "Source", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L, "Привет", new DateTime(2022, 1, 20, 23, 30, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 1L },
                    { 2L, 1L, "Дарова", new DateTime(2022, 1, 20, 23, 31, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 2L },
                    { 3L, 1L, null, new DateTime(2022, 1, 20, 23, 32, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 1L },
                    { 4L, 1L, "Нормально", new DateTime(2022, 1, 20, 23, 33, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 2L },
                    { 5L, 1L, "HelloWorld", new DateTime(2022, 1, 20, 23, 34, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 1L },
                    { 6L, 1L, "Если бы не характер, то природная смекалка, хитрость и отвага, доходящая до авантюризма,\r\nсделали бы его в наше время очень богатым", new DateTime(2022, 1, 20, 23, 35, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 2L },
                    { 7L, 1L, "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.", new DateTime(2022, 1, 20, 23, 36, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 1L },
                    { 8L, 1L, "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.", new DateTime(2022, 1, 20, 23, 37, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 2L },
                    { 9L, 1L, "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.", new DateTime(2022, 1, 20, 23, 38, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 1L },
                    { 10L, 1L, "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.", new DateTime(2022, 1, 20, 23, 39, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 2L },
                    { 11L, 1L, "Cupidatat exercitation et culpa nisi consectetur laborum eu voluptate enim deserunt nostrud.\r\nVoluptate id nulla exercitation enim do qui elit proident ullamco qui pariatur cillum. \r\nPariatur ea eu duis laborum occaecat deserunt.", new DateTime(2022, 1, 20, 23, 40, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 1L },
                    { 12L, 1L, "Eiusmod dolore est id ipsum mollit ex.", new DateTime(2022, 1, 20, 23, 41, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 2L },
                    { 13L, 1L, "Dolore enim ea est incididunt do", new DateTime(2022, 1, 20, 23, 42, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 1L },
                    { 14L, 2L, "Eiusmod dolore est id ipsum mollit ex.", new DateTime(2022, 1, 20, 23, 39, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 1L },
                    { 15L, 2L, "Cupidatat exercitation et culpa nisi consectetur laborum eu voluptate enim deserunt nostrud.\r\nVoluptate id nulla exercitation enim do qui elit proident ullamco qui pariatur cillum. \r\nPariatur ea eu duis laborum occaecat deserunt.", new DateTime(2022, 1, 20, 23, 40, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 2L },
                    { 16L, 2L, "Eiusmod dolore est id ipsum mollit ex.", new DateTime(2022, 1, 20, 23, 41, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 2L },
                    { 17L, 2L, "Dolore enim ea est incididunt do", new DateTime(2022, 1, 20, 23, 42, 0, 0, DateTimeKind.Unspecified), null, 1L, null, 2L }
                });

            migrationBuilder.InsertData(
                table: "MessageStatuses",
                columns: new[] { "Id", "IsRead", "MessageId", "UserId" },
                values: new object[,]
                {
                    { 1L, false, 1L, 1L },
                    { 2L, false, 1L, 2L },
                    { 3L, false, 2L, 1L },
                    { 4L, false, 2L, 2L },
                    { 5L, false, 3L, 1L },
                    { 6L, false, 3L, 2L },
                    { 7L, false, 4L, 1L },
                    { 8L, false, 4L, 2L },
                    { 9L, false, 5L, 1L },
                    { 10L, false, 5L, 2L },
                    { 11L, false, 6L, 2L },
                    { 12L, false, 6L, 1L },
                    { 13L, false, 7L, 1L },
                    { 14L, false, 7L, 2L },
                    { 15L, false, 8L, 1L },
                    { 16L, false, 8L, 2L },
                    { 17L, false, 9L, 1L },
                    { 18L, false, 9L, 2L },
                    { 19L, false, 10L, 1L },
                    { 20L, false, 10L, 2L },
                    { 21L, false, 11L, 1L },
                    { 22L, false, 11L, 2L },
                    { 23L, false, 12L, 1L },
                    { 24L, false, 12L, 2L },
                    { 25L, false, 13L, 1L },
                    { 26L, false, 13L, 2L },
                    { 27L, false, 14L, 1L },
                    { 28L, false, 14L, 2L },
                    { 29L, false, 15L, 1L },
                    { 30L, false, 15L, 2L },
                    { 31L, false, 16L, 1L },
                    { 32L, false, 16L, 2L },
                    { 33L, false, 17L, 1L },
                    { 34L, false, 17L, 2L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipants_ChatId",
                table: "ChatParticipants",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipants_UserId",
                table: "ChatParticipants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ChatTypeId",
                table: "Chats",
                column: "ChatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ForwardedMessageId",
                table: "Messages",
                column: "ForwardedMessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageTypeId",
                table: "Messages",
                column: "MessageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageStatuses_MessageId",
                table: "MessageStatuses",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageStatuses_UserId",
                table: "MessageStatuses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatParticipants");

            migrationBuilder.DropTable(
                name: "MessageStatuses");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "MessageTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ChatTypes");
        }
    }
}
