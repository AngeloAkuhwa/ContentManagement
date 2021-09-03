using ContentManagement.Application.CustomExceptions;
using ContentManagement.Domain.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ContentManagement.Presentation.ExceptionHandlers
{
    public class GlobalExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleWare> _logger;
        private readonly IHostEnvironment _env;
        private IList<Error> error = new List<Error>();

        /// <summary>
        /// ExceptionMiddleware constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        /// <param name="env"></param>
        public GlobalExceptionHandlerMiddleWare(
            RequestDelegate next, 
            ILogger<GlobalExceptionHandlerMiddleWare> logger, 
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// Method to invoke exception
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (BadRequestException exc)
            {
                _logger.LogError(exc.StackTrace, exc.Message);
                await ConvertException(exc, context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex.Message);
                await ConvertException(ex, context);
            }
        }

        //Determines the status code to return and the creates the response object in the event of an error.
        private async Task ConvertException(Exception exception, HttpContext context)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            switch (exception)
            {
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    error = badRequestException.Errors;
                    break;
                default:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.StatusCode = (int)httpStatusCode;
            var response = _env.IsDevelopment()
                    ? new Response<string>(exception.Message, exception.StackTrace?.ToString(), error).ToString()
                    : new Response<string>(exception.Message).ToString();

            await context.Response.WriteAsync(response);
        }
    }
}
