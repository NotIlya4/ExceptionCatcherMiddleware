namespace ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;

public interface IExceptionMapper<in T>
{
    public BadResponse Map(T exception);
}