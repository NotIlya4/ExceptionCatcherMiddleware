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

    public IReflectionBundle GetByFirstAvailableParent(Type exceptionType)
    {
        if (!exceptionType.IsAssignableTo(typeof(Exception)))
        {
            throw new TypeValidationException(exceptionType, $"Type must be inherited from Exception");
        }

        Type? parentExceptionType = exceptionType;
        
        while (parentExceptionType is not null)
        {
            var reflectionBundle = Get(parentExceptionType);
            if (reflectionBundle is not null)
            {
                return reflectionBundle;
            }
            parentExceptionType = parentExceptionType.BaseType;
        }

        throw new ExceptionCatcherMiddlewareException
            ("Something went wrong because bundle manager have to contain bundle at least for Exception but it doesn't");
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