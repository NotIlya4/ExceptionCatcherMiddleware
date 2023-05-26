using ExceptionCatcherMiddleware.Core.Models;

namespace ExceptionCatcherMiddleware.UnitTests.Core.Models;

public class BadResponseTests
{
    private readonly object _responseDto = new {Title = "aaa"};
    private readonly BadResponseRawResponse _rawResponse = new BadResponseRawResponse(401, "aaa", "bbb");
    
    [Fact]
    public async Task Switch_FromObject_SwitchResponseDtoCase()
    {
        var badResponse = BadResponse.FromObject(400, _responseDto);
        bool flag = false;

        await badResponse.Switch(dto =>
        {
            Assert.Equal(new BadResponseResponseDto(400, _responseDto), dto);
            flag = true;
            return Task.CompletedTask;
        }, a => throw new Exception());
        
        Assert.True(flag);
    }

    [Fact]
    public async Task Switch_FromRaw_SwitchRawResponseCase()
    {
        var badResponse = BadResponse.FromRaw(401, "aaa", "bbb");
        bool flag = false;

        await badResponse.Switch(dto => throw new Exception(),
            dto =>
            {
                flag = true;
                Assert.Equal(_rawResponse, dto);
                return Task.CompletedTask;
            });
        
        Assert.True(flag);
    }
}