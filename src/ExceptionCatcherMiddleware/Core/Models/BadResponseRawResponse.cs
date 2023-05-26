using System.Text;

namespace ExceptionCatcherMiddleware.Core.Models;

internal record BadResponseRawResponse
{
    public int StatusCode { get; }
    public string RawResponse { get; }
    public string ContentType { get; }
    public Encoding? Encoding { get; }

    public BadResponseRawResponse(int statusCode, string rawResponse, string contentType, Encoding? encoding = null)
    {
        StatusCode = statusCode;
        RawResponse = rawResponse;
        ContentType = contentType;
        Encoding = encoding;
    }
}