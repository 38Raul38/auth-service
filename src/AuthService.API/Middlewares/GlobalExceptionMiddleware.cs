using System.Net;
using AuthService.Core.Exceptions;

namespace AuthService.API.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            await HandleExceptionAsync(context, e);
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        int statusCode;
        string code;

        if (exception is AuthServiceExceptions authEx)
        {
            code = authEx.Types.ToString();
            statusCode = authEx.Types switch
            {
                AuthServiceExceptionTypes.INVALID_PASSWORD => StatusCodes.Status401Unauthorized,
                AuthServiceExceptionTypes.USER_NOT_FOUND => StatusCodes.Status404NotFound,
                AuthServiceExceptionTypes.TOKEN_EXPIRED => StatusCodes.Status401Unauthorized,
                AuthServiceExceptionTypes.TOKEN_INVALID => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };
        }
        else
        {
            code = "INTERNAL_SERVER_ERROR";
            statusCode = StatusCodes.Status500InternalServerError;
        }

        context.Response.StatusCode = statusCode;

        var response = new { status = statusCode, code };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}
