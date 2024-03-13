namespace KBM.WebApp.MVC.Extensions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (CustomHttpRequestException ex)
        {
            HandleRequestExcecptionAsync(httpContext, ex);
        }
    }

    private static void HandleRequestExcecptionAsync(HttpContext httpContext, CustomHttpRequestException ex)
    {
        if(ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
            return;
        }

        httpContext.Response.StatusCode = (int)ex.StatusCode;
    }
}
