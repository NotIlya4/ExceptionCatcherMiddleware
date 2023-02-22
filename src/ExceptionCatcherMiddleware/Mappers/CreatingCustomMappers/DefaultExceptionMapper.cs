using Microsoft.AspNetCore.Http;

namespace ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;

internal class DefaultExceptionMapper : IExceptionMapper<Exception>
{
    public BadResponse Map(Exception exception)
    {
        return new BadResponse()
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            ExceptionDto = new
            {
                Title = "Internal exception",
                Detail = exception.Message
            }
        };
    }
}