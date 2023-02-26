using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;

namespace ExceptionCatcherMiddleware.Mappers.Reflection;

internal interface IReflectionBundle
{
    Type MapperType { get; }
    Type ExceptionTypeThatMapperMaps { get; }
    Func<object, Exception, BadResponse> CompiledMapperMethod { get; }
}