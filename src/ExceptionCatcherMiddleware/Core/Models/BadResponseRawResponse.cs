using System.Text;

namespace ExceptionCatcherMiddleware.Core.Models;

internal record BadResponseRawResponse
{
    public int StatusCode { get; }
    public string RawResponse { get; }
    public Encoding? Encoding { get; }
    public string ContentType { get; }

    public BadResponseRawResponse(int statusCode, string rawResponse, Encoding? encoding, string contentType)
    {
        StatusCode = statusCode;
        RawResponse = rawResponse;
        Encoding = encoding;
        ContentType = contentType;
    }
}