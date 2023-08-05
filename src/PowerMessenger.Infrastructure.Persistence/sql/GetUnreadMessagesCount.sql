CREATE OR REPLACE FUNCTION get_unread_message_count(p_user_id bigint, p_chat_id bigint)
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
$$ LANGUAGE plpgsql;