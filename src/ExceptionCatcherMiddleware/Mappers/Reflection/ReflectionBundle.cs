using System.Reflection;
using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;
using ExceptionCatcherMiddleware.Mappers.Exceptions;
using Exception = System.Exception;

namespace ExceptionCatcherMiddleware.Mappers.Reflection;

internal class ReflectionBundle : IReflectionBundle
{
    public Type MapperType { get; }
    public Type ExceptionTypeThatMapperMaps { get; }
    public Func<object, Exception, BadResponse> CompiledMapperMethod => _lazyCompiledMapperMethodProvider.Value;

    private readonly Lazy<Func<object, Exception, BadResponse>> _lazyCompiledMapperMethodProvider; 

    public ReflectionBundle(Type mapperType)
    {
        // Ensure that type has method Map
        MethodInfo mapMethod = mapperType.GetMethod(nameof(IExceptionMapper<Exception>.Map))
                               ?? throw new TypeValidationException(mapperType, $"Map method not found");
        
        // Ensure that return type is BadResponse
        if (mapMethod.ReturnType != typeof(BadResponse))
        {
            throw new TypeValidationException(mapperType, $"Map method have to return BadResponse");
        } 

        // Ensure that type has only 1 param
        if (mapMethod.GetParameters().Length != 1)
        {
            throw new TypeValidationException(mapperType, $"Map method have to contain only one parameter that assignable to Exception");
        }
        
        Type paramterType = mapMethod.GetParameters()[0].ParameterType;
        
        // Ensure that param is assignable to Exception
        if (!paramterType.IsAssignableTo(typeof(Exception)))
        {
            throw new TypeValidationException(mapperType, $"Exception type must be assignable to Exception");
        }

        MapperType = mapperType;
        ExceptionTypeThatMapperMaps = paramterType;
        _lazyCompiledMapperMethodProvider = new Lazy<Func<object, Exception, BadResponse>>(
            () => MapperMethodCompiler.CompileMapperMethod(mapperType), true);
    }
}