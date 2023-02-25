namespace ExceptionCatcherMiddleware.Mappers.Exceptions;

internal class TypeValidationException : ExceptionCatcherMiddlewareException
{
    public TypeValidationException(Type typeThatFailedValidation, string msg) : base($"Type validation failed in {typeThatFailedValidation.FullName} due to: {msg}")
    {
        
    }
}