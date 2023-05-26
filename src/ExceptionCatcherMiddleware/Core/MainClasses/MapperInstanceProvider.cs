using Microsoft.Extensions.DependencyInjection;

namespace ExceptionCatcherMiddleware.Core.MainClasses;

internal class MapperInstanceProvider
{
    private readonly IServiceProvider _services;

    public MapperInstanceProvider(IServiceProvider services)
    {
        _services = services;
    }
    
    public object GetMapperInstanceByType(Type mapperInstanceType)
    {
        return _services.GetRequiredService(mapperInstanceType);
    }
}