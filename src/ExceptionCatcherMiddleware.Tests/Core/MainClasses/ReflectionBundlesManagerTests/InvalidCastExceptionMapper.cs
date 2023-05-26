using ExceptionCatcherMiddleware.Core.DefaultMappers;
using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.UnitTests.Core.MainClasses.ReflectionBundlesManagerTests;

public class InvalidCastExceptionMapper : IExceptionMapper<InvalidCastException>
{
    public BadResponse Map(InvalidCastException exception)
    {
        return BadResponse.FromObject(
            statusCode: 400,
            responseDto: new
            {
                Title = "Invalid cast exception",
                Detail = exception.Message
            });
    }
}