﻿using Microsoft.AspNetCore.Builder;

namespace ExceptionCatcherMiddleware.Extensions;

public static class UseMiddlewareExtensions
{
    public static void UseExceptionCatcherMiddleware(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<Middleware.ExceptionCatcherMiddleware>();
    }
}