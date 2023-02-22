using ExceptionCatcherMiddleware.Mappers.Dispatcher.MappersReflection;

namespace ExceptionCatcherMiddleware.Mappers.Dispatcher.DispatcherDependencies;

internal interface IReflectionBundlesProvider
{
    public ReflectionBundle? Get(Type exceptionType);

    public ReflectionBundle GetDefault();
}