using System.Reflection;
using ExceptionCatcherMiddleware.Core.DefaultMappers;
using ExceptionCatcherMiddleware.Core.Exceptions;
using ExceptionCatcherMiddleware.Core.MainClasses;
using Exception = System.Exception;

namespace ExceptionCatcherMiddleware.Core.Models;

internal class ReflectionBundle
{
    public Type MapperType { get; }
    public Type ExceptionType { get; }
    public Func<object, Exception, BadResponse> CompiledMapperMethod { get; }

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
        ExceptionType = paramterType;
        CompiledMapperMethod = MapperMethodCompiler.CompileMapperMethod(mapperType);
    }
}