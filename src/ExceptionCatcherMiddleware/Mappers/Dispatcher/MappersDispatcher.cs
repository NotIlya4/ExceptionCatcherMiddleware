using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;
using ExceptionCatcherMiddleware.Mappers.Exceptions;
using ExceptionCatcherMiddleware.Mappers.Reflection;

namespace ExceptionCatcherMiddleware.Mappers.Dispatcher;

internal class MappersDispatcher
{
    private readonly IMapperInstanceProvider _mapperInstanceProvider;
    private readonly IReflectionBundlesProvider _reflectionBundlesProvider;

    public MappersDispatcher(IMapperInstanceProvider mapperInstanceProvider,
        IReflectionBundlesProvider reflectionBundlesProvider)
    {
        _mapperInstanceProvider = mapperInstanceProvider;
        _reflectionBundlesProvider = reflectionBundlesProvider;
    }
    
    public BadResponse Map(Exception exception)
    {
        IReflectionBundle reflectionBundle = 
            _reflectionBundlesProvider.GetByFirstAvailableParent(exception.GetType());
        
        object? mapperInstance = _mapperInstanceProvider.GetMapperInstanceByType(reflectionBundle.MapperType);
        
        if (mapperInstance is null)
        {
            throw new MappingException(
                $"Reflection bundle for {reflectionBundle.ExceptionTypeThatMapperMaps
                    .FullName} exists but mapper instance for {reflectionBundle.MapperType.FullName} doesn't");
        }
        
        Func<object, Exception, BadResponse> compiledMapMethod = reflectionBundle.CompiledMapperMethod;
        
        return compiledMapMethod(mapperInstance, exception);
    }
}