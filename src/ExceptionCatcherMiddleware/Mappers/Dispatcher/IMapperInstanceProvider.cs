namespace ExceptionCatcherMiddleware.Mappers.Dispatcher;

internal interface IMapperInstanceProvider
{
    public object? GetMapperInstanceByType(Type mapperInstanceType);
}