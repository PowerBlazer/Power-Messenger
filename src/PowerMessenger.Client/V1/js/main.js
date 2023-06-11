const apiUrl = "http://localhost:6001/api/v1/";
const apiAssets = "http://localhost:6001/"

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
    
        if(response.status === 401){
            location.href = "Login.html"
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

function convertToLocalTime(dateTimeString) {
    const date = new Date(dateTimeString);
    const localTime = date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    return localTime;
}

const tokenService = new TokenService(apiUrl);
const chatService = new ChatService(apiUrl,tokenService);
const messageService = new MessageService(apiUrl,tokenService);

let selectedChat = {};

async function chatsInitialize(){
    const chatsPanel = document.getElementsByClassName("chats_toolbar__inner")[0];

    let chats = await chatService.getChatsByUser();

    chats.forEach(element => {
         chatsPanel.innerHTML+= `<button class="chat-item" type="button" id="chatid-${element.id}">
            <img src="${apiAssets}${element.photo}" alt="chat-image" width="50" height="50">
            <div class="chat-item_inner">
                <div class="chat-name">${element.name}</div>
                <div class="chat-item-content">
                     <div class="last-message-chat">${element.lastMessage.content}</div>
                     <div class="last-message-chat_time">${convertToLocalTime(element.dateCreate)}</div>
                </div>
            </div>
            ${element.countUnreadMessages === 0 ? "" :`<div class="count-unread-messages">${element.countUnreadMessages}</div>`}
        </button>`
    });

    for (let index = 0; index < chatsPanel.children.length; index++) {
        const element = chatsPanel.children[index];
        
        element.addEventListener("click",()=>{
            chatClickHandler(chats.find(p=>p.id == element.id.split("-").pop()))
        })
    }

    const selectedChat = chatService.getSelectedChat();

    if(selectedChat !== null){
        chatClickHandler(selectedChat);
    }
}

async function chatClickHandler(chat){
    const chatInfoInner = document.getElementsByClassName("chat-info_inner")[0];
    const chatsPanel = document.getElementsByClassName("chats_toolbar__inner")[0];

    let chatinfo = `<img width=45 height=45  class="chat-avatar" alt="chat-avatar" src=${apiAssets}${chat.photo}>
    <div class="chat-info-block">
        <div class="chat-name">${chat.name}</div>
        <div class="chat-count-info_inner">
            <div class="count-participants">Участников - ${chat.countParticipants}</div>
            <div class="count-messages">Количество сообщении - ${chat.countMessages}</div>
        </div>
    </div>`;

    for (let index = 0; index < chatsPanel.children.length; index++) {
        chatsPanel.children[index].style.backgroundColor = "";
        const childrenUnreadMessageCount = chatsPanel.children[index].querySelector(".count-unread-messages");
        if(childrenUnreadMessageCount!==null){
            childrenUnreadMessageCount.style.color = "";
            childrenUnreadMessageCount.style.backgroundColor = "";
        }
        
    }
    
    let clickedChat = document.getElementById(`chatid-${chat.id}`);
    clickedChat.style.backgroundColor = "var(--theme-color)";
    const countUnreadMessagesElement = clickedChat.querySelector(".count-unread-messages");

    if(countUnreadMessagesElement!== null){
        countUnreadMessagesElement.style.backgroundColor = "white";
        countUnreadMessagesElement.style.color = "var(--theme-color)";    
    }
    
    chatInfoInner.innerHTML = chatinfo;
    chatService.setSelectedChat(chat);

    loadMessages(chat);
}

let optionObserver = {
    root: document.querySelector(".message-window-scroll"),
    rootMargin: "0px",
}

let nextLoadObserver = new IntersectionObserver(loadNextMessages,optionObserver);
let prevLoadObserver = new IntersectionObserver(loadPrevMessages,optionObserver);


function createMessageItem(element,messages,index){
    const messageItem = document.createElement("div");
    messageItem.classList.add("message-item");
    messageItem.classList.add(element.isOwner ? "right" : "left");
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
    messageOwnerUserName.innerText = (index === 0 ||
        element.messageOwner.userName !== messages[index - 1].messageOwner.userName)
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
    messageItemInner.appendChild(messageTime);

    messageItemPosition.appendChild(messageItemInner);
    messageItem.appendChild(messageItemPosition);

    return messageItem;
}

async function loadMessages(chat){                  
    const messageWindow = document.getElementsByClassName("message-window_inner")[0];
    messageWindow.innerHTML = "";

    const readMessagesObserver = new IntersectionObserver(entries=>readMessage(entries,messagesResult,readMessagesObserver),optionObserver);
    let messagesResult = await messageService.getMessagesByChat(chat.id,chat.type);

    for (let index = 0; index < messagesResult.messages.length; index++) {
        const element = messagesResult.messages[index];

        const messageItem = createMessageItem(element,messagesResult.messages,index);
        
        messageWindow.appendChild(messageItem);

        if(element.isRead == false && messagesResult.unreadMessagesCount){
            readMessagesObserver.observe(messageItem);
        }
    }

    const firstInreadMessageIndex = messagesResult.messages.findIndex(p=>p.isRead == false);

    if(firstInreadMessageIndex !== -1){
        messageWindow.children[firstInreadMessageIndex].scrollIntoView();
    }
    else{
        messageWindow.children[messageWindow.children.length - 1].scrollIntoView();
    }

    nextLoadObserver.disconnect();
    prevLoadObserver.disconnect();

    if(messagesResult.nextMessagesCount > 0){
        nextLoadObserver.observe(messageWindow.children[messageWindow.children.length-1]);
    }
    
    if(messagesResult.prevMessagesCount > 0){
        prevLoadObserver.observe(messageWindow.children[0]);
    }

    chatService.setUnreadMessagesCount(messagesResult.unreadMessagesCount);
    
}

function setNextMessages(nextMessagesObj){
    const readMessagesObserver = new IntersectionObserver(entries=>readMessage(entries,nextMessagesObj,readMessagesObserver),optionObserver);
    const messageWindow = document.getElementsByClassName("message-window_inner")[0];
    if(nextMessagesObj.messages.length > 0){
        for (let index = 0; index < nextMessagesObj.messages.length; index++) {
            const element = nextMessagesObj.messages[index];
            const messageItem = createMessageItem(element,nextMessagesObj.messages,index);
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

            const messageItem = createMessageItem(element,prevMessagesObj.messages,index);

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

    unreadMessagesCountElement.innerHTML = Number(unreadMessagesCountElement.innerHTML) - visibleMessages.length;

    if(unreadMessagesCountElement.innerHTML == 0)
        unreadMessagesCountElement.remove();

    if(visibleMessages.length > 0){
        const lastEntryId = visibleMessages[visibleMessages.length - 1].target.id;
        const lastReadMessage = messagesResult.messages.find(p=>`message_id-${p.id}` === lastEntryId);
        messageService.setReadMessage(lastReadMessage);
    }
   
}

function listenScrollStop(messageWindow) {
    let isScrolling;
    
    messageWindow.addEventListener('scroll', function() {
      clearTimeout(isScrolling);
      
      isScrolling = setTimeout(async function() {
       const lastReadMessage = messageService.getReadMessage();
       const selectedChat = chatService.getSelectedChat();

       if(lastReadMessage !== null && selectedChat !== null && chatService.getUnreadMessagesCount() > 0){
        let result = await messageService.setMessageAsRead(selectedChat.id,lastReadMessage.id);
        console.log(chatService.getUnreadMessagesCount());
        chatService.setUnreadMessagesCount(result.unreadMessagesCount);
       }
      }, 1000);
    });
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



