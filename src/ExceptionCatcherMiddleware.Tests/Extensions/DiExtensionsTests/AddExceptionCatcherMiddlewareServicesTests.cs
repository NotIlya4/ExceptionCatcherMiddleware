using ExceptionCatcherMiddleware.Extensions;
using ExceptionCatcherMiddleware.Mappers.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace ExceptionCatcherMiddleware.UnitTests.Extensions.DiExtensions;

public class AddExceptionCatcherMiddlewareServicesTests
{
    [SetUp]
    public void SetUp()
    {
        
    }

    [Test]
    public void WhenCalled_AllDependenciesMustBeAdded()
    {
        ServiceCollection serviceCollection = new();
        serviceCollection.AddLogging();
        serviceCollection.AddExceptionCatcherMiddlewareServices(optionsBuilder =>
        {
        });
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        var instanceProviderService = serviceProvider.GetRequiredService<IMapperInstanceProvider>();
        var bundleProviderService = serviceProvider.GetRequiredService<IReflectionBundlesProvider>();
        var dispatcherService = serviceProvider.GetRequiredService<MappersDispatcher>();
        var middlewareService = serviceProvider.GetRequiredService<Middleware.ExceptionCatcherMiddleware>();
    }

    [Test]
    public void UserAddsMapper_ItMustBeRegisteredInUserServiceCollection()
    {
        ServiceCollection serviceCollection = new();
        serviceCollection.AddExceptionCatcherMiddlewareServices(optionsBuilder =>
        {
            optionsBuilder.RegisterExceptionMapper<ArgumentException, ArgumentExceptionMapper>();
        });
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        var userMapper = serviceProvider.GetRequiredService<ArgumentExceptionMapper>();
    }
}