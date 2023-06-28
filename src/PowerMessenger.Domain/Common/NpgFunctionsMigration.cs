namespace PowerMessenger.Domain.Common;

public static class NpgFunctionsMigration
{
	public static IEnumerable<string> CreateFunctions => new List<string>
	{
		#region get_unread_message_count

		@"CREATE OR REPLACE FUNCTION get_unread_message_count(p_user_id bigint, p_chat_id bigint)
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
               ) AND messages.chat_id = p_chat_id AND messages.user_id != p_user_id
               );
               END;
               $$ LANGUAGE plpgsql;",

		#endregion
		#region get_chats_by_user

		@"CREATE OR REPLACE FUNCTION public.get_chats_by_user(IN p_user_id bigint)
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
                          	lastmessageid bigint,
                          	lastmessagecontent text,
                              lastmessageusername character varying,
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
                          		 AND messages.chat_id = chats.id AND messages.user_id != p_user_id),
                          		 
                                  (SELECT count(*)::integer FROM public.messages WHERE messages.chat_id = chats.id),
                          		(SELECT messages.id FROM public.messages WHERE messages.chat_id = chats.id 
                                          ORDER BY messages.date_create DESC LIMIT 1),
                                  (SELECT messages.content FROM public.messages WHERE messages.chat_id = chats.id 
                                          ORDER BY messages.date_create DESC LIMIT 1),
                                  (SELECT users.user_name FROM public.messages 
                                      INNER JOIN users ON users.user_id = messages.user_id
                                      WHERE messages.chat_id = chats.id         
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
                          $BODY$;",
		#endregion
		#region get_messages_group_chat_by_user
		 @"CREATE OR REPLACE FUNCTION get_messages_group_chat_by_user(IN p_chat_id bigint,IN p_user_id bigint,IN p_next integer, IN p_prev integer) 
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
                          	forwarded_message_type character varying,
                          	forwarded_message_chat_id bigint
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
									ORDER BY messages.date_create
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
                          			CASE WHEN (messages.user_id = p_user_id OR messages.id IN (SELECT read_messages.id FROM read_messages)) THEN true ELSE false END as isRead,
                                      users.user_id,
                          			users.user_name,
                          			users.avatar,
                          			forwarded_messages.id,
                          			forwarded_messages.content,
                          			forwarded_users.user_name,
                          			forwarded_types.type,
                          			forwarded_messages.chat_id
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
                          $BODY$;", 
		 #endregion
		#region get_next_messages_group_chat_by_user
			@"CREATE OR REPLACE FUNCTION public.get_next_messages_group_chat_by_user(
                          	p_chat_id bigint,
                          	p_user_id bigint,
                          	p_message_id bigint,
                          	p_count integer)
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
                          forwarded_message_type character varying,
                          forwarded_message_chat_id bigint
                          ) 
                              LANGUAGE 'plpgsql'
                              COST 100
                              VOLATILE PARALLEL UNSAFE
                              ROWS 1000
                          
                          AS $BODY$
                          
                          DECLARE
                          	messageDate timestamp with time zone;
                          	last_message_read_date timestamp with time zone;
                          BEGIN
                          	SELECT public.messages.date_create INTO messageDate FROM public.messages 
                          	WHERE messages.id = p_message_id LIMIT 1;
                          
                          	SELECT public.messages.date_create INTO last_message_read_date FROM public.message_statuses
                          	INNER JOIN public.messages ON message_statuses.last_message_read_id = messages.id 
                          	WHERE message_statuses.user_id = p_user_id AND message_statuses.chat_id = p_chat_id LIMIT 1;
                          		
                          	RETURN QUERY 
                          		SELECT messages.id,
                          			messages.content,
                          			messages.source,
                          			messages.date_create,
                          			message_types.type,
                          			CASE WHEN messages.user_id = p_user_id THEN true ELSE false END as isOwner,
                          			CASE WHEN (messages.user_id = p_user_id OR messages.date_create <= last_message_read_date) THEN true ELSE false END as isRead,
                                      users.user_id,
                          			users.user_name,
                          			users.avatar,
                          			forwarded_messages.id,
                          			forwarded_messages.content,
                          			forwarded_users.user_name,
                          			forwarded_types.type,
                          			forwarded_messages.chat_id
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
                          $BODY$;",
			#endregion
		#region get_prev_messages_group_chat_by_user
               @"CREATE OR REPLACE FUNCTION public.get_prev_messages_group_chat_by_user(
                          	p_chat_id bigint,
                          	p_user_id bigint,
                          	p_message_id bigint,
                          	p_count integer)
                              RETURNS TABLE(id bigint, content text, source text, date_create timestamp with time zone, type character varying, is_owner boolean, is_read boolean, message_user_id bigint, message_user_name character varying, message_user_avatar text, forwarded_message_id bigint, forwarded_message_content text, forwarded_message_user_name character varying, forwarded_message_type character varying,forwarded_message_chat_id bigint) 
                              LANGUAGE 'plpgsql'
                              COST 100
                              VOLATILE PARALLEL UNSAFE
                              ROWS 1000
                          
                          AS $BODY$
                          
                          DECLARE
                          	messageDate timestamp with time zone;
                          	last_message_read_date timestamp with time zone;
                          BEGIN
                          	SELECT public.messages.date_create INTO messageDate FROM public.messages 
                          	WHERE messages.id = p_message_id LIMIT 1;
                          
                          	SELECT public.messages.date_create INTO last_message_read_date FROM public.message_statuses
                          	INNER JOIN public.messages ON message_statuses.last_message_read_id = messages.id 
                          	WHERE message_statuses.user_id = p_user_id AND message_statuses.chat_id = p_chat_id LIMIT 1;
                          		
                          	RETURN QUERY 
                          		WITH unSortedMessages AS (SELECT messages.id,
                          			messages.content,
                          			messages.source,
                          			messages.date_create,
                          			message_types.type,
                          			CASE WHEN messages.user_id = p_user_id THEN true ELSE false END as isOwner,
                          			CASE WHEN (messages.user_id = p_user_id OR messages.date_create <= last_message_read_date) THEN true ELSE false END as isRead,
                                      users.user_id,
                          			users.user_name,
                          			users.avatar,
                          			forwarded_messages.id,
                          			forwarded_messages.content,
                          			forwarded_users.user_name,
                          			forwarded_types.type,
                          			forwarded_messages.chat_id
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
                          $BODY$;",
        #endregion
        #region get_last_messages_group_chat_by_user
                @"CREATE OR REPLACE FUNCTION public.get_last_messages_group_chat_by_user(
	p_chat_id bigint,
	p_user_id bigint,
	p_count integer)
RETURNS TABLE(id bigint, content text, source text, date_create timestamp with time zone, type character varying, is_owner boolean, is_read boolean, message_user_id bigint, message_user_name character varying, message_user_avatar text, forwarded_message_id bigint, forwarded_message_content text, forwarded_message_user_name character varying, forwarded_message_type character varying, forwarded_message_chat_id bigint) 
LANGUAGE 'plpgsql'
COST 100
VOLATILE PARALLEL UNSAFE
ROWS 1000

  AS $BODY$
  DECLARE
  	last_message_read_date timestamp with time zone;
  BEGIN
  	SELECT public.messages.date_create INTO last_message_read_date FROM public.message_statuses
  	INNER JOIN public.messages ON message_statuses.last_message_read_id = messages.id 
  	WHERE message_statuses.user_id = p_user_id AND message_statuses.chat_id = p_chat_id LIMIT 1;
  		
  	RETURN QUERY 
  		WITH last_messages AS (
  			SELECT messages.id,
  				messages.content,
  				messages.source,
  				messages.date_create,
  				message_types.type,
  				CASE WHEN messages.user_id = p_user_id THEN true ELSE false END as isOwner,
  				CASE WHEN (messages.user_id = p_user_id OR messages.date_create <= last_message_read_date) THEN true ELSE false END as isRead,
  				users.user_id,
  				users.user_name,
  				users.avatar,
  				forwarded_messages.id,
  				forwarded_messages.content,
  				forwarded_users.user_name,
  				forwarded_types.type,
  				forwarded_messages.chat_id
  			FROM public.messages 
  			INNER JOIN users ON messages.user_id = users.user_id
  			INNER JOIN message_types ON messages.message_type_id = message_types.id
  			LEFT JOIN messages as forwarded_messages ON messages.forwarded_message_id = forwarded_messages.id
  			LEFT JOIN users as forwarded_users ON forwarded_messages.user_id = forwarded_users.user_id
  			LEFT JOIN message_types as forwarded_types ON forwarded_messages.message_type_id = forwarded_types.id
  			WHERE (messages.deleted_by_user_id != p_user_id OR messages.deleted_by_all != true)
  				AND messages.chat_id = p_chat_id
  			ORDER BY messages.date_create DESC LIMIT p_count) 
  		SELECT * FROM last_messages ORDER BY last_messages.date_create;
END;
$BODY$;",
        #endregion
        #region get_message_data_by_id
               @"CREATE OR REPLACE FUNCTION public.get_message_data_by_id(
p_message_id bigint)
    RETURNS TABLE(
        id bigint, 
        chat_id bigint,
        content text, 
        source text, 
        date_create timestamp with time zone, 
        type character varying, 
        message_user_id bigint, 
        message_user_name character varying, 
        message_user_avatar text, 
        forwarded_message_id bigint, 
        forwarded_message_content text, 
        forwarded_message_user_name character varying, 
        forwarded_message_type character varying, 
        forwarded_message_chat_id bigint) 
LANGUAGE 'plpgsql'
COST 100
VOLATILE PARALLEL UNSAFE
ROWS 1000
AS $BODY$
    BEGIN
        RETURN QUERY 
			SELECT messages.id,
            messages.chat_id,
            messages.content,
            messages.source,
            messages.date_create,
            message_types.type,
            users.user_id,
            users.user_name,
            users.avatar,
            forwarded_messages.id,
            forwarded_messages.content,
            forwarded_users.user_name,
            forwarded_types.type,
            forwarded_messages.chat_id
            FROM public.messages 
            INNER JOIN users ON messages.user_id = users.user_id
            INNER JOIN message_types ON messages.message_type_id = message_types.id
            LEFT JOIN messages as forwarded_messages ON messages.forwarded_message_id = forwarded_messages.id
            LEFT JOIN users as forwarded_users ON forwarded_messages.user_id = forwarded_users.user_id
            LEFT JOIN message_types as forwarded_types ON forwarded_messages.message_type_id = forwarded_types.id
            WHERE messages.id = p_message_id
            LIMIT 1;
END;
$BODY$;",
		#endregion
		#region get_messages_group_chat_by_message_id
		@"CREATE OR REPLACE FUNCTION public.get_messages_group_chat_by_message_id(
	p_chat_id bigint,
	p_user_id bigint,
	p_message_id bigint,
	p_next integer,
	p_prev integer)
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
		forwarded_message_type character varying, 
		forwarded_message_chat_id bigint) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
first_read_message_date timestamp with time zone;
message_position_date timestamp with time zone;
BEGIN
     SELECT public.messages.date_create INTO first_read_message_date FROM public.message_statuses
            INNER JOIN public.messages ON public.messages.id = message_statuses.last_message_read_id
     WHERE message_statuses.user_id = p_user_id AND message_statuses.chat_id = p_chat_id LIMIT 1;  
	 
	 SELECT messages.date_create INTO message_position_date FROM messages WHERE messages.id = p_message_id;
	 
     IF first_read_message_date IS NULL THEN
        SELECT messages.date_create INTO first_read_message_date FROM public.messages 
        WHERE public.messages.user_id = p_user_id 
        AND public.messages.chat_id = p_chat_id
        ORDER BY date_create DESC LIMIT 1;
     END IF;
	 
	 
RETURN QUERY 
       WITH next_messages AS (
            SELECT messages.id
            FROM messages
            WHERE messages.chat_id = p_chat_id
            AND messages.date_create > message_position_date 
			ORDER BY messages.date_create
			LIMIT p_next), 
       prev_messages AS (
            SELECT messages.id
            FROM messages
            WHERE messages.chat_id = p_chat_id
                  AND messages.date_create <= message_position_date
            ORDER BY messages.date_create DESC
            LIMIT p_prev)
      SELECT messages.id,
             messages.content,
             messages.source,
             messages.date_create,
             message_types.type,
             CASE WHEN messages.user_id = p_user_id THEN true ELSE false END as isOwner,
             CASE WHEN (messages.user_id = p_user_id OR messages.date_create <= first_read_message_date) THEN true ELSE false END as isRead,
             users.user_id,
             users.user_name,
             users.avatar,
             forwarded_messages.id,
             forwarded_messages.content,
             forwarded_users.user_name,
             forwarded_types.type,
             forwarded_messages.chat_id
       FROM public.messages 
            INNER JOIN users ON messages.user_id = users.user_id
            INNER JOIN message_types ON messages.message_type_id = message_types.id
            LEFT JOIN messages as forwarded_messages ON messages.forwarded_message_id = forwarded_messages.id
            LEFT JOIN users as forwarded_users ON forwarded_messages.user_id = forwarded_users.user_id
            LEFT JOIN message_types as forwarded_types ON forwarded_messages.message_type_id = forwarded_types.id
       WHERE (messages.deleted_by_user_id != p_user_id OR messages.deleted_by_all != true)
             AND messages.chat_id = p_chat_id
             AND (messages.id IN (SELECT next_messages.id FROM next_messages) 
             OR messages.id IN (SELECT prev_messages.id FROM prev_messages))
      ORDER BY messages.date_create;
END;                       
$BODY$;"
		

		#endregion
	};
        
   
    
    public static IEnumerable<string> DeleteFunctions => new List<string>
    {
        "DROP FUNCTION get_unread_message_count",
        "DROP FUNCTION get_chats_by_user",
        "DROP FUNCTION get_messages_group_chat_by_user",
        "DROP FUNCTION get_next_messages_group_chat_by_user",
        "DROP FUNCTION get_prev_messages_group_chat_by_user",
        "DROP FUNCTION get_last_messages_group_chat_by_user",
        "FROP FUNCTION get_message_data_by_id"
    };
}