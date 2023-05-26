namespace ExceptionCatcherMiddleware.Core.Exceptions;

public class ExceptionCatcherMiddlewareException : Exception
{
    public ExceptionCatcherMiddlewareException()
    {
        
    }
    
    public ExceptionCatcherMiddlewareException(string msg) : base(msg)
    {
        
    }
}