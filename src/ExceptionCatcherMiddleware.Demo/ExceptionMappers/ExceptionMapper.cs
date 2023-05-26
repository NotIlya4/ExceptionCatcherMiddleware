using ExceptionCatcherMiddleware.Core.DefaultMappers;
using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.Demo.ExceptionMappers;

public class ExceptionMapper : IExceptionMapper<Exception>
{
    public BadResponse Map(Exception exception)
    {
        return BadResponse.FromObject(
            statusCode: 500,
            responseDto: new
            {
                Message = "Internal exception"
            });
    }
}