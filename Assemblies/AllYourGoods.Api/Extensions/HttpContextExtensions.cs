using Newtonsoft.Json;

namespace AllYourGoods.Api.Extensions;

public static class HttpContextExtensions
{
    public static string GetRequestString(this HttpContext context) =>
        JsonConvert.SerializeObject(new { context.Request.Method, context.Request.Host, context.Request.Path, context.Request.Protocol, context.Request.Headers, context.Request.Query, context.Request.Cookies }, Newtonsoft.Json.Formatting.Indented);

    public static async Task<string> GetBodyString(this HttpContext context)
    {
        using var sr = new StreamReader(context.Request.Body, leaveOpen: true);
        // Reset the request body stream to be sure we start reading from the beginning
        context.Request.Body.Position = 0;
        var body = await sr.ReadToEndAsync();
        // Reset the request body stream for further processing
        context.Request.Body.Position = 0;
        return body;
    }
}