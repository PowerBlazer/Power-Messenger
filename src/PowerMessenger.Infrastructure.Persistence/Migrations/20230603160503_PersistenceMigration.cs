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
                    date_of_birth = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    theme = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                        principalColumn: "id",
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
                    date_create = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_messages_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
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
                        principalColumn: "id",
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
                    { 1L, 2L, new DateTime(2023, 6, 3, 19, 5, 3, 599, DateTimeKind.Local).AddTicks(5904), "Чат для .NET разработчиков и C# программистов.", "Group1", "ChatsImage/efe4e2f6-d7b2-49f4-80bf-a2b5e8fa7178.jpg" },
                    { 2L, 2L, new DateTime(2023, 6, 3, 19, 5, 3, 599, DateTimeKind.Local).AddTicks(5917), "Стараемся не флудить. Пишем по делу. Правила: https://t.me/professorweb/430450 Для флуда @svoboda_obsh", "DOT.NET Talking", "ChatsImage/5009efc6-6891-44b6-8d20-68ec9a9199de.jpg" }
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
                    { 1L, 1L, "Привет", new DateTime(2022, 1, 20, 23, 30, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 1L },
                    { 2L, 1L, "Дарова", new DateTime(2022, 1, 20, 23, 31, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 2L },
                    { 3L, 1L, null, new DateTime(2022, 1, 20, 23, 32, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 1L },
                    { 4L, 1L, "Нормально", new DateTime(2022, 1, 20, 23, 33, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 2L },
                    { 5L, 1L, "HelloWorld", new DateTime(2022, 1, 20, 23, 34, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 1L },
                    { 6L, 1L, "Если бы не характер, то природная смекалка, хитрость и отвага, доходящая до авантюризма,\r\nсделали бы его в наше время очень богатым", new DateTime(2022, 1, 20, 23, 35, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 2L },
                    { 7L, 1L, "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.", new DateTime(2022, 1, 20, 23, 36, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 1L },
                    { 8L, 1L, "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.", new DateTime(2022, 1, 20, 23, 37, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 2L },
                    { 9L, 1L, "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.", new DateTime(2022, 1, 20, 23, 38, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 1L },
                    { 10L, 1L, "Eiusmod id pariatur reprehenderit minim ea est laboris. \r\nDo consectetur officia consectetur consequat deserunt. In labore excepteur non ipsum esse commodo officia. \r\nAliquip sit aliquip laborum dolor nisi mollit consequat exercitation sit laboris in reprehenderit exercitation.", new DateTime(2022, 1, 20, 23, 39, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 2L },
                    { 11L, 1L, "Cupidatat exercitation et culpa nisi consectetur laborum eu voluptate enim deserunt nostrud.\r\nVoluptate id nulla exercitation enim do qui elit proident ullamco qui pariatur cillum. \r\nPariatur ea eu duis laborum occaecat deserunt.", new DateTime(2022, 1, 20, 23, 40, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 1L },
                    { 12L, 1L, "Eiusmod dolore est id ipsum mollit ex.", new DateTime(2022, 1, 20, 23, 41, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 2L },
                    { 13L, 1L, "Dolore enim ea est incididunt do", new DateTime(2022, 1, 20, 23, 42, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 1L },
                    { 14L, 2L, "Eiusmod dolore est id ipsum mollit ex.", new DateTime(2022, 1, 20, 23, 39, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 1L },
                    { 15L, 2L, "Cupidatat exercitation et culpa nisi consectetur laborum eu voluptate enim deserunt nostrud.\r\nVoluptate id nulla exercitation enim do qui elit proident ullamco qui pariatur cillum. \r\nPariatur ea eu duis laborum occaecat deserunt.", new DateTime(2022, 1, 20, 23, 40, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 2L },
                    { 16L, 2L, "Eiusmod dolore est id ipsum mollit ex.", new DateTime(2022, 1, 20, 23, 41, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 2L },
                    { 17L, 2L, "Dolore enim ea est incididunt do", new DateTime(2022, 1, 20, 23, 42, 0, 0, DateTimeKind.Unspecified), false, null, null, 1L, null, 2L }
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

            if (migrationBuilder.IsNpgsql())
            {
                #region GetChatsByUser

                migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION public.get_chats_by_user(IN p_user_id bigint)
                    RETURNS TABLE(
	                    id bigint, 
	                    Name character varying, 
	                    datecreate timestamp without time zone, 
	                    photo text, 
	                    description text,
	                    type character varying, 
	                    countparticipants integer,
	                    countunreadmessages integer, 
	                    countmessages integer, 
	                    lastmessagecontent text, 
	                    lastmessagetype character varying, 
	                    lastmessagedatecreate timestamp without time zone)
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
                            (SELECT COUNT(*)::integer FROM public.messages WHERE public.messages.date_create >= (SELECT public.messages.date_create FROM public.message_statuses 
			                    INNER JOIN public.messages ON public.messages.id = public.message_statuses.last_message_read_id 
			                    WHERE public.message_statuses.chat_id = chats.id 
			                    AND public.message_statuses.user_id = p_user_id LIMIT 1)
		                    AND public.messages.chat_id = chats.id),
		                     
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
                #region GetMessagesGroupChatByUser

                migrationBuilder.Sql(
                    @"CREATE OR REPLACE FUNCTION get_messages_group_chat_by_user(IN p_chat_id bigint,IN p_user_id bigint) 
RETURNS TABLE(
	id bigint,
	content text,
	source text,
	date_create timestamp without time zone,
	type character varying,
	is_owner boolean,
	is_read boolean,
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
	firstUnReadMessageDate timestamp without time zone;
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
			LIMIT 10
		), 
		read_messages AS (
			SELECT messages.id
			FROM messages
			WHERE messages.chat_id = p_chat_id
				AND messages.date_create <= firstUnReadMessageDate
			ORDER BY messages.date_create DESC
			LIMIT 10
		)
		SELECT messages.id,
			messages.content,
			messages.source,
			messages.date_create,
			message_types.type,
			CASE WHEN messages.user_id = p_user_id THEN true ELSE false END as isOwner,
			CASE WHEN messages.id IN (SELECT read_messages.id FROM read_messages) THEN true ELSE false END as isRead,
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
                migrationBuilder.Sql("DROP FUNCTION get_chats_by_user");
                migrationBuilder.Sql("DROP FUNCTION get_messages_group_chat_by_user");
            }
        }
    }
}
