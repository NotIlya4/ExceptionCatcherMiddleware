using Microsoft.Extensions.DependencyInjection;

namespace ExceptionCatcherMiddleware.Core.MainClasses;

internal class MapperInstanceProvider : IMapperInstanceProvider
{
    private readonly IServiceProvider _services;

    public MapperInstanceProvider(IServiceProvider services)
    {
        _services = services;
    }
    
    public object Get(Type mapperInstanceType)
    {
        return _services.GetRequiredService(mapperInstanceType);
    }
}