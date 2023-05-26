using ExceptionCatcherMiddleware.Core.Exceptions;
using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.Core.MainClasses;

internal class ReflectionBundlesManager
{
    private readonly Dictionary<Type, ReflectionBundle> _exceptionToItsMapper = new();

    public void Set(ReflectionBundle reflectionBundle)
    {
        _exceptionToItsMapper[reflectionBundle.ExceptionType] = reflectionBundle;
    }

    public void Set(Type exceptionMapperType)
    {
        Set(new ReflectionBundle(exceptionMapperType));
    }

    public void Set<TExceptionMapper>()
    {
        Set(typeof(TExceptionMapper));
    }

    public ReflectionBundle Get(Type exceptionType)
    {
        return _exceptionToItsMapper[FindTypeInDictKeyThatMostCloseToProvided(_exceptionToItsMapper, exceptionType)];
    }

    public ReflectionBundle Get<TException>()
    {
        return Get(typeof(TException));
    }

    public static Type FindTypeInDictKeyThatMostCloseToProvided<T>(Dictionary<Type, T> exceptionDictionary, Type exceptionType)
    {
        Type? parentExceptionType = exceptionType;
        while (parentExceptionType is not null)
        {
            var containsKey = exceptionDictionary.ContainsKey(parentExceptionType);;
            if (containsKey)
            {
                return parentExceptionType;
            }
            parentExceptionType = parentExceptionType.BaseType;
        }

        throw new ExceptionCatcherMiddlewareException("There are no exception types that satisfies any parent type");
    }
}