using ExceptionCatcherMiddleware.Core.DefaultMappers;
using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.UnitTests.Core.ReflectionBundlesManager;

public class SystemExceptionMapper : IExceptionMapper<SystemException>
{
    public BadResponse Map(SystemException exception)
    {
        return BadResponse.FromObject(
            statusCode: 400,
            responseDto: new object());
    }
}