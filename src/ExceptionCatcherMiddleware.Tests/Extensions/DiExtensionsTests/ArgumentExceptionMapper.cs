using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;

namespace ExceptionCatcherMiddleware.UnitTests.Extensions.DiExtensions;

public class ArgumentExceptionMapper : IExceptionMapper<ArgumentException>
{
    public BadResponse Map(ArgumentException exception)
    {
        return new BadResponse()
        {
            StatusCode = 400,
            ExceptionDto = new
            {
                A = "a"
            }
        };
    }
}