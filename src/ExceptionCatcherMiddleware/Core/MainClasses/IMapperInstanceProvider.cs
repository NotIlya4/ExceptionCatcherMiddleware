namespace ExceptionCatcherMiddleware.Core.MainClasses;

internal interface IMapperInstanceProvider
{
    object Get(Type mapperInstanceType);
}