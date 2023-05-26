using ExceptionCatcherMiddleware.Core.DefaultMappers;
using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.UnitTests.Core.ReflectionBundlesManager;

public class InvalidCastExceptionMapper : IExceptionMapper<InvalidCastException>
{
    public BadResponse Map(InvalidCastException exception)
    {
        return BadResponse.FromObject(
            statusCode: 400,
            responseDto: new object());
    }
}