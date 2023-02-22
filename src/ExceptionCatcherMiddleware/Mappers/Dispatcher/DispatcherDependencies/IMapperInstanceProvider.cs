namespace ExceptionCatcherMiddleware.Mappers.Dispatcher.DispatcherDependencies;

internal interface IMapperInstanceProvider
{
    public object GetMapperInstanceByType(Type mapperInstanceType);
}