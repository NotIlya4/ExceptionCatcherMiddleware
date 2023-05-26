namespace ExceptionCatcherMiddleware.Api;

public interface IExceptionMiddlewareOptionsBuilder
{
    public void RegisterExceptionMapper<TMapper>();
}