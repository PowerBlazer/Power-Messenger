CREATE OR REPLACE FUNCTION get_messages_group_chat_by_user(IN p_chat_id bigint,IN p_user_id bigint) 
RETURNS TABLE(
	content text,
) 
	LANGUAGE 'plpgsql'
	VOLATILE
	PARALLEL UNSAFE
	COST 100 ROWS 1000 
AS $BODY$
DECLARE
	firstUnReadMessageDate timestamp without time zone;
BEGIN
	SELECT public.messaages.date_create INTO firstUnReadMessageDate FROM public.message_statuses 
		INNER JOIN public.messages ON public.messages.id = public.message_statuses.last_message_read_id
	WHERE public.message_statuses.user_id = 1 AND public.message_statuses.chat_id = 1 LIMIT 1;
			
			
	IF firstUnReadMessageDate IS NULL THEN
		RETURN QUERY;
	ELSE
		RETURN QUERY SELECT public.messages.content FROM public.messages LIMIT 1;
	END IF;
END;
$BODY$;