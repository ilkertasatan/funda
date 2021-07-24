using System.Text;
using Flurl.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Funda.Assignment.Api.Extensions
{
   public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    var logger = context.RequestServices.GetService<ILogger>();
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    logger.LogError(exception, "Error: {ErrorMessage}", exception.Message);

                    int statusCode;
                    var detail = "An error occurred";

                    switch (exception)
                    {
                        case FlurlHttpTimeoutException timeoutException:
                            statusCode = StatusCodes.Status502BadGateway;
                            detail = timeoutException.Message;
                            
                            break;
                        case FlurlHttpException httpException:
                            statusCode = httpException.StatusCode.GetValueOrDefault();
                            detail = httpException.Message;
                            
                            break;
                        default:
                            statusCode = context.Response.StatusCode;
                            break;
                    }
                    
                    var problemDetails = new ProblemDetails
                    {
                        Status = context.Response.StatusCode,
                        Detail = detail
                    };

                    context.Response.StatusCode = statusCode;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails), Encoding.UTF8);
                });
            });

            return app;
        }
    }
}