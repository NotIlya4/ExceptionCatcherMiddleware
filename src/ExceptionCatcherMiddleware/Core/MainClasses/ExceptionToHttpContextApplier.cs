using ExceptionCatcherMiddleware.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionCatcherMiddleware.Core.MainClasses;

internal class ExceptionToHttpContextApplier
{
    private readonly GlobalExceptionMapper _globalExceptionMapper;
    
    public ExceptionToHttpContextApplier(GlobalExceptionMapper globalExceptionMapper)
    {
        _globalExceptionMapper = globalExceptionMapper;
    }

    public async Task ApplyToHttpContext(HttpContext context, Exception exception)
    {
        BadResponse badResponse = _globalExceptionMapper.Map(exception);
        await badResponse.Switch(
            responseDtoCase: async (BadResponseResponseDto badResponseResponseDto) =>
            {
                ActionContext actionContext = new();
                actionContext.HttpContext = context;

                ObjectResult objectResult = new(badResponseResponseDto.ResponseDto);
                objectResult.StatusCode = badResponseResponseDto.StatusCode;
                await objectResult.ExecuteResultAsync(actionContext);
            },
            rawResponseCase: async (BadResponseRawResponse badResponseRawResponse) =>
            {
                context.Response.StatusCode = badResponseRawResponse.StatusCode;
                context.Response.ContentType = badResponseRawResponse.ContentType;
                if (badResponseRawResponse.Encoding is null)
                {
                    await context.Response.WriteAsync(badResponseRawResponse.RawResponse);
                }
                else
                {
                    await context.Response.WriteAsync(badResponseRawResponse.RawResponse, badResponseRawResponse.Encoding);
                }
            });
    }
}