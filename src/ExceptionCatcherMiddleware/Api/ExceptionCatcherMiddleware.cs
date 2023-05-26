using ExceptionCatcherMiddleware.Core.MainClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ExceptionCatcherMiddleware.Api;

internal class ExceptionCatcherMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionCatcherMiddleware> _logger;
    private readonly ExceptionToHttpContextApplier _exceptionToHttpContextApplier;

    public ExceptionCatcherMiddleware(ILogger<ExceptionCatcherMiddleware> logger,
        ExceptionToHttpContextApplier exceptionToHttpContextApplier)
    {
        _logger = logger;
        _exceptionToHttpContextApplier = exceptionToHttpContextApplier;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception caught by {Catcher}", nameof(ExceptionCatcherMiddleware));

            await _exceptionToHttpContextApplier.ApplyToHttpContext(context, exception);
        }
    }
}