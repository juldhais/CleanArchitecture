using System.Net;
using System.Text.Json;
using CleanArchitecture.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanArchitecture.WebAPI.Extensions;

public static class ErrorHandlerExtensions
{
    public static void UseErrorHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature == null) return;

                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = contextFeature.Error switch
                {
                    BadRequestException => (int) HttpStatusCode.BadRequest,
                    OperationCanceledException => (int) HttpStatusCode.ServiceUnavailable,
                    NotFoundException => (int) HttpStatusCode.NotFound,
                    _ => (int) HttpStatusCode.InternalServerError
                };

                var errorResponse = new
                {
                    statusCode = context.Response.StatusCode,
                    message = contextFeature.Error.GetBaseException().Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            });
        });
    }
}