using Microsoft.AspNetCore.Mvc;

namespace PowerMessenger.WebApi.Controllers;

public class BaseController: ControllerBase
{
    protected void SetCookie(string key, string value, TimeSpan time)
    {
        HttpContext.Response.Cookies.Append(key,value,new CookieOptions
        {
            MaxAge = time
        });
    }
    
}