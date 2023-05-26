using ExceptionCatcherMiddleware.Core.MainClasses;
using ExceptionCatcherMiddleware.Core.Models;
using ExceptionCatcherMiddleware.UnitTests.Core.MainClasses.ReflectionBundlesManagerTests;

namespace ExceptionCatcherMiddleware.UnitTests.Core.MainClasses;

public class MapperMethodCompilerTests
{
    private readonly InvalidCastExceptionMapper _invalidCastExceptionMapper = new();
    private readonly Func<object, Exception, BadResponse> _invalidCastExceptionMapperCompiledMethod;
    private readonly InvalidCastException _invalidCastException = new("That was an invalid cast");
    
    private readonly SystemExceptionMapper _systemExceptionMapper = new();
    private readonly Func<object, Exception, BadResponse> _systemExceptionMapperCompiledMethod;
    private readonly SystemException _systemException = new("That was a system exception");

    public MapperMethodCompilerTests()
    {
        _invalidCastExceptionMapperCompiledMethod = MapperMethodCompiler.CompileMapperMethod(typeof(InvalidCastExceptionMapper));
        _systemExceptionMapperCompiledMethod = MapperMethodCompiler.CompileMapperMethod(typeof(SystemExceptionMapper));
    }
    
    [Fact]
    public void CompileMapperMethod_InvalidCastExceptionAndInvalidCastExceptionMapper_ResponseThatInvalidCastExceptionMapperReturn()
    {
        BadResponse result = _invalidCastExceptionMapperCompiledMethod(_invalidCastExceptionMapper, _invalidCastException);
        
        Assert.Equal(_invalidCastExceptionMapper.Map(_invalidCastException), result);
    }

    [Fact]
    public void CompileMapperMethod_SystemExceptionAndSystemExceptionMapper_ResponseThatSystemExceptionMapperReturn()
    {
        BadResponse result = _systemExceptionMapperCompiledMethod(_systemExceptionMapper, _systemException);
        
        Assert.Equal(_systemExceptionMapper.Map(_systemException), result);
    }
}