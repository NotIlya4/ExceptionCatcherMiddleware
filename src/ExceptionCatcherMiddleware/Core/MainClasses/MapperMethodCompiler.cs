using System.Linq.Expressions;
using System.Reflection;
using ExceptionCatcherMiddleware.Core.DefaultMappers;
using ExceptionCatcherMiddleware.Core.Exceptions;
using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.Core.MainClasses;

internal class MapperMethodCompiler
{
    public static Func<object, Exception, BadResponse> CompileMapperMethod(Type mapperType)
    {
        MethodInfo methodInfo = mapperType.GetMethod(nameof(IExceptionMapper<Exception>.Map)) 
                                ?? throw new TypeValidationException(mapperType, $"Map method not found");;
        
        Type argumentExceptionType = methodInfo.GetParameters()[0].ParameterType;
        
        ParameterExpression thisArgument = Expression.Parameter(typeof(object), "this");
        ParameterExpression passedException = Expression.Parameter(typeof(Exception), "arguments");

        MethodCallExpression call = Expression.Call(
            Expression.Convert(thisArgument, mapperType),
            methodInfo,
            Expression.Convert(passedException, argumentExceptionType));

        Expression<Func<object, Exception, BadResponse>> delegateExpression = Expression.Lambda<Func<object, Exception, BadResponse>>(
            call,
            thisArgument,
            passedException);

        return delegateExpression.Compile();
    }
}