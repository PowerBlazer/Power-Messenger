using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PowerMessenger.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class IdentityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "DateCreated", "Email", "EmailConfirmed", "PasswordHash", "PhoneNumber", "TwoFactorEnabled" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 4, 27, 23, 15, 22, 241, DateTimeKind.Local).AddTicks(9548), "yak.ainur@yandex.ru", true, "", null, false },
                    { 2L, new DateTime(2023, 4, 27, 23, 15, 22, 241, DateTimeKind.Local).AddTicks(9566), "test@yandex.ru", true, "", null, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_Email",
                table: "IdentityUser",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityUser");
        }
    }
}
