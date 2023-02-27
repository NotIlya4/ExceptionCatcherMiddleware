# ExceptionCatcherMiddleware
This is a small package that provides a simple way to catch exceptions in middleware and map them to the appropriate status code and DTO that will be sent to the client. It integrated with ASP.NET content negotiation, so you don't need to worry about serializing the DTO.
## Instalation
ExceptionCatcherMiddleware is [available on NuGet](https://www.nuget.org/packages/ExceptionCatcherMiddleware) and can be installed via the below commands:
```
$ Install-Package ExceptionCatcherMiddleware
```
or via the .NET Core CLI:

```
$ dotnet add package ExceptionCatcherMiddleware
```
## Getting started
To add your own mapper you need to create class that implements `IExceptionMapper<T>` where T represents the type of exception that the mapper will map.
Example for ArgumentException:
```csharp
using ExceptionCatcherMiddleware.Mappers.CreatingCustomMappers;

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
```
Exception mappers should map from `TException` to `BadResponse`. `BadResponse` class is utilized by the middleware. 
* `StatusCode` will be applied to response's status code.
* `ResponseDto` will be used as the response body. Since the package is connected to ASP.NET content negotiation, the framework will handle the serialization of the DTO.

### Adding mapper to middleware
```csharp
builder.Services.AddExceptionCatcherMiddlewareServices(optionsBuilder =>
{
    optionsBuilder.RegisterExceptionMapper<ArgumentException, ArgumentExceptionMapper>();
});
```
This method has the following signature `RegisterExceptionMapper<TException, TMapper>`.
* `TException` is a type of exception that mapper will map.
* `TMapper` is a type of mapper for that exception

Note: `TMapper` must be bound strictly to `TException`. For instance, if your mapper implements `IExceptionMapper<ArgumentException>`, you can register it only for `ArgumentException` and not for `ArgumentOutOfRangeException`

### Adding Middelware to pipeline
```csharp
app.UseExceptionCatcherMiddleware();
```
That's it! Now, any `ArgumentException` and its derived exceptions, such as _ArgumentOutOfRangeException_, _ArgumentNullException_, and so on, will be mapped by the registered mapper. If an exception occurs that doesn't inherit from `ArgumentException`, it will be mapped by the default mapper for `Exception`. You can override this by registering your own `IExceptionMapper<Exception>`.
