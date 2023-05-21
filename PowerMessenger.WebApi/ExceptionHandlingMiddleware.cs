﻿using System.Net;
using System.Text.Json;
using PowerMessenger.Domain.Common;
using PowerMessenger.Domain.Exceptions;

namespace PowerMessenger.WebApi;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex.Errors);
        }
        catch (LoginNotValidException ex)
        {
            await HandleExceptionAsync(httpContext,HttpStatusCode.NotFound,ex.Errors);
        }
        catch (SessionNotFoundException ex)
        {
            await HandleExceptionAsync(httpContext,HttpStatusCode.NotFound,ex.Error);
        }
        catch (SessionCodeNotValidException ex)
        {
            

            await HandleExceptionAsync(httpContext,HttpStatusCode.BadRequest,ex.Error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            
            await HandleExceptionAsync(httpContext,HttpStatusCode.InternalServerError, "Ошибка на сервере");
        }

    }
        
    private static async Task HandleExceptionAsync(HttpContext httpContext,
        HttpStatusCode httpStatusCode, Dictionary<string, List<string>>? errors)
    {
        var response = httpContext.Response;

        response.ContentType = "application/json";
        response.StatusCode = (int)httpStatusCode;

        var errorResult = new ApiActionResult<string>
        {
            Errors = errors
        };

        var result = JsonSerializer.Serialize(errorResult);

        await response.WriteAsync(result);
    }
    
    private static async Task HandleExceptionAsync(HttpContext httpContext,
        HttpStatusCode httpStatusCode, string error)
    {
        var response = httpContext.Response;

        response.ContentType = "application/json";
        response.StatusCode = (int)httpStatusCode;

        var errorResult = new
        {
            Error = error
        };
        
        var result = JsonSerializer.Serialize(errorResult);

        await response.WriteAsync(result);
    }
    
    

    
    
}