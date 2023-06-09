﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PowerMessenger.Infrastructure.Identity.Contexts;

#nullable disable

namespace PowerMessenger.Infrastructure.Identity.Migrations
{
    [DbContext(typeof(IdentityContext))]
    [Migration("20230604165706_IdentityMigration")]
    partial class IdentityMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PowerMessenger.Infrastructure.Identity.Entities.IdentityToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expiration");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_identity_tokens");

                    b.HasIndex("Token")
                        .IsUnique()
                        .HasDatabaseName("ix_identity_tokens_token");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_identity_tokens_user_id");

                    b.ToTable("identity_tokens", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Expiration = new DateTime(2023, 6, 11, 19, 57, 6, 311, DateTimeKind.Local).AddTicks(8521),
                            Token = "121212121212121",
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            Expiration = new DateTime(2023, 6, 11, 19, 57, 6, 311, DateTimeKind.Local).AddTicks(8541),
                            Token = "1212121212121212",
                            UserId = 2L
                        });
                });

            modelBuilder.Entity("PowerMessenger.Infrastructure.Identity.Entities.IdentityUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("phone_number");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.HasKey("Id")
                        .HasName("pk_identity_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_identity_users_email");

                    b.ToTable("identity_users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            DateCreated = new DateTimeOffset(new DateTime(2023, 6, 4, 16, 57, 6, 311, DateTimeKind.Unspecified).AddTicks(5616), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "yak.ainur@yandex.ru",
                            EmailConfirmed = true,
                            PasswordHash = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = 2L,
                            DateCreated = new DateTimeOffset(new DateTime(2023, 6, 4, 16, 57, 6, 311, DateTimeKind.Unspecified).AddTicks(5620), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "power.blaze@mail.ru",
                            EmailConfirmed = true,
                            PasswordHash = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                            TwoFactorEnabled = false
                        });
                });

            modelBuilder.Entity("PowerMessenger.Infrastructure.Identity.Entities.IdentityToken", b =>
                {
                    b.HasOne("PowerMessenger.Infrastructure.Identity.Entities.IdentityUser", "User")
                        .WithOne("IdentityToken")
                        .HasForeignKey("PowerMessenger.Infrastructure.Identity.Entities.IdentityToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_identity_tokens_identity_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PowerMessenger.Infrastructure.Identity.Entities.IdentityUser", b =>
                {
                    b.Navigation("IdentityToken");
                });
#pragma warning restore 612, 618
        }
    }
}
