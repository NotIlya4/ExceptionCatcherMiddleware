using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;

namespace ExceptionCatcherMiddleware.Demo.ExceptionMappers;

public class ExceptionMapper : IExceptionMapper<Exception>
{
    public BadResponse Map(Exception exception)
    {
        return new BadResponse()
        {
            StatusCode = 500,
            ResponseDto = new
            {
                Message = "Internal exception"
            }
        };
    }
}