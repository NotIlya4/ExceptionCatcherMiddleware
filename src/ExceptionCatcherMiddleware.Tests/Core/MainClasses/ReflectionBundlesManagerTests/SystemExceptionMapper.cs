using ExceptionCatcherMiddleware.Core.DefaultMappers;
using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.UnitTests.Core.MainClasses.ReflectionBundlesManagerTests;

public class SystemExceptionMapper : IExceptionMapper<SystemException>
{
    public BadResponse Map(SystemException exception)
    {
        return BadResponse.FromObject(
            statusCode: 500,
            responseDto: new
            {
                Title = "System exception",
                Detail = exception.Message
            });
    }
}