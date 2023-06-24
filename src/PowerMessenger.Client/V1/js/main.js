const apiUrl = "http://localhost:6001/api/v1/";
const apiAssets = "http://localhost:6001/";

class TokenService{
    constructor(apiUrl){
        this.url = apiUrl
    }

    requestHeaders(){
        return new Headers({
            'Content-Type': 'application/json;charset=utf-8',
            "Authorization": `Bearer ${this.getAccessToken()}`
        });
    }

    getAccessToken(){
        return localStorage.getItem("accessToken");
    }
    
    getRefreshToken(){
        return localStorage.getItem("refreshToken");
    }  

    async handleTokenResponse(response, action, ...args){
        if (response.status === 200) {
          const result = await response.json();
          return result.result;
        }
      
        if (response.status === 401) {
            const successRefresh = await this.refreshToken();
      
            if(successRefresh) {
                return await action(...args);
            }
        }
      
        throw new Error('Request failed with status: ' + response.status);
    }

    async refreshToken(){
        const bodyJson = JSON.stringify({
            accessToken:this.getAccessToken(),
            refreshToken:this.getRefreshToken()
        });
    
        const response = await fetch(`${this.url}Authorization/Refresh`,{
            method:"POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body:bodyJson
        });
    
        if(response.status === 200){
            const result = (await response.json()).result;
    
            localStorage.setItem("accessToken",result.accessToken);
            localStorage.setItem("refreshToken",result.refreshToken);
    
            return true;
        }
    
        if(response.status === 401 || response.status === 400){
            //location.href = "Login.html"
        }

        throw new Error('Request failed with status: ' + response.status);
    }

    clearTokens(){
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
    }
}

class ChatService{

    constructor(uri,tokenService){
        this.tokenService = tokenService;
        this.uri = uri;
    }

    getChatsByUser = async () => {
        const response = await fetch(`${apiUrl}Chat/chats`,{
            method:"GET",
            headers: this.tokenService.requestHeaders()
        });
    
        return await this.tokenService.handleTokenResponse(response,this.getChatsByUser)
    }

    setSelectedChat = (chat) => {
        const chatJson = JSON.stringify(chat);
        localStorage.removeItem("selected-chat");
        localStorage.setItem("selected-chat",chatJson);
    }

    getSelectedChat = () => {
        const chatJson = localStorage.getItem("selected-chat");

        if(chatJson === null || chatJson.length === 0){
            return null;
        }

        const selectedChat = JSON.parse(chatJson);

        return selectedChat;
    }

    setUnreadMessagesCount = (count) => {
        localStorage.removeItem("unread-messages-count");
        localStorage.setItem("unread-messages-count",count);
    }

    getUnreadMessagesCount = () => {
        return localStorage.getItem("unread-messages-count");
    }

}

class MessageService{
    constructor(uri, tokenService) {
        this.tokenService = tokenService;
        this.uri = uri;
      }
    
      getMessagesByChat = async (chatId, type) => {
        switch (type) {
          case "Group":
            const response = await fetch(`${apiUrl}Message/groupchat/${chatId}?next=10`, {
              method: "GET",
              headers: this.tokenService.requestHeaders(),
            });
    
            return await this.tokenService.handleTokenResponse(response, this.getMessagesByChat, chatId, type);
          default:
            break;
        }
      }
    
      getNextMessagesByChat = async (chatId, messageId, type) => {
        switch (type) {
          case "Group":
            const response = await fetch(`${apiUrl}Message/groupchat/${chatId}/next/${messageId}`, {
              method: "GET",
              headers: this.tokenService.requestHeaders(),
            });
    
            return await this.tokenService.handleTokenResponse(response, this.getNextMessagesByChat, chatId, messageId, type);
          default:
            break;
        }
      }
    
      getPrevMessagesByChat = async (chatId, messageId, type) => {
        switch (type) {
          case "Group":
            const response = await fetch(`${this.uri}Message/groupchat/${chatId}/prev/${messageId}`, {
              method: "GET",
              headers: this.tokenService.requestHeaders(),
            });
    
            return await this.tokenService.handleTokenResponse(response, this.getPrevMessagesByChat, chatId, messageId, type);
          default:
            break;
        }
      }

      getLastMessagesByChat = async (chatId,type) => {
        switch (type) {
            case "Group":
              const response = await fetch(`${this.uri}Message/groupchat/${chatId}/last`, {
                method: "GET",
                headers: this.tokenService.requestHeaders(),
              });
      
              return await this.tokenService.handleTokenResponse(response, this.getPrevMessagesByChat, chatId, type);
            default:
              break;
        }
      }

      getMessagesByMessageId = async (chatId,messageId,type) => {
        switch (type) {
            case "Group":
              const response = await fetch(`${this.uri}Message/groupchat/${chatId}/message/${messageId}`, {
                method: "GET",
                headers: this.tokenService.requestHeaders(),
              });
      
              return await this.tokenService.handleTokenResponse(response, this.getMessageByMessageId, chatId, type);
            default:
              break;
        }
      }
      
      setMessageAsRead = async (chatId,messageId) => {
        const response = await fetch(`${this.uri}Message/${messageId}/read?chatId=${chatId}`, {
            method: "PUT",
            headers: this.tokenService.requestHeaders(),
        });

        return await this.tokenService.handleTokenResponse(response, this.getPrevMessagesByChat, chatId, messageId);
      }

      setReadMessage = (message) => {
        localStorage.removeItem("read-message");
        localStorage.setItem("read-message",JSON.stringify(message));
      }

      getReadMessage = () => {
        const readMessageJson = localStorage.getItem("read-message");
        if(readMessageJson == null || readMessageJson.length === 0){
            return null;
        }

        return JSON.parse(readMessageJson);
      }
}

class ElementsService{
    createUnreadMessagesCountMark = (count) => {
        const countUnreadMessagesMark = document.createElement("div");
        countUnreadMessagesMark.classList.add("count-unread-messages");
        countUnreadMessagesMark.innerText = count;

        return countUnreadMessagesMark;
    }

    createChatElements = (panel,chats) => {
        chats.forEach(element => {
            const chatItem = document.createElement("button");
            chatItem.classList.add("chat-item");
            chatItem.setAttribute("type", "button");
            chatItem.setAttribute("id", `chatid-${element.id}`);
            
            const chatImage = document.createElement("img");
            chatImage.setAttribute("src", `${apiAssets}${element.photo}`);
            chatImage.setAttribute("alt", "chat-image");
            chatImage.setAttribute("width", "50");
            chatImage.setAttribute("height", "50");
            
            const chatItemInner = document.createElement("div");
            chatItemInner.classList.add("chat-item_inner");
            
            const chatName = document.createElement("div");
            chatName.classList.add("chat-name");
            chatName.innerText = element.name;
            
            const chatItemContent = document.createElement("div");
            chatItemContent.classList.add("chat-item-content");
            
            const chatItemContentInner = document.createElement("div");
            chatItemContentInner.classList.add("chat-item-content_inner");
            chatItemContentInner.dir = "auto"

            const lastMessageChatOwner = document.createElement("div");
            lastMessageChatOwner.classList.add("last-message-owner");
            lastMessageChatOwner.innerHTML = element.lastMessage.userName+":";

            const lastMessageChat = document.createElement("div");
            lastMessageChat.classList.add("last-message-chat")
            lastMessageChat.dir = "auto";
            lastMessageChat.innerText = element.lastMessage.content;
            
            const lastMessageChatTime = document.createElement("div");
            lastMessageChatTime.classList.add("last-message-chat_time");
            lastMessageChatTime.innerText = convertToLocalTime(element.lastMessage.dateCreate);
            
            const countUnreadMessages = this.createUnreadMessagesCountMark(element.countUnreadMessages);
            
            chatItemContentInner.appendChild(lastMessageChatOwner);
            chatItemContentInner.appendChild(lastMessageChat);
            chatItemContent.appendChild(chatItemContentInner);
            chatItemContent.appendChild(lastMessageChatTime);
            
            chatItemInner.appendChild(chatName);
            chatItemInner.appendChild(chatItemContent);
            
            chatItem.appendChild(chatImage);
            chatItem.appendChild(chatItemInner);
    
            if(element.countUnreadMessages > 0){
                chatItem.appendChild(countUnreadMessages);
            }
            
            panel.appendChild(chatItem);

            chatItem.addEventListener("click", () => {
                chatClickHandler(element);
            });
        });
    }

    resetChatPanelStyles = (chatsPanel) => {
        Array.from(chatsPanel.children).forEach(chatItem => {
            chatItem.style.backgroundColor = "";
            const countUnreadMessagesElement = chatItem.querySelector(".count-unread-messages");
            if (countUnreadMessagesElement !== null) {
                countUnreadMessagesElement.style.backgroundColor = "";
                countUnreadMessagesElement.style.color = "";
            }
        });
    }

    createMessageItem = (element,messages,index,callBack) => {
        const messageItem = document.createElement("div");
        messageItem.classList.add("message-item");
        messageItem.classList.add(element.isOwner ? "right" : "left");
        

        if(element.isOwner){
            messageItem.classList.add("owner");
        }

        messageItem.id = `message_id-${element.id}`;

        const messageItemPosition = document.createElement("div");
        messageItemPosition.classList.add("message-item_position");

        if ((index === messages.length - 1 ||
            messages[index + 1].messageOwner.userName !== element.messageOwner.userName)
            && !element.isOwner) {
            const avatarImage = document.createElement("img");
            avatarImage.src = "./assets/img/testimg.png";
            avatarImage.width = "40";
            avatarImage.height = "40";
            avatarImage.alt = "message-avatar";
            avatarImage.classList.add("message-item-avatar");
            messageItemPosition.appendChild(avatarImage);
        }

        const messageItemInner = document.createElement("div");
        messageItemInner.classList.add("message-item_inner");

        const messageOwnerUserName = document.createElement("div");
        messageOwnerUserName.classList.add("message-owner-userName");

        messageOwnerUserName.innerText = ((index === 0 ||
            element.messageOwner.userName !== messages[index - 1].messageOwner.userName) && !element.isOwner)
            ? element.messageOwner.userName
            : "";

        messageItemInner.appendChild(messageOwnerUserName);
        if(element.forwardedMessage !== null){
            const forwardedMessageButton = document.createElement("button");
            forwardedMessageButton.classList.add("forwarded-message-button");
            const forwardedLine = document.createElement("div");
            forwardedLine.classList.add("forwarded-line");
            forwardedMessageButton.appendChild(forwardedLine);
            const forwardedMessageUserName = document.createElement("div");
            forwardedMessageUserName.classList.add("forwarded_message_username");
            forwardedMessageUserName.innerHTML = element.forwardedMessage.userName;
            forwardedMessageButton.appendChild(forwardedMessageUserName);
            const forwardedMessageContent = document.createElement("div");
            forwardedMessageContent.classList.add("forwarded_message_content");
            forwardedMessageContent.innerHTML = element.forwardedMessage.content;

            if(callBack){
                forwardedMessageButton.addEventListener('click',callBack)
            }
            

            forwardedMessageButton.appendChild(forwardedMessageContent);
            messageItemInner.appendChild(forwardedMessageButton);
        }
        

        const messageContent = document.createElement("div");
        messageContent.classList.add("message-content");
        messageContent.innerText = element.content === null ? "" : element.content;
        messageItemInner.appendChild(messageContent);

        const messageTime = document.createElement("div");
        messageTime.classList.add("message-time");
        messageTime.innerText = convertToLocalTime(element.dateCreate);
        messageTime.title = new Date(element.dateCreate).toUTCString();
        messageItemInner.appendChild(messageTime);

        messageItemPosition.appendChild(messageItemInner);
        messageItem.appendChild(messageItemPosition);

        return messageItem;
    }

    createShutdownButton = (chatId,lastMessageId,unreadMessageCount) => {
        const button = document.createElement("button");
        button.className = "shutdown-position";
        if(unreadMessageCount>0){
            button.dataset.unreadCount = unreadMessageCount;
        }
        
        button.dataset.lastMessageId = lastMessageId;

        const image = document.createElement("img");
        image.src = "./assets/down.svg";
        image.width = "25";
        image.height = "25";

        button.appendChild(image);
        button.addEventListener("click",()=>shutDownButtonHandler(lastMessageId,chatId));

        return button;
    }
}

function convertToLocalTime(dateTimeString){
    const date = new Date(dateTimeString);
    const localTime = date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    return localTime;
}

const tokenService = new TokenService(apiUrl);
const chatService = new ChatService(apiUrl,tokenService);
const messageService = new MessageService(apiUrl,tokenService);
const elementsService = new ElementsService();

let selectedChat = {};
let optionObserver = {
    root: document.querySelector(".message-window-scroll"),
    rootMargin: "0px",
}

let nextLoadObserver = new IntersectionObserver(loadNextMessages,optionObserver);
let prevLoadObserver = new IntersectionObserver(loadPrevMessages,optionObserver);


async function chatsInitialize(){
    const chatsPanel = document.getElementsByClassName("chats_toolbar__inner")[0];
    
    let chats = await chatService.getChatsByUser();
    
    elementsService.createChatElements(chatsPanel,chats);
    
    const selectedChat = chatService.getSelectedChat();
    
    if (selectedChat !== null) {
        const chat = chats.find(p=>p.id == selectedChat.id);

        chatClickHandler(chat);
        chatService.setSelectedChat(chat);
    }
}

async function chatClickHandler(chat){
    const chatInfoInner = document.getElementsByClassName("chat-info_inner")[0];
    const chatsPanel = document.getElementsByClassName("chats_toolbar__inner")[0];
    const shutDownPosition = document.querySelector(".shutdown-position");

    if(shutDownPosition){
        shutDownPosition.remove();
    }

    chatInfoInner.innerHTML = `
        <img width="45" height="45" class="chat-avatar" alt="chat-avatar" src="${apiAssets}${chat.photo}">
        <div class="chat-info-block">
            <div class="chat-name">${chat.name}</div>
            <div class="chat-count-info_inner">
                <div class="count-participants">Участников - ${chat.countParticipants}</div>
                <div class="count-messages">Количество сообщений - ${chat.countMessages}</div>
            </div>
        </div>
    `;

    elementsService.resetChatPanelStyles(chatsPanel);

    const clickedChat = document.getElementById(`chatid-${chat.id}`);

    clickedChat.style.backgroundColor = "var(--theme-color)";

    const countUnreadMessagesElement = clickedChat.querySelector(".count-unread-messages");

    if (countUnreadMessagesElement !== null){
        countUnreadMessagesElement.style.backgroundColor = "white";
        countUnreadMessagesElement.style.color = "var(--theme-color)";
    }

    chatService.setSelectedChat(chat);

    loadMessages(chat);
}

async function loadMessages(chat){
    const messageWindow = document.getElementsByClassName("message-window_inner")[0];
    const senderPanel = document.querySelector(".sender-panel");
    const shutDownPosition = document.querySelector(".shutdown-position");

    messageWindow.innerHTML = "";

    const readMessagesObserver = new IntersectionObserver(entries => 
        readMessage(entries, messagesResult, readMessagesObserver), optionObserver);

    const messagesResult = await messageService.getMessagesByChat(chat.id, chat.type);

    for (let index = 0; index < messagesResult.messages.length; index++) {
        const message = messagesResult.messages[index];

        const messageItem = elementsService.createMessageItem(message, messagesResult.messages, index,()=>forwardedMessageHandler(message));

        messageWindow.appendChild(messageItem);

        if (!message.isRead && messagesResult.unreadMessagesCount) {
            readMessagesObserver.observe(messageItem);
        }
    }

    const firstUnreadMessageIndex = messagesResult.messages.findIndex(p => !p.isRead);

    if (firstUnreadMessageIndex !== -1) {
        messageWindow.children[firstUnreadMessageIndex].scrollIntoView({block: 'center'});
    }else{
        messageWindow.children[messageWindow.children.length - 1].scrollIntoView({block: 'center'});
    }

    nextLoadObserver.disconnect();
    prevLoadObserver.disconnect();

    if (messagesResult.nextMessagesCount > 0) {
        nextLoadObserver.observe(messageWindow.children[messageWindow.children.length - 1]);
    }

    if (messagesResult.prevMessagesCount > 0) {
        prevLoadObserver.observe(messageWindow.children[0]);
    }

    chatService.setUnreadMessagesCount(messagesResult.unreadMessagesCount);

    if(shutDownPosition){
        shutDownPosition.remove();
    }

    if(chat.countUnreadMessages > 0){
        senderPanel.appendChild(elementsService.createShutdownButton(chat.id,chat.lastMessage.id,messagesResult.unreadMessagesCount));
    }

}

async function setFowardedMessages(messageId,messagesResult){
    const messageWindow = document.getElementsByClassName("message-window_inner")[0];
    // const senderPanel = document.querySelector(".sender-panel");
    // const shutDownPosition = document.querySelector(".shutdown-position");
    messageWindow.innerHTML = "";


    for (let index = 0; index < messagesResult.messages.length; index++) {
        const message = messagesResult.messages[index];
        const messageItem = elementsService.createMessageItem(message, messagesResult.messages, index,()=>forwardedMessageHandler(message));
        
        messageWindow.appendChild(messageItem);
    }

    const messageIndex = messagesResult.messages.findIndex(p => p.id === messageId);
    messageWindow.children[messageIndex].scrollIntoView({behavior:"smooth",block: 'center'});



    nextLoadObserver.disconnect();
    prevLoadObserver.disconnect();

    if (messagesResult.nextMessagesCount > 0) {
        nextLoadObserver.observe(messageWindow.children[messageWindow.children.length - 1]);
    }

    if (messagesResult.prevMessagesCount > 0) {
        prevLoadObserver.observe(messageWindow.children[0]);
    }
}

function setNextMessages(nextMessagesObj){
    const readMessagesObserver = new IntersectionObserver(entries=>readMessage(entries,nextMessagesObj,readMessagesObserver),optionObserver);

    const messageWindow = document.getElementsByClassName("message-window_inner")[0];

    if(nextMessagesObj.messages.length > 0){
        for (let index = 0; index < nextMessagesObj.messages.length; index++) {
            const element = nextMessagesObj.messages[index];
            const messageItem = elementsService.createMessageItem(element,nextMessagesObj.messages,index,()=>forwardedMessageHandler(element));
            messageWindow.appendChild(messageItem);

            if(element.isRead == false){
                readMessagesObserver.observe(messageItem);
            }
        }
    }

    if(nextMessagesObj.nextCount !== 0){
        nextLoadObserver.observe(messageWindow.children[messageWindow.children.length-1]);
    }
}

function setPrevMessages(prevMessagesObj){
    const messageWindow = document.getElementsByClassName("message-window_inner")[0];
    const messageWindowScroll = document.querySelector(".message-window-scroll");

    const prevScrollHeight = messageWindowScroll.scrollHeight;
    const prevScrollTop = messageWindowScroll.scrollTop;

    if(prevMessagesObj.messages.length > 0){
        const fragment = document.createDocumentFragment();

        for (let index = 0; index < prevMessagesObj.messages.length; index++) {
            const element = prevMessagesObj.messages[index];

            const messageItem = elementsService.createMessageItem(element,prevMessagesObj.messages,index,()=>forwardedMessageHandler(element));

            fragment.appendChild(messageItem);
        }

        messageWindow.prepend(fragment);

        const newScrollHeight = messageWindowScroll.scrollHeight;
        const scrollDifference = newScrollHeight - prevScrollHeight;
        messageWindowScroll.scrollTop = prevScrollTop + scrollDifference;
    }
    

    
    if(prevMessagesObj.prevCount !== 0){
        prevLoadObserver.observe(messageWindow.children[0]);
    }
}

function setLastMessages(lastMessagesObj){
    const messagesWindow = document.querySelector(".message-window_inner");
    messagesWindow.innerHTML = "";

    for (let index = 0; index < lastMessagesObj.messages.length; index++) {
        const message = lastMessagesObj.messages[index];
        const messageItem = elementsService.createMessageItem(message, lastMessagesObj.messages, index,()=>forwardedMessageHandler(message));
        messagesWindow.appendChild(messageItem);
    }
    prevLoadObserver.disconnect();
    console.log(messagesWindow.children[messagesWindow.children.length - 1])
    messagesWindow.children[messagesWindow.children.length - 1].scrollIntoView({behavior:"smooth"});

    setTimeout(()=>{
        if (lastMessagesObj.prevMessagesCount > 0) {
            prevLoadObserver.observe(messagesWindow.children[0]);
        }
    },200);
    
}

function setLastMessage(message){
    
}

function setStatuseMessage(messageId,status){

}

function consumerMessage(message){
    const selectedChat = chatService.getSelectedChat();

    if(selectedChat.id == message.chatId){

    }
}

async function loadNextMessages(entries){
    if(entries.length > 0){
        const entry = entries[0];

        if(entry.isIntersecting){
            nextLoadObserver.unobserve(entry.target);

            const messageId = entry.target.id.split("-").pop();
            const selectedChat = chatService.getSelectedChat();
            const nextMessages = await messageService.getNextMessagesByChat(selectedChat.id,messageId,selectedChat.type);

            setNextMessages(nextMessages);
        }
    }
}

async function loadPrevMessages(entries){
    if(entries.length > 0){
        const entry = entries[0];
        
        if(entry.isIntersecting){
            prevLoadObserver.unobserve(entry.target);
            const messageId = entry.target.id.split("-").pop();
            const selectedChat = chatService.getSelectedChat();
            const prevMessagesObj = await messageService.getPrevMessagesByChat(selectedChat.id,messageId,selectedChat.type);

            setPrevMessages(prevMessagesObj);
        }
    }
}

function readMessage(entries,messagesResult,readMessagesObserver){
    const visibleMessages = entries.filter(entry=>{
        return messagesResult.messages.findIndex(p=>`message_id-${p.id}` === entry.target.id && p.isRead === false) !== -1 && entry.isIntersecting;
    }).sort(p=>p.boundingClientRect.y);

    visibleMessages.forEach(messageEntry => readMessagesObserver.unobserve(messageEntry.target));

    const selectedChat = chatService.getSelectedChat();
    const selectedChatElement = document.getElementById(`chatid-${selectedChat.id}`);
    const unreadMessagesCountElement = selectedChatElement.querySelector(".count-unread-messages");
    const shutDownPosition = document.querySelector(".shutdown-position");


    unreadMessagesCountElement.innerHTML = Number(unreadMessagesCountElement.innerHTML) - visibleMessages.length;

    if(shutDownPosition !== null){
        shutDownPosition.dataset.unreadCount = Number(shutDownPosition.dataset.unreadCount) - visibleMessages.length;
        if(shutDownPosition.dataset.unreadCount == 0){
            shutDownPosition.remove();
        }
    }

    

    if(unreadMessagesCountElement.innerHTML == 0)
        unreadMessagesCountElement.remove();

    if(visibleMessages.length > 0){
        const lastEntryId = visibleMessages[visibleMessages.length - 1].target.id;
        const lastReadMessage = messagesResult.messages.find(p=>`message_id-${p.id}` === lastEntryId);
        messageService.setReadMessage(lastReadMessage);
    }
   
}

function listenScrollStop(messageWindow){
    let isScrolling;
    const senderPanel = document.querySelector(".sender-panel");
    
    messageWindow.addEventListener('scroll', function(e) {
      clearTimeout(isScrolling);
      const shutDownPosition = document.querySelector(".shutdown-position");
      var scrollPosition = e.target.scrollTop;
      var blockHeight = e.target.clientHeight;

      // Определить высоту содержимого блока
      var contentHeight = e.target.scrollHeight;

      // Проверить, находится ли прокрутка в нижней части блока
      if (scrollPosition + blockHeight < contentHeight / 1.3) {
        if(!shutDownPosition){
            senderPanel.appendChild(elementsService.createShutdownButton(chatService.getSelectedChat().id,0,0))
        }
            
      } else {
        const messagesPanel = document.querySelector(".message-window_inner");
        let messageIndex = Array.from(messagesPanel.children).findIndex(p=>p.id === `message_id-${chatService.getSelectedChat().lastMessage.id}`);
        if(shutDownPosition && chatService.getUnreadMessagesCount() == 0 && messageIndex !== -1){
            shutDownPosition.remove();
        }
      }


      isScrolling = setTimeout(async function() {
       const lastReadMessage = messageService.getReadMessage();
       const selectedChat = chatService.getSelectedChat();

       if(lastReadMessage !== null && selectedChat !== null && chatService.getUnreadMessagesCount() > 0){
        let result = await messageService.setMessageAsRead(selectedChat.id,lastReadMessage.id);
        chatService.setUnreadMessagesCount(result.unreadMessagesCount);
       }
      }, 1000);
    });
}
// В чате есть lastMessageId, если в списке сообщений есть такой id,
// то можно пролистать вниз или добавить полученное сообщение 
async function shutDownButtonHandler(lastMessageId,chatId){
   const selectedChatElement = document.getElementById(`chatid-${chatId}`);
   const unreadMessagesCountElement = selectedChatElement.querySelector(".count-unread-messages");
   const shutDownPosition = document.querySelector(".shutdown-position");
   const messageWindow = document.querySelector(".message-window_inner");
   const selectedChat = chatService.getSelectedChat();
   
   if(unreadMessagesCountElement){
    unreadMessagesCountElement.remove()
   }

   if(shutDownPosition){
    shutDownPosition.remove();
   }

   let messageElement = Array.from(messageWindow.children).find(p=>p.id == `message_id-${selectedChat.lastMessage.id}`);

    if(messageElement){
        messageElement.scrollIntoView({ behavior: 'smooth', block: 'center' });
        return;
    }

    if(chatService.getUnreadMessagesCount() > 0){
        const unreadMessageCount = await messageService.setMessageAsRead(chatId,lastMessageId);
        chatService.setUnreadMessagesCount(unreadMessageCount.unreadMessagesCount); 
       }


   const lastMessagesObj = await messageService.getLastMessagesByChat(chatId,"Group");

   setLastMessages(lastMessagesObj);
}

async function sendMessage(){
    let unreadMessagesCount = chatService.getUnreadMessagesCount();

    
}

async function forwardedMessageHandler(message){
    const chat = chatService.getSelectedChat();
    const messageWindow = document.querySelector(".message-window_inner")
    let messageElement = Array.from(messageWindow.children).find(p=>p.id == `message_id-${message.forwardedMessage.id}`);

    if(messageElement){
        messageElement.scrollIntoView({ behavior: 'smooth', block: 'center' });
        return;
    }

    const messagesResult = await messageService.getMessagesByMessageId(chat.id,message.forwardedMessage.id,"Group");
    setFowardedMessages(message.forwardedMessage.id,messagesResult);
}


document.addEventListener("DOMContentLoaded",() => {
    document.getElementById("logout-button").addEventListener("click",logout);
    listenScrollStop(document.querySelector(".message-window-scroll"));
    chatsInitialize();
});

function logout(){
    tokenService.clearTokens();
    location.href = "Login.html";
}



