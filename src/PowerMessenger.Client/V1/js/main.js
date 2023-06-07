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
    
        if(response.status === 401 || response.status === 400 
            || response.status === 500){
            location.href = "Login.html"
        }
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

    async getChatsByUser(){
        const response = await fetch(`${apiUrl}Chat/chats`,{
            method:"GET",
            headers:this.tokenService.requestHeaders()
        });
    
        return await this.tokenService.handleTokenResponse(response,this.getChatsByUser)
    }

}

class MessageService{
    constructor(uri,tokenService){
        this.tokenServive = tokenService;
        this.uri = uri;
    }

    async getMessagesByChat(chatId,type){
        switch (type){
            case "Group":
                const response = await fetch(`${this.uri}Message/groupchat/${chatId}?next=10`,{
                    method:"GET",
                    headers:this.tokenServive.requestHeaders()
                });
                
                return await this.tokenServive.handleTokenResponse(response,this.getMessagesByChat,chatId,type);
            default:
                break;
        }
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
            <div class="count-unread-messages">${element.countUnreadMessages}</div>
        </button>`
    });

    for (let index = 0; index < chatsPanel.children.length; index++) {
        const element = chatsPanel.children[index];
        
        element.addEventListener("click",()=>{
            chatClickHandler(chats.find(p=>p.id == element.id.split("-").pop()))
        })
    }
}

async function chatClickHandler(chat){
    const chatInfoInner = document.getElementsByClassName("chat-info_inner")[0];

    let chatinfo = `<img width=50 height=50  class="chat-avatar" src=${apiAssets}${chat.photo}>
    <div class="chat-info-block">
        <div class="chat-name">${chat.name}</div>
        <div class="chat-count-info_inner">
            <div class="count-participants">Участников - ${chat.countParticipants}</div>
            <div class="count-messages">Количество сообщении - ${chat.countMessages}</div>
        </div>
    </div>`;

    chatInfoInner.innerHTML = chatinfo;

    loadMessages(chat);
}

async function loadMessages(chat){                  
    const messageWindow = document.getElementsByClassName("message-window_inner")[0];
    
    messageWindow.innerHTML = "";

    let messagesResult = await messageService.getMessagesByChat(chat.id,chat.type);

    for (let index = 0; index < messagesResult.messages.length; index++) {
        const element = messagesResult.messages[index];
        
        messageWindow.innerHTML+= `
        <div class="message-item ${element.isOwner ? "right":"left"}">
            <div class="message-item_position">
                ${(index === messagesResult.messages.length-1 
                    || messagesResult.messages[index + 1].messageOwner.userName !== element.messageOwner.userName) && !element.isOwner ? 
                    "<img src=\"./assets/img/testimg.png\" width=\"40\" height=\"40\" class=\"message-item-avatar\">" : ""}
                <div class="message-item_inner">
                    <div class="message-owner-userName">${index === 0 || element.messageOwner.userName !== messagesResult.messages[index - 1].messageOwner.userName ? element.messageOwner.userName : ""}</div>
                    <div class="message-content">${element.content === null ? "":element.content}</div>
                    <div class="message-time">${convertToLocalTime(element.dateCreate)}</div>
                </div>
            </div>
        </div>`

    }

}

document.addEventListener("DOMContentLoaded",() => {
    document.getElementById("logout-button").addEventListener("click",logout);
    document.getElementById("test").addEventListener("click",test);

    chatsInitialize();
});

function logout(){
    tokenService.clearTokens();
    location.href = "Login.html";
}

async function test(){
    let messageWindow = document.getElementsByClassName("message-window_inner")[0];

    messageWindow.innerHTML = `
        <div class="message-item left">
            <div class="message-item_position">
                <img src="./assets/img/testimg.png" width="40" height="40" class="message-item-avatar">
                <div class="message-item_inner">
                    <div class="message-owner-userName">Grubgerg</div>
                    <div class="message-content">Hello_World</div>
                    <div class="message-time">23:01</div>
                </div>
            </div>
        </div>` + messageWindow.innerHTML;
}


