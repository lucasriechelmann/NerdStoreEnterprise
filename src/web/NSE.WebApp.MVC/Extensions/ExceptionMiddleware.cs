using Refit;
using System.Net;

namespace NSE.WebApp.MVC.Extensions;
public class ExceptionMiddleware
{
private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CustomHttpRequestException ex)
        {
            HandleRequestExceptionAsync(context, ex.StatusCode);
        }
        catch (ValidationApiException ex)//For Refit
        {
            HandleRequestExceptionAsync(context, ex.StatusCode);
        }
        catch (ApiException ex)//For Refit
        {
            HandleRequestExceptionAsync(context, ex.StatusCode);
        }
        catch (Exception ex)
        {
            HandleExceptionAsync(context, ex);
        }
    }

    private void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
    {
        if (statusCode == HttpStatusCode.Unauthorized)
        {
            if (context.Request.Path != "/login")
            {
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }
        }

        context.Response.StatusCode = (int)statusCode;
    }

    private void HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An error has occurred!");

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }
}
