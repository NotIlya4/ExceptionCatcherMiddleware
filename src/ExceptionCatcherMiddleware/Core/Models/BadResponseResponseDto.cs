namespace ExceptionCatcherMiddleware.Core.Models;

internal record BadResponseResponseDto
{
    public int StatusCode { get; }
    public object ResponseDto { get; }

    public BadResponseResponseDto(int statusCode, object responseDto)
    {
        StatusCode = statusCode;
        ResponseDto = responseDto;
    }
}