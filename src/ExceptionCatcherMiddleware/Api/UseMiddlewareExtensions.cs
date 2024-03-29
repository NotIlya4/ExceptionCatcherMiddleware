﻿using Microsoft.AspNetCore.Builder;

namespace ExceptionCatcherMiddleware.Api;

public static class UseMiddlewareExtensions
{
    public static void UseExceptionCatcherMiddleware(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ExceptionCatcherMiddleware>();
    }
}