using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;

namespace ExceptionCatcherMiddleware.Demo.ExceptionMappers;

public class ArgumentExceptionMapper : IExceptionMapper<ArgumentException>
{
    public BadResponse Map(ArgumentException exception)
    {
        return new BadResponse()
        {
            StatusCode = 400,
            ResponseDto = new
            {
                Title = "Argument Exception occurred during execution",
                Detail = exception.Message
            }
        };
    }
}