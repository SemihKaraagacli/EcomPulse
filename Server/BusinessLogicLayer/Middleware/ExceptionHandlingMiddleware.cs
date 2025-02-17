﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BusinessLogicLayer.Middleware;

public static class ExceptionHandlingMiddleware
{
    public static void ConfigureExceptionHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionHandlerFeature is not null)
                {
                    var exception = exceptionHandlerFeature.Error; ;
                    var statusCode = exception switch
                    {
                        ArgumentException => (int)HttpStatusCode.BadRequest,
                        KeyNotFoundException => (int)HttpStatusCode.NotFound,
                        UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                        _ => (int)HttpStatusCode.InternalServerError
                    };

                    var problemDetails = new ProblemDetails
                    {
                        Status = statusCode,
                        Title = "An error occurred",
                        Detail = exceptionHandlerFeature.Error.Message.First().ToString()
                    };
                    context.Response.StatusCode = statusCode;
                    context.Response.ContentType = "application/problem+json";
                    await context.Response.WriteAsJsonAsync(problemDetails);
                }

            });
        });
    }
}
