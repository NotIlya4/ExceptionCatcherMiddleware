namespace ExceptionCatcherMiddleware.Mappers.Exceptions;

public class MapperNotProvidedException : ExceptionCatcherMiddlewareException
{
    public Type ExceptionType { get; }
    
    public MapperNotProvidedException(Type exceptionType) : base($"Mapper not found for {exceptionType.FullName} and all its parents")
    {
        ExceptionType = exceptionType;
    }
}