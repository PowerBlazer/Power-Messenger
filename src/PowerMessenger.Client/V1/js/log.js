const apiUrl = "http://localhost:6001/api/v1/Authorization/"


async function Login(){
    const email = document.getElementById("email-input").value;
    const password = document.getElementById("password-input").value;

    const bodyJson = JSON.stringify({
        email:email,
        password:password
    });

    const response = await fetch(apiUrl+"Login",{
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

    if(response.status === 401){
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