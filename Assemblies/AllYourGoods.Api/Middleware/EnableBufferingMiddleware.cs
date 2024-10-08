namespace AllYourGoods.Api.Middleware;

public class EnableBufferingMiddleware
{
    private readonly RequestDelegate _next;

    public EnableBufferingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        httpContext.Request.EnableBuffering();

        await _next(httpContext);
    }
}