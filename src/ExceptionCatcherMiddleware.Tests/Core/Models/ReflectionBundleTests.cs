using ExceptionCatcherMiddleware.Core.Models;
using ExceptionCatcherMiddleware.UnitTests.Core.MainClasses.ReflectionBundlesManagerTests;

namespace ExceptionCatcherMiddleware.UnitTests.Core.Models;

public class ReflectionBundleTests
{
    [Fact]
    public void Ctor_InvalidCastExceptionMapper_ParsedMapperType()
    {
        var reflectionBundle = new ReflectionBundle(typeof(InvalidCastExceptionMapper));
        
        Assert.Equal(typeof(InvalidCastException), reflectionBundle.ExceptionType);
        Assert.Equal(typeof(InvalidCastExceptionMapper), reflectionBundle.MapperType);

        var instance = new InvalidCastExceptionMapper();
        var exception = new InvalidCastException("Aaa");
        Assert.Equal(instance.Map(exception), reflectionBundle.CompiledMapperMethod(instance, exception));
    }
}