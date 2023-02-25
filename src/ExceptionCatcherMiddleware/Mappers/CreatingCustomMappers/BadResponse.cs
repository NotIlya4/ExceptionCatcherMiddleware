namespace ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;

public class BadResponse
{
    public required int StatusCode { get; init; }
    public required object ExceptionDto { get; init; }
}