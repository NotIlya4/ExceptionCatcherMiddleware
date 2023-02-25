using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;
using ExceptionCatcherMiddleware.Mappers.Dispatcher.DispatcherDependencies;
using ExceptionCatcherMiddleware.Mappers.Dispatcher.MappersReflection;
using ExceptionCatcherMiddleware.Mappers.Exceptions;

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
        IReflectionBundle? reflectionBundle = 
            _reflectionBundlesProvider.GetByFirstAvailableParent(exception.GetType());

        if (reflectionBundle is null)
        {
            throw new MapperNotProvidedException(exception.GetType());
        }
        
        object mapperInstance = _mapperInstanceProvider.GetMapperInstanceByType(reflectionBundle.MapperType);
        Func<object, Exception, BadResponse> compiledMapMethod = reflectionBundle.CompiledMapperMethod;
        
        return compiledMapMethod(mapperInstance, exception);
    }
}