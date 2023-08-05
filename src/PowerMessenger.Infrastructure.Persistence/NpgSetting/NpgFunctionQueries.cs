// ReSharper disable InvalidXmlDocComment
namespace PowerMessenger.Infrastructure.Persistence.NpgSetting;

public static class NpgFunctionQueries
{
    /// <summary>
    /// GetMessagesGroupChatByMessageId Query
    /// </summary>
    /// <param name="@chatId">ChatId</param>
    /// <param name="@userId">UserId</param>
    /// <param name="@messageId">Message Id</param>
    /// <param name="@next">Next message count</param>
    /// <param name="@prev">Prev message count</param>
    public const string GetMessagesGroupChatByMessageId = @"SELECT 
            id,
            content,
            source,
            date_create as DateCreate,
            type,
            is_owner as IsOwner,
            is_read as IsRead,
            message_user_id as Id,
            message_user_name as UserName,
            message_user_avatar as Avatar,
            forwarded_message_id as Id,
            forwarded_message_user_name as UserName,
            forwarded_message_content as Content,
            forwarded_message_type as Type,
            forwarded_message_chat_id as ChatId
        FROM get_messages_group_chat_by_message_id(@chatId,@userId,@messageId,@next,@prev)";
    
    /// <summary>
    /// GetMessagesGroupChatByUser Query
    /// </summary>
    /// <param name="@chatId">ChatId</param>
    /// <param name="@userId">UserId</param>
    /// <param name="@next">Next message count</param>
    /// <param name="@prev">Prev message count</param>
    public const string GetMessagesGroupChatByUser = @"SELECT 
            id,
            content,
            source,
            date_create as DateCreate,
            type,
            is_owner as IsOwner,
            is_read as IsRead,
            message_user_id as Id,
            message_user_name as UserName,
            message_user_avatar as Avatar,
            forwarded_message_id as Id,
            forwarded_message_user_name as UserName,
            forwarded_message_content as Content,
            forwarded_message_type as Type,
            forwarded_message_chat_id as ChatId
        FROM get_messages_group_chat_by_user(@chatId,@userId,@next,@prev)";
    
    /// <summary>
    /// GetNextMessagesGroupChatByUser Query
    /// </summary>
    /// <param name="@chatId">Chat Id</param>
    /// <param name="@userId">User Id</param>
    /// <param name="@messageId">Message Id</param>
    /// <param name="@count">Messages Count</param>
    public const string GetNextMessagesGroupChatByUser = @"SELECT 
            id,
            content,
            source,
            date_create as DateCreate,
            type,
            is_owner as IsOwner,
            is_read as IsRead,
            message_user_id as Id,
            message_user_name as UserName,
            message_user_avatar as Avatar,
            forwarded_message_id as Id,
            forwarded_message_user_name as UserName,
            forwarded_message_content as Content,
            forwarded_message_type as Type,
            forwarded_message_chat_id as ChatId
        FROM get_next_messages_group_chat_by_user(@chatId,@userId,@messageId,@count)";
    
    /// <summary>
    /// GetPrevMessagesGroupChatByUser Query
    /// </summary>
    /// <param name="@chatId">Chat Id</param>
    /// <param name="@userId">User Id</param>
    /// <param name="@messageId">Message Id</param>
    /// <param name="@count">Messages Count</param>
    public const string GetPrevMessagesGroupChatByUser = @"SELECT 
            id,
            content,
            source,
            date_create as DateCreate,
            type,
            is_owner as IsOwner,
            is_read as IsRead,
            message_user_id as Id,
            message_user_name as UserName,
            message_user_avatar as Avatar,
            forwarded_message_id as Id,
            forwarded_message_user_name as UserName,
            forwarded_message_content as Content,
            forwarded_message_type as Type,
            forwarded_message_chat_id as ChatId
        FROM get_prev_messages_group_chat_by_user(@chatId,@userId,@messageId,@count)";
    
    /// <summary>
    /// GetLastMessagesGroupChatByUser Query
    /// </summary>
    /// <param name="@chatId">Chat Id</param>
    /// <param name="@userId">User Id</param>
    /// <param name="@count">Messages Count</param>
    public const string GetLastMessagesGroupChatByUser = @"SELECT 
            id,
            content,
            source,
            date_create as DateCreate,
            type,
            is_owner as IsOwner,
            is_read as IsRead,
            message_user_id as Id,
            message_user_name as UserName,
            message_user_avatar as Avatar,
            forwarded_message_id as Id,
            forwarded_message_user_name as UserName,
            forwarded_message_content as Content,
            forwarded_message_type as Type,
            forwarded_message_chat_id as ChatId
        FROM get_last_messages_group_chat_by_user(@chatId,@userId,@count)";
    
    /// <summary>
    /// Get Message Data by Id Query
    /// </summary>
    /// <param name="@messageId">Message Id</param>
    public const string GetMessageDataById = @"SELECT 
            id as Id,
            chat_id as ChatId,
            content,
            source,
            date_create as DateCreated,
            type,
            message_user_id as Id,
            message_user_name as UserName,
            message_user_avatar as Avatar,
            forwarded_message_id as Id,
            forwarded_message_user_name as UserName,
            forwarded_message_content as Content,
            forwarded_message_type as Type,
            forwarded_message_chat_id as ChatId
        FROM get_message_data_by_id(@messageId)";

    /// <summary>
    /// Get Chats By User Id Query
    /// </summary>
    /// <param name="@userId"></param>
    public const string GetChatsDataByUserId = @"SELECT 
                Id,
                Name,
                datecreate,
                photo,
                description,
                type,
                countparticipants,
                countunreadmessages,
                countmessages,
                lastmessageid as Id,
                lastmessageusername as UserName,
                lastmessagecontent as Content,
                lastmessagetype as Type,
                lastmessagedatecreate as DateCreate
            FROM get_chats_by_user(@userId)";
    /// <summary>
    /// Get Unread Messages Count
    /// </summary>
    /// <param name="@userId">User Id</param>
    /// <param name="@chatId">Chat Id</param>
    public const string GetUnreadMessagesCount = @"SELECT * FROM get_unread_message_count(@userId, @chatId)";
}