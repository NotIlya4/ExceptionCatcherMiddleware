using ExceptionCatcherMiddleware.Core.DefaultMappers;
using ExceptionCatcherMiddleware.Core.MainClasses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ExceptionCatcherMiddleware.Api;

public static class DiExtensions
{
    public static void AddExceptionCatcherMiddlewareServices(this IServiceCollection services,
        Action<IExceptionMiddlewareOptionsBuilder>? action = null)
    {
        var manager = new ReflectionBundlesManager();
        manager.Set<ExceptionMapper>();
        services.AddScoped<ExceptionMapper>();
        
        var options = new ExceptionMiddlewareOptions(manager, services);
        if (action is not null)
        {
            action(options);
        }
        manager = options.ReflectionBundlesManager;

        services.AddScoped(serviceProvider =>
        {
            var globalExceptionMapper = new GlobalExceptionMapper(new MapperInstanceProvider(serviceProvider), manager);
            var exceptionToHttpContextApplier = new ExceptionToHttpContextApplier(globalExceptionMapper);

            return new ExceptionCatcherMiddleware(
                serviceProvider.GetRequiredService<ILogger<ExceptionCatcherMiddleware>>(),
                exceptionToHttpContextApplier);
        });
    }
}
