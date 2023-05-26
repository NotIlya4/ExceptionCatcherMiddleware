using Newtonsoft.Json.Linq;

namespace ExceptionCatcherMiddleware.UnitTests.TestsWithWebApplicationFactory;

[Collection(nameof(AppFixture))]
public class EndTests
{
    private readonly Client _client;
    
    public EndTests(AppFixture fixture)
    {
        _client = fixture.Client;
    }

    [Fact]
    public async Task Get_Exception_MappedException()
    {
        HttpResponseMessage response = await _client.Exception("asd");
        
        Assert.Equal(500, (int)response.StatusCode);
        Assert.Equal(new JObject() { ["Title"] = "Internal exception", ["Detail"] = "asd" },
            JObject.Parse(await response.Content.ReadAsStringAsync()));
    }

    [Fact]
    public async Task Get_ArgumentException_MapperExceptionToResponse()
    {
        HttpResponseMessage response = await _client.ArgumentException("asdd");
        
        Assert.Equal(400, (int)response.StatusCode);
        Assert.Equal(new JObject() { ["Title"] = "Argument Exception occurred during execution", ["Detail"] = "asdd" },
            JObject.Parse(await response.Content.ReadAsStringAsync()));
    }
}