using ExceptionCatcherMiddleware.Mappers.Dispatcher.DispatcherDependencies;
using ExceptionCatcherMiddleware.Mappers.Exceptions;

namespace ExceptionCatcherMiddleware.Mappers.Dispatcher.MappersReflection;

internal class ReflectionBundlesManager : IReflectionBundlesProvider
{
    private readonly Dictionary<Type, ReflectionBundle> _dictionary = new();

    public ReflectionBundlesManager(ReflectionBundle defaultReflectionBundle)
    {
        if (defaultReflectionBundle.ExceptionTypeThatMapperMaps != typeof(Exception))
        {
            throw new TypeValidationException(defaultReflectionBundle.GetType(),
                $"{nameof(defaultReflectionBundle)}'s exception that mapper maps must be Exception");
        }
        
        Set(defaultReflectionBundle);
    }

    public void Set(ReflectionBundle reflectionBundle)
    {
        _dictionary[reflectionBundle.ExceptionTypeThatMapperMaps] = reflectionBundle;
    }

    public ReflectionBundle? Get(Type exceptionType)
    {
        return _dictionary.GetValueOrDefault(exceptionType);
    }

    public ReflectionBundle? GetByFirstAvailableParent(Type? exceptionType)
    {
        while (exceptionType is not null)
        {
            var reflectionBundle = Get(exceptionType);
            if (reflectionBundle is not null)
            {
                return reflectionBundle;
            }
            exceptionType = exceptionType.BaseType;
        }

        return null;
    }

    public ICollection<ReflectionBundle> GetAllMapperTypes()
    {
        return _dictionary.Values;
    }

    public void CompileAllMappersMethods()
    {
        foreach (ReflectionBundle mapperType in _dictionary.Values)
        {
            var compiledMapperMethod = mapperType.CompiledMapperMethod;
        }
    }
}