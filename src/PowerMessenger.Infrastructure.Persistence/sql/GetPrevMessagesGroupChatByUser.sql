CREATE OR REPLACE FUNCTION public.get_prev_messages_group_chat_by_user(
    p_chat_id bigint,
    p_user_id bigint,
    p_message_id bigint,
    p_count integer
)
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
    SELECT public.messages.date_create INTO messageDate 
    FROM public.messages
    WHERE messages.id = p_message_id LIMIT 1;

    SELECT public.messages.date_create INTO last_message_read_date 
    FROM public.message_statuses
    INNER JOIN public.messages ON message_statuses.last_message_read_id = messages.id
    WHERE message_statuses.user_id = p_user_id AND message_statuses.chat_id = p_chat_id LIMIT 1;

    RETURN QUERY
        WITH unSortedMessages AS 
            (SELECT 
                messages.id,
                messages.content,
                messages.source,
                messages.date_create,
                message_types.type,
                CASE WHEN messages.user_id = p_user_id THEN true ELSE false END as isOwner,
                CASE WHEN (messages.date_create <= last_message_read_date) THEN true ELSE false END as isRead,
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
            ORDER BY messages.date_create DESC LIMIT p_count) 
        SELECT * FROM unSortedMessages ORDER BY unSortedMessages.date_create;
END;
$BODY$;