using ExceptionCatcherMiddleware.Core.DefaultMappers;
using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.Demo.ExceptionMappers;

public class ArgumentExceptionMapper : IExceptionMapper<ArgumentException>
{
    public BadResponse Map(ArgumentException exception)
    {
        return BadResponse.FromObject(
            statusCode: 400,
            responseDto: new
            {
                Title = "Argument Exception occurred during execution",
                Detail = exception.Message
            });
    }
}