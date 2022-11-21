using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Task1_T.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler(
                    options =>
                    {
                        options.Run(
                            async context =>
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                var exception = context.Features.Get<IExceptionHandlerFeature>();
                                if (exception != null)
                                {
                                    await context.Response.WriteAsync(exception.Error.Message);
                                }
                            });
                    });
            }
        }
    }
}
