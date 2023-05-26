using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.Core.DefaultMappers;

internal class InvalidOperationExceptionMapper : IExceptionMapper<InvalidOperationException>
{
    public BadResponse Map(InvalidOperationException exception)
    {
        return BadResponse.FromObject(
            statusCode: 400,
            responseDto: new
            {
                Title = "Invalid operation exception",
                Detail = exception.Message
            });
    }
}