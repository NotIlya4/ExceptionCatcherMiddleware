using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.Core.MainClasses;

internal class GlobalExceptionMapper
{
    private readonly IMapperInstanceProvider _mapperInstanceProvider;
    private readonly ReflectionBundlesManager _reflectionBundlesManager;

    public GlobalExceptionMapper(IMapperInstanceProvider mapperInstanceProvider,
        ReflectionBundlesManager reflectionBundlesManager)
    {
        _mapperInstanceProvider = mapperInstanceProvider;
        _reflectionBundlesManager = reflectionBundlesManager;
    }
    
    public BadResponse Map(Exception exception)
    {
        ReflectionBundle reflectionBundle = GetReflectionBundle(exception.GetType());
        object mapperInstance = GetMapper(reflectionBundle);
        return reflectionBundle.CompiledMapperMethod(mapperInstance, exception);
    }

    private ReflectionBundle GetReflectionBundle(Type exceptionType)
    {
        return _reflectionBundlesManager.Get(exceptionType);
    }

    private object GetMapper(ReflectionBundle reflectionBundle)
    {
        return _mapperInstanceProvider.Get(reflectionBundle.MapperType);
    }
}