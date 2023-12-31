﻿using Gallery.Common.Helpers;
using Gallery.WebAPI.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            catch (HttpStatusCodeException exception)
            {
                await HandleExceptionAsync(context, exception);             
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            context.Response.ContentType = "application/json";

            switch ((int)exception.StatusCode)
            {
                case (int)HttpStatusCode.BadRequest:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.AddArgumentnExcention(exception.Message);
                    break;
                case (int)HttpStatusCode.NotFound:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.AddNotFoundExcention(exception.Message);
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.AddApplicationExcention(exception.Message);
                    break;
            }

            await context.Response.WriteAsync(exception.Message);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.AddApplicationExcention(exception.Message);
            await context.Response.WriteAsync(exception.Message);
        }
    }
}
