using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;
using ExceptionCatcherMiddleware.Mappers.Reflection;

namespace ExceptionCatcherMiddleware.Options;

internal class ExceptionMiddlewareOptions : IExceptionMiddlewareOptionsBuilder
{
    public MapperMethodsCompilePolicy CompilePolicy { get; set; } = MapperMethodsCompilePolicy.LazyCompile;
    public ReflectionBundlesManager ReflectionBundlesManager { get; }

    public ExceptionMiddlewareOptions()
    {
        ReflectionBundlesManager = new(new ReflectionBundle(typeof(DefaultExceptionMapper)));
    }

    public void RegisterExceptionMapper<TException, TMapper>() 
        where TMapper : class, IExceptionMapper<TException>
        where TException : Exception
    {
        ReflectionBundlesManager.Set(new ReflectionBundle(typeof(TMapper)));
    }
}