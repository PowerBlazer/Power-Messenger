using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerMessenger.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PersistenceMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatParticipants_Chats_ChatId",
                table: "ChatParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatParticipants_Users_UserId",
                table: "ChatParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_ChatTypes_ChatTypeId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageTypes_MessageTypeId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_ForwardedMessageId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageStatuses_Messages_MessageId",
                table: "MessageStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageStatuses_Users_UserId",
                table: "MessageStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageTypes",
                table: "MessageTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageStatuses",
                table: "MessageStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatTypes",
                table: "ChatTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatParticipants",
                table: "ChatParticipants");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "messages");

            migrationBuilder.RenameTable(
                name: "Chats",
                newName: "chats");

            migrationBuilder.RenameTable(
                name: "MessageTypes",
                newName: "message_types");

            migrationBuilder.RenameTable(
                name: "MessageStatuses",
                newName: "message_statuses");

            migrationBuilder.RenameTable(
                name: "ChatTypes",
                newName: "chat_types");

            migrationBuilder.RenameTable(
                name: "ChatParticipants",
                newName: "chat_participants");

            migrationBuilder.RenameColumn(
                name: "Theme",
                table: "users",
                newName: "theme");

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "users",
                newName: "avatar");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "users",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "users",
                newName: "date_of_birth");

            migrationBuilder.RenameColumn(
                name: "Source",
                table: "messages",
                newName: "source");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "messages",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "messages",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "messages",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "MessageTypeId",
                table: "messages",
                newName: "message_type_id");

            migrationBuilder.RenameColumn(
                name: "ForwardedMessageId",
                table: "messages",
                newName: "forwarded_message_id");

            migrationBuilder.RenameColumn(
                name: "DateCreate",
                table: "messages",
                newName: "date_create");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "messages",
                newName: "chat_id");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_UserId",
                table: "messages",
                newName: "ix_messages_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_MessageTypeId",
                table: "messages",
                newName: "ix_messages_message_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ForwardedMessageId",
                table: "messages",
                newName: "ix_messages_forwarded_message_id");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ChatId",
                table: "messages",
                newName: "ix_messages_chat_id");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "chats",
                newName: "photo");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "chats",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "chats",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "chats",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DateCreate",
                table: "chats",
                newName: "date_create");

            migrationBuilder.RenameColumn(
                name: "ChatTypeId",
                table: "chats",
                newName: "chat_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_ChatTypeId",
                table: "chats",
                newName: "ix_chats_chat_type_id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "message_types",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "message_types",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "message_statuses",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "message_statuses",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "message_statuses",
                newName: "message_id");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "message_statuses",
                newName: "is_read");

            migrationBuilder.RenameIndex(
                name: "IX_MessageStatuses_UserId",
                table: "message_statuses",
                newName: "ix_message_statuses_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_MessageStatuses_MessageId",
                table: "message_statuses",
                newName: "ix_message_statuses_message_id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "chat_types",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "chat_types",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "chat_participants",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "chat_participants",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "chat_participants",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "chat_participants",
                newName: "chat_id");

            migrationBuilder.RenameIndex(
                name: "IX_ChatParticipants_UserId",
                table: "chat_participants",
                newName: "ix_chat_participants_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_ChatParticipants_ChatId",
                table: "chat_participants",
                newName: "ix_chat_participants_chat_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_messages",
                table: "messages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_chats",
                table: "chats",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_message_types",
                table: "message_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_message_statuses",
                table: "message_statuses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_chat_types",
                table: "chat_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_chat_participants",
                table: "chat_participants",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_chat_participants_chats_chat_id",
                table: "chat_participants",
                column: "chat_id",
                principalTable: "chats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_chat_participants_users_user_id",
                table: "chat_participants",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_chats_chat_types_chat_type_id",
                table: "chats",
                column: "chat_type_id",
                principalTable: "chat_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_message_statuses_messages_message_id",
                table: "message_statuses",
                column: "message_id",
                principalTable: "messages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_message_statuses_users_user_id",
                table: "message_statuses",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_messages_chats_chat_id",
                table: "messages",
                column: "chat_id",
                principalTable: "chats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_messages_message_types_message_type_id",
                table: "messages",
                column: "message_type_id",
                principalTable: "message_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_messages_messages_forward_message_id",
                table: "messages",
                column: "forwarded_message_id",
                principalTable: "messages",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_messages_users_user_id",
                table: "messages",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_chat_participants_chats_chat_id",
                table: "chat_participants");

            migrationBuilder.DropForeignKey(
                name: "fk_chat_participants_users_user_id",
                table: "chat_participants");

            migrationBuilder.DropForeignKey(
                name: "fk_chats_chat_types_chat_type_id",
                table: "chats");

            migrationBuilder.DropForeignKey(
                name: "fk_message_statuses_messages_message_id",
                table: "message_statuses");

            migrationBuilder.DropForeignKey(
                name: "fk_message_statuses_users_user_id",
                table: "message_statuses");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_chats_chat_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_message_types_message_type_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_messages_forward_message_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_users_user_id",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_messages",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "pk_chats",
                table: "chats");

            migrationBuilder.DropPrimaryKey(
                name: "pk_message_types",
                table: "message_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_message_statuses",
                table: "message_statuses");

            migrationBuilder.DropPrimaryKey(
                name: "pk_chat_types",
                table: "chat_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_chat_participants",
                table: "chat_participants");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "messages",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "chats",
                newName: "Chats");

            migrationBuilder.RenameTable(
                name: "message_types",
                newName: "MessageTypes");

            migrationBuilder.RenameTable(
                name: "message_statuses",
                newName: "MessageStatuses");

            migrationBuilder.RenameTable(
                name: "chat_types",
                newName: "ChatTypes");

            migrationBuilder.RenameTable(
                name: "chat_participants",
                newName: "ChatParticipants");

            migrationBuilder.RenameColumn(
                name: "theme",
                table: "Users",
                newName: "Theme");

            migrationBuilder.RenameColumn(
                name: "avatar",
                table: "Users",
                newName: "Avatar");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "date_of_birth",
                table: "Users",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "source",
                table: "Messages",
                newName: "Source");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Messages",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Messages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Messages",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "message_type_id",
                table: "Messages",
                newName: "MessageTypeId");

            migrationBuilder.RenameColumn(
                name: "forwarded_message_id",
                table: "Messages",
                newName: "ForwardedMessageId");

            migrationBuilder.RenameColumn(
                name: "date_create",
                table: "Messages",
                newName: "DateCreate");

            migrationBuilder.RenameColumn(
                name: "chat_id",
                table: "Messages",
                newName: "ChatId");

            migrationBuilder.RenameIndex(
                name: "ix_messages_user_id",
                table: "Messages",
                newName: "IX_Messages_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_messages_message_type_id",
                table: "Messages",
                newName: "IX_Messages_MessageTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_messages_forwarded_message_id",
                table: "Messages",
                newName: "IX_Messages_ForwardedMessageId");

            migrationBuilder.RenameIndex(
                name: "ix_messages_chat_id",
                table: "Messages",
                newName: "IX_Messages_ChatId");

            migrationBuilder.RenameColumn(
                name: "photo",
                table: "Chats",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Chats",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Chats",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Chats",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "date_create",
                table: "Chats",
                newName: "DateCreate");

            migrationBuilder.RenameColumn(
                name: "chat_type_id",
                table: "Chats",
                newName: "ChatTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_chats_chat_type_id",
                table: "Chats",
                newName: "IX_Chats_ChatTypeId");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "MessageTypes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MessageTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MessageStatuses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "MessageStatuses",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "message_id",
                table: "MessageStatuses",
                newName: "MessageId");

            migrationBuilder.RenameColumn(
                name: "is_read",
                table: "MessageStatuses",
                newName: "IsRead");

            migrationBuilder.RenameIndex(
                name: "ix_message_statuses_user_id",
                table: "MessageStatuses",
                newName: "IX_MessageStatuses_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_message_statuses_message_id",
                table: "MessageStatuses",
                newName: "IX_MessageStatuses_MessageId");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "ChatTypes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ChatTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "ChatParticipants",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ChatParticipants",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "ChatParticipants",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "chat_id",
                table: "ChatParticipants",
                newName: "ChatId");

            migrationBuilder.RenameIndex(
                name: "ix_chat_participants_user_id",
                table: "ChatParticipants",
                newName: "IX_ChatParticipants_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_chat_participants_chat_id",
                table: "ChatParticipants",
                newName: "IX_ChatParticipants_ChatId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageTypes",
                table: "MessageTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageStatuses",
                table: "MessageStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatTypes",
                table: "ChatTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatParticipants",
                table: "ChatParticipants",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DateCreated", "Email" },
                values: new object[] { new DateTime(2023, 4, 19, 20, 58, 53, 755, DateTimeKind.Local).AddTicks(3332), "power@mail.ru" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DateCreated", "Email" },
                values: new object[] { new DateTime(2023, 4, 19, 20, 58, 53, 755, DateTimeKind.Local).AddTicks(3348), "tower@mail.ru" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChatParticipants_Chats_ChatId",
                table: "ChatParticipants",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatParticipants_Users_UserId",
                table: "ChatParticipants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_ChatTypes_ChatTypeId",
                table: "Chats",
                column: "ChatTypeId",
                principalTable: "ChatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageTypes_MessageTypeId",
                table: "Messages",
                column: "MessageTypeId",
                principalTable: "MessageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_ForwardedMessageId",
                table: "Messages",
                column: "ForwardedMessageId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageStatuses_Messages_MessageId",
                table: "MessageStatuses",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageStatuses_Users_UserId",
                table: "MessageStatuses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
