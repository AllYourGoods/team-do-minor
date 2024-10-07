using AllYourGoods.Api.Extensions;

namespace AllYourGoods.Api.Middleware;

public class EnableRequestBodyLoggingMiddleware
{
    public string ConfigurationKey = "EnableRequestBodyLogging";

    private readonly RequestDelegate _next;

    public EnableRequestBodyLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IConfiguration configuration, ILogger<EnableRequestBodyLoggingMiddleware> logger)
    {
        if (configuration.GetValue(ConfigurationKey, defaultValue: true))
        {
            var body = await context.GetBodyString();

            logger.LogInformation(LogEvents.RequestBodyLogged,
                "Request: {Request},\n\nBody: {Body}",
                context.GetRequestString(), body == string.Empty ? "Empty" : body
            );
        }

        await _next(context);
    }
}