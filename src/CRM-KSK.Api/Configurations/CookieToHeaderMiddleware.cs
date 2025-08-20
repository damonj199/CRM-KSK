namespace CRM_KSK.Api.Configurations;

public class CookieToHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public CookieToHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("jwt", out var token))
        {
            context.Request.Headers.Append("Authorization", $"Bearer {token}");
        }
        await _next(context);
    }
}
