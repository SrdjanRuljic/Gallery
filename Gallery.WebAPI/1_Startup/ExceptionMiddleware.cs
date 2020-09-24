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
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                switch (context.Response.StatusCode)
                {
                    case (int)HttpStatusCode.BadRequest:
                        await HandleArgumentExceptionExceptionAsync(context, ex);
                        break;
                    default:
                        await HandleApplicationExceptionAsync(context, ex);
                        break;
                }
            }
        }

        private Task HandleApplicationExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.AddApplicationExcention(exception.Message);
            return context.Response.WriteAsync(exception.Message);
        }

        private Task HandleArgumentExceptionExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.AddArgumentnExcention(exception.Message);
            return context.Response.WriteAsync(exception.Message);
        }
    }
}
