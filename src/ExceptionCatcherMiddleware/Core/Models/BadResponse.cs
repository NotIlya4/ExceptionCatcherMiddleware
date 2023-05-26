using System.Text;
using ExceptionCatcherMiddleware.Core.Exceptions;
using Newtonsoft.Json.Linq;

namespace ExceptionCatcherMiddleware.Core.Models;

public record BadResponse
{
    private readonly BadResponseResponseDto? _badResponseResponseDto;
    private readonly BadResponseRawResponse? _badResponseRawResponse;
    public bool IsRaw { get; }

    private BadResponse(BadResponseResponseDto badResponseResponseDto)
    {
        _badResponseResponseDto = badResponseResponseDto;
        _badResponseRawResponse = null;
        IsRaw = false;
    }
    
    private BadResponse(BadResponseRawResponse badResponseRawResponse)
    {
        _badResponseResponseDto = null;
        _badResponseRawResponse = badResponseRawResponse;
        IsRaw = false;
    }

    public static BadResponse FromObject(int statusCode, object responseDto)
    {
        return new BadResponse(new BadResponseResponseDto(statusCode: statusCode, responseDto: responseDto));
    }

    public static BadResponse FromRaw(int statusCode, string rawResponse, string contentType, Encoding? encoding = null)
    {
        return new BadResponse(new BadResponseRawResponse(
            statusCode: statusCode,
            rawResponse: rawResponse,
            encoding: encoding,
            contentType: contentType));
    }

    public static BadResponse FromJson(int statusCode, string jsonResponse)
    {
        return FromRaw(
            statusCode: statusCode,
            encoding: null,
            rawResponse: jsonResponse,
            contentType: "application/json");
    }
    
    public static BadResponse FromJson(int statusCode, JObject jObject)
    {
        return FromJson(
            statusCode: statusCode,
            jsonResponse: jObject.ToString());
    }

    internal async Task Switch(Func<BadResponseResponseDto, Task> responseDtoCase,
        Func<BadResponseRawResponse, Task> rawResponseCase)
    {
        if (!IsRaw)
        {
            if (_badResponseResponseDto is null)
            {
                throw SomethingWentWrong(nameof(_badResponseResponseDto));
            }
            await responseDtoCase(_badResponseResponseDto);
        }
        else
        {
            if (_badResponseRawResponse is null)
            {
                throw SomethingWentWrong(nameof(_badResponseRawResponse));
            }
            await rawResponseCase(_badResponseRawResponse);
        }
    }

    private ExceptionCatcherMiddlewareException SomethingWentWrong(string property)
    {
        return new ExceptionCatcherMiddlewareException($"Something went wrong because {property} must be not null");
    }
}