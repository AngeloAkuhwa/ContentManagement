using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentManagement.Infrastructure.Helpers
{
    public class LoggingHelper
    {
        public static void LogInformation<T>(ILogger<T> logger, string nameOfMethod, string information)
        {
            logger.LogInformation($"Information: {nameOfMethod} - {information}");
        }

        public static void LogInformation<T>(ILogger<T> logger, HttpRequest request, string information)
        {
            logger.LogInformation($"Information: {request.Method} {request.Path.Value} - {information}");
        }

        public static void LogError<T>(ILogger<T> logger, string nameOfMethod, string error)
        {
            logger.LogError($"Error: {nameOfMethod} - {error}");
        }

        public static void LogError<T>(ILogger<T> logger, HttpRequest request, string error)
        {
            logger.LogError($"Error: {request.Method} {request.Path.Value} - {error}");
        }

        public static void LogErrorAndThrowException<T>(ILogger<T> logger, string nameOfMethod, string error)
        {
            logger.LogError($"Error: {nameOfMethod} - {error}");
            throw new Exception(error);
        }

        public static void LogErrorAndThrowException<T>(ILogger<T> logger, HttpRequest request, string error)
        {
            logger.LogError($"Error: {request.Method} {request.Path.Value} - {error}");
            throw new Exception(error);
        }
    }
}
