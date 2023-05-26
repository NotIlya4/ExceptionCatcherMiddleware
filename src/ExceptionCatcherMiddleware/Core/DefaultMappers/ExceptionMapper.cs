using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.Core.DefaultMappers;

internal class ExceptionMapper : IExceptionMapper<Exception>
{
    public BadResponse Map(Exception exception)
    {
        return BadResponse.FromObject(
            statusCode: 500,
            responseDto: new
            {
                Title = "Internal exception",
                Detail = exception.Message 
            });
    }
}