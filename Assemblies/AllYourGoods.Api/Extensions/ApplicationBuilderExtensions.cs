using AllYourGoods.Api.Middleware;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace AllYourGoods.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    private static List<Type>? _excludedControllers;

    public static IApplicationBuilder EnableBuffering(this IApplicationBuilder builder) => builder.UseMiddleware<EnableBufferingMiddleware>();

    /// <summary>
    /// NOTE: You need to call this middleware method AFTER IApplicationBuilder.UseRouting AND BEFORE IApplicationBuilder.UseEndpoints,
    /// Also, request buffering needs to be enabled for this functionality to work! Use IApplicationBuilder.EnableBuffering()
    /// </summary>
    /// <param name="builder">Your application's IApplicationBuilder</param>
    /// <param name="exclude">List of controller types to exclude from body logging</param>
    /// <returns>IApplicationBuilder</returns>
    public static IApplicationBuilder EnableRequestBodyLoggingMiddleware(this IApplicationBuilder builder, List<Type>? exclude = null)
    {
        _excludedControllers = exclude;
        builder.UseWhen(ExcludeControllersDelegate, b => b.UseMiddleware<EnableRequestBodyLoggingMiddleware>());

        return builder;
    }

    private static bool ExcludeControllersDelegate(HttpContext context)
    {
        if (_excludedControllers is null)
        {
            return true;
        }

        var controllerActionDescriptor = context.GetEndpoint()?.Metadata.GetMetadata<ControllerActionDescriptor>();
        var controllerType = controllerActionDescriptor?.ControllerTypeInfo.AsType();

        return controllerType is null || !_excludedControllers.Contains(controllerType);
    }
}