namespace ExceptionCatcherMiddleware.Mappers.Exceptions;

public class ExceptionCatcherMiddlewareException : Exception
{
    public ExceptionCatcherMiddlewareException()
    {
        
    }
    
    public ExceptionCatcherMiddlewareException(string msg) : base(msg)
    {
        
    }
}