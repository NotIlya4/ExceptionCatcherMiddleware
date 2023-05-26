using Newtonsoft.Json.Linq;

namespace ExceptionCatcherMiddleware.UnitTests.TestsWithWebApplicationFactory;

public class Client
{
    private readonly HttpClient _client;

    public Client(HttpClient client)
    {
        _client = client;
    }

    public async Task<HttpResponseMessage> Exception(string message)
    {
        return await _client.GetAsync($"exception/message/{message}");
    }
    
    public async Task<HttpResponseMessage> ArgumentException(string message)
    {
        return await _client.GetAsync($"argumentException/message/{message}");
    }
    
    public async Task<HttpResponseMessage> ArgumentOutOfRangeException(string message)
    {
        return await _client.GetAsync($"argumentOutOfRangeException/message/{message}");
    }
}