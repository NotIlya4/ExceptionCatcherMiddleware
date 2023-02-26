namespace ExceptionCatcherMiddleware.Mappers.Dispatcher;

internal class ServiceProviderWrapper : IMapperInstanceProvider
{
    private readonly IServiceProvider _serviceProvider;

    public ServiceProviderWrapper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public object? GetMapperInstanceByType(Type mapperInstanceType)
    {
        return _serviceProvider.GetService(mapperInstanceType);
    }
}