CREATE OR REPLACE FUNCTION public.get_message_data_by_id(
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
$BODY$;