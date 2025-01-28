using System.ComponentModel.DataAnnotations;
using earfest.Shared.Base;
using Microsoft.AspNetCore.Diagnostics;

namespace earfest.API.Middlewares;

public static class GlobalExceptionHandler
{
    public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(options =>
        {
            options.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                //var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionFeature?.Error == null)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    var defaultResponse = AppResult<NoContentDto>.Fail("An unknown error occurred.", StatusCodes.Status500InternalServerError);
                    await context.Response.WriteAsJsonAsync(defaultResponse);
                    return;
                }

                var statusCode = exceptionFeature.Error switch
                {
                    ValidationException => StatusCodes.Status422UnprocessableEntity,
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    ArgumentException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = statusCode;
                var response = AppResult<NoContentDto>.Fail(exceptionFeature.Error.Message, statusCode);
                await context.Response.WriteAsJsonAsync(response);
            });
        });
    }
}
