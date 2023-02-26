using ExceptionCatcherMiddleware.Mappers.Reflection;

namespace ExceptionCatcherMiddleware.Mappers.Dispatcher;

internal interface IReflectionBundlesProvider
{
    public IReflectionBundle GetByFirstAvailableParent(Type exceptionType);
}