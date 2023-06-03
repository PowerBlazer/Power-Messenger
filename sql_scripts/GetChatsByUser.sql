CREATE OR REPLACE FUNCTION public.get_chats_by_user(IN p_user_id bigint)
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
$BODY$;