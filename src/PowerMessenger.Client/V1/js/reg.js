const apiUrl = "http://localhost:6001/api/v1/Authorization";

const emailWindow = document.getElementsByClassName("email-window");
const codeWindow = document.getElementsByClassName("code_verification-window");
const registerWindow = document.getElementsByClassName("register-window");

codeWindow[0].style.display = "none";
registerWindow[0].style.display = "none"




async function NextEmail(){
    const emailInner = document.getElementById("email-input").value;

    const bodyJson = JSON.stringify({
        email:`${emailInner}`
    })

    const response = await fetch(apiUrl+"/SendEmailVerification",{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body:bodyJson
    });

    if(response.status === 200){
        emailWindow[0].style.display = "none";
        codeWindow[0].style.display = "";

        let sessionId = await response.json();

        localStorage.setItem("sessionId",sessionId.result);
    }

    if(response.status === 400){
        let errorContent = await response.json();x
        ErrorNotification(errorContent.Errors);
    }
}

async function ResendVerificationCode(){
    const emailInner = document.getElementById("email-input").value;
    const sessionId = localStorage.getItem("sessionId");

    const bodyJson = JSON.stringify({
        sessionId:sessionId,
        email:emailInner
    })

    const response = await fetch(apiUrl+"/ResendEmailVerification",{
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body:bodyJson
    });

    if(response.status === 200){
        alert("Отправлено");
    }

}

async function NextCode(){
    const codeInner = document.getElementById("codeVerification-input").value;
    const sessionId = localStorage.getItem("sessionId");

    const bodyJson = JSON.stringify({
        sessionId:`${sessionId}`,
        verificationCode:`${codeInner}`
    });

    const response = await fetch(apiUrl+"/ConfirmEmail",{
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body:bodyJson
    });

    if (response.status === 200) {
        codeWindow[0].style.display = "none";
        registerWindow[0].style.display = "";
    }

    if(response.status === 400){
        let errorContent = await response.json();
        ErrorNotification(errorContent.Errors);
    }
}

async function Register(){
    const userName = document.getElementById("username-input").value;
    const password = document.getElementById("password-input").value;
    const passwordConfirm = document.getElementById("passwordConfirm-input").value;
    const sessionId = localStorage.getItem("sessionId");

    const bodyJson = JSON.stringify({
        sessionId:`${sessionId}`,
        userName:`${userName}`,
        password:`${password}`,
        passwordConfirm:`${passwordConfirm}`
    });

    const response = await fetch(apiUrl+"/Registration",{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body:bodyJson
    });

    if(response.status === 200){
        let result = await response.json();

        localStorage.setItem("accessToken",result.result.accessToken);
        localStorage.setItem("refreshToken",result.result.refreshToken);

        location.href = "Index.html";
    }

    if(response.status === 400){
        let errorContent = await response.json();
        ErrorNotification(errorContent.Errors);
    }

    
    
}

function ErrorNotification(obj){
    let message = "";
    for(let key in obj){
        message+= `${key} - ${obj[key][0]}`
    }

    alert(message)
}

