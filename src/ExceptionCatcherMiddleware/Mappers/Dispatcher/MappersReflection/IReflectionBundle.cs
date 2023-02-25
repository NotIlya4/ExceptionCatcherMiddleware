using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;

namespace ExceptionCatcherMiddleware.Mappers.Dispatcher.MappersReflection;

internal interface IReflectionBundle
{
    Type MapperType { get; }
    Type ExceptionTypeThatMapperMaps { get; }
    Func<object, Exception, BadResponse> CompiledMapperMethod { get; }
}