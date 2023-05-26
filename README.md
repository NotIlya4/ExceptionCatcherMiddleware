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
To add your own mapper you need to create class that implements `IExceptionMapper<T>` where `T` represents the type of exception that the mapper will map.
Example for `ArgumentException`:
```csharp
public class ArgumentExceptionMapper : IExceptionMapper<ArgumentException>
{
    public BadResponse Map(ArgumentException exception)
    {
        return BadResponse.FromObject(
            statusCode: 400,
            responseDto: new
            {
                Title = "Argument Exception occurred during execution",
                Detail = exception.Message
            });
    }
}
```
`BadResponse` has different factory methods:
- `FromObject` Use it when you want to apply ASP.NET's content negotiation.
- `FromRaw` Use it when you want to serialize response yourself without ASP.NET content negotiation.
- `FromJson` Its just a `FromRaw` but with some predefined values.

You can easily create your own factory methods using extension methods with `FromObject` or `FromRaw`.

### Adding mapper to middleware
```csharp
builder.Services.AddExceptionCatcherMiddlewareServices(optionsBuilder =>
{
    optionsBuilder.RegisterExceptionMapper<ArgumentExceptionMapper>();
});
```

Note: `TMapper` must be bound strictly to `TException`. For instance, if your mapper implements `IExceptionMapper<ArgumentException>`, you can register it only for `ArgumentException` and not for `ArgumentOutOfRangeException`

### Adding Middelware to pipeline
```csharp
app.UseExceptionCatcherMiddleware();
```
That's it! Now, any `ArgumentException` and its derived exceptions, such as _ArgumentOutOfRangeException_, _ArgumentNullException_, and so on, will be mapped by the registered mapper, I mean it works just like try catch block. If an exception occurs that doesn't inherit from `ArgumentException`, it will be mapped by the default mapper for `Exception`. You can override this by registering your own `IExceptionMapper<Exception>`.
