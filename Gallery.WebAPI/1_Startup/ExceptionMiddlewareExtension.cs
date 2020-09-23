using Gallery.WebAPI.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Gallery.WebAPI._1_Startup
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var exception = context.Features.Get<IExceptionHandlerFeature>();

                    if (exception != null)
                    {
                        context.Response.AddApplicationExcention(exception.Error.Message);
                        await context.Response.WriteAsync(exception.Error.Message);
                    }
                });
            });
        }
    }
}
