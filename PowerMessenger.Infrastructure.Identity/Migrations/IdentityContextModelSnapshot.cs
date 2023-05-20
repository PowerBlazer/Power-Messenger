﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PowerMessenger.Infrastructure.Identity.Contexts;

#nullable disable

namespace PowerMessenger.Infrastructure.Identity.Migrations
{
    [DbContext(typeof(IdentityContext))]
    partial class IdentityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                });

            modelBuilder.Entity("PowerMessenger.Infrastructure.Identity.Entities.IdentityUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
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
                            Id = 2L,
                            DateCreated = new DateTime(2023, 5, 14, 19, 17, 30, 58, DateTimeKind.Local).AddTicks(2670),
                            Email = "test@yandex.ru",
                            EmailConfirmed = true,
                            PasswordHash = "",
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
