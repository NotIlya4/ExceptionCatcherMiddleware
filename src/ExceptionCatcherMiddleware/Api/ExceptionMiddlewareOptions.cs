using ExceptionCatcherMiddleware.Core.MainClasses;
using ExceptionCatcherMiddleware.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ExceptionCatcherMiddleware.Api;

internal class ExceptionMiddlewareOptions : IExceptionMiddlewareOptionsBuilder
{
    private readonly IServiceCollection _services;
    public ReflectionBundlesManager ReflectionBundlesManager { get; }

    public ExceptionMiddlewareOptions(ReflectionBundlesManager manager, IServiceCollection services)
    {
        ReflectionBundlesManager = manager;
        _services = services;
    }

    public void RegisterExceptionMapper<TMapper>()
    {
        ReflectionBundlesManager.Set(new ReflectionBundle(typeof(TMapper)));
        _services.AddScoped(typeof(TMapper));
    }
}