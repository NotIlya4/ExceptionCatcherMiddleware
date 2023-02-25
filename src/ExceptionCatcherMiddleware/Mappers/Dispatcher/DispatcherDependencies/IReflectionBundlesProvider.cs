using ExceptionCatcherMiddleware.Mappers.Dispatcher.MappersReflection;

namespace ExceptionCatcherMiddleware.Mappers.Dispatcher.DispatcherDependencies;

internal interface IReflectionBundlesProvider
{
    public IReflectionBundle? Get(Type exceptionType);
    public IReflectionBundle? GetByFirstAvailableParent(Type? exceptionType);
}