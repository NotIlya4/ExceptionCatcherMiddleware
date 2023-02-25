namespace ExceptionCatcherMiddleware.Mappers.Exceptions;

public class MappingException : ExceptionCatcherMiddlewareException
{
    public MappingException(string msg) : base(msg)
    {
        
    }
}