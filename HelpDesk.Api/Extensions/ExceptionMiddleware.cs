﻿using System.Net;

namespace HelpDesk.Api.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;            
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;            
        }
    }
}
