using ExceptionCatcherMiddleware.Mappers.Dispatcher;
using ExceptionCatcherMiddleware.Mappers.Exceptions;

namespace ExceptionCatcherMiddleware.Mappers.Reflection;

internal class ReflectionBundlesManager : IReflectionBundlesProvider
{
    private readonly Dictionary<Type, IReflectionBundle> _dictionary = new();

    public ReflectionBundlesManager(IReflectionBundle defaultReflectionBundle)
    {
        if (defaultReflectionBundle.ExceptionTypeThatMapperMaps != typeof(Exception))
        {
            throw new TypeValidationException(defaultReflectionBundle.GetType(),
                $"{nameof(defaultReflectionBundle)}'s exception that mapper maps must be Exception");
        }
        
        Set(defaultReflectionBundle);
    }

    public void Set(IReflectionBundle reflectionBundle)
    {
        _dictionary[reflectionBundle.ExceptionTypeThatMapperMaps] = reflectionBundle;
    }

    public IReflectionBundle? Get(Type exceptionType)
    {
        return _dictionary.GetValueOrDefault(exceptionType);
    }

    public IReflectionBundle? GetByFirstAvailableParent(Type? exceptionType)
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

    public ICollection<IReflectionBundle> GetAllMapperTypes()
    {
        return _dictionary.Values;
    }

    public void CompileAllMappersMethods()
    {
        foreach (IReflectionBundle mapperType in _dictionary.Values)
        {
            var compiledMapperMethod = mapperType.CompiledMapperMethod;
        }
    }
}