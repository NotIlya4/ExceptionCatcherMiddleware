using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;

namespace ExceptionCatcherMiddleware.Options;

public interface IExceptionMiddlewareOptionsBuilder
{
    public MapperMethodsCompilePolicy CompilePolicy { get; set; }

    public void RegisterExceptionMapper<TException, TMapper>()
        where TMapper : class, IExceptionMapper<TException>
        where TException : Exception;
}