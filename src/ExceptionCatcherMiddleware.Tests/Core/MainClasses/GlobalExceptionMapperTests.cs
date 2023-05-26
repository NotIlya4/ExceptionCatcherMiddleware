using ExceptionCatcherMiddleware.Core.MainClasses;
using ExceptionCatcherMiddleware.Core.Models;
using ExceptionCatcherMiddleware.UnitTests.Core.MainClasses.ReflectionBundlesManagerTests;

namespace ExceptionCatcherMiddleware.UnitTests.Core.MainClasses;

public class GlobalExceptionMapperTests
{
    private readonly ReflectionBundlesManager _reflectionBundlesManager;
    private readonly GlobalExceptionMapper _globalExceptionMapper;
    private readonly SystemExceptionMapper _systemMapper;
    private readonly SystemException _systemException = new("This is a system exception");
    private readonly InvalidCastException _invalidCastException = new("This is an invalid");

    public GlobalExceptionMapperTests()
    {
        _reflectionBundlesManager = new ReflectionBundlesManager();
        _reflectionBundlesManager.Set<SystemExceptionMapper>();

        _systemMapper = new SystemExceptionMapper();

        var mapperInstanceProvider = new Mock<IMapperInstanceProvider>();
        mapperInstanceProvider.Setup(m => m.Get(typeof(SystemExceptionMapper))).Returns(_systemMapper);
        
        _globalExceptionMapper = new GlobalExceptionMapper(mapperInstanceProvider.Object, _reflectionBundlesManager);
    }

    [Fact]
    public void Map_InvalidCastException_BadResponseFromSystemMapper()
    {
        BadResponse result = _globalExceptionMapper.Map(_invalidCastException);
        
        Assert.Equal(_systemMapper.Map(_invalidCastException), result);
    }

    [Fact]
    public void Map_SystemException_BadResponseFromSystemMapper()
    {
        BadResponse result = _globalExceptionMapper.Map(_systemException);
        
        Assert.Equal(_systemMapper.Map(_systemException), result);
    }
}