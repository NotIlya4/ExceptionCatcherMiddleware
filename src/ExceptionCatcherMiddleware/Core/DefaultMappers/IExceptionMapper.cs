using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.Core.DefaultMappers;

public interface IExceptionMapper<in T>
{
    public BadResponse Map(T exception);
}