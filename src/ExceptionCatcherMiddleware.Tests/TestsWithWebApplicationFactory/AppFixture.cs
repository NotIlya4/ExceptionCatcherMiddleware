using Microsoft.AspNetCore.Mvc.Testing;

namespace ExceptionCatcherMiddleware.UnitTests.TestsWithWebApplicationFactory;

[CollectionDefinition(nameof(AppFixture))]
public class AppFixture : ICollectionFixture<AppFixture>
{
    internal WebApplicationFactory<Program> Factory { get; }
    public Client Client { get; }
    
    public AppFixture()
    {
        Factory = new WebApplicationFactory<Program>();
        Client = new Client(Factory.CreateClient());
    }
}