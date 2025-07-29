using Hotel.Core.Entities.Enum;
using HotelSystem.Exceptions;
using HotelSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization; 
using System.Text.Json;
using HotelSystem.Resources;

namespace HotelSystem.Middlewares
{
    public class GlobalErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IStringLocalizer<SharedResources> _localization; 

        public GlobalErrorHandlerMiddleware(
            ILogger<GlobalErrorHandlerMiddleware> logger,
            IWebHostEnvironment environment,
            IStringLocalizer<SharedResources> localization) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _localization = localization ?? throw new ArgumentNullException(nameof(localization));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var traceId = context.TraceIdentifier;

            try
            {
                _logger.LogInformation($"Request started. TraceId: {traceId}, Path: {context.Request.Path}, Method: {context.Request.Method}");

                await next(context);

                _logger.LogInformation($"Request completed successfully. TraceId: {traceId}, StatusCode: {context.Response.StatusCode}");
            }
            catch (BaseApplicationException appEx)
            {
                await HandleApplicationExceptionAsync(context, appEx, traceId);
            }
            catch (UnauthorizedAccessException unauthorizedEx)
            {
                await HandleUnauthorizedExceptionAsync(context, unauthorizedEx, traceId);
            }
            catch (Exception ex)
            {
                await HandleUnexpectedExceptionAsync(context, ex, traceId);
            }
        }

        private async Task HandleApplicationExceptionAsync(HttpContext context, BaseApplicationException exception, string traceId)
        {
            var logLevel = exception is BusinessLogicException ? LogLevel.Warning : LogLevel.Error;

            _logger.Log(
                logLevel,
                exception,
                "Application Exception. TraceId: {TraceId}, Message: {Message}, ErrorCode: {ErrorCode}, RequestPath: {RequestPath}",
                traceId,
                exception.Message,
                exception.ErrorCode,
                context.Request.Path);

            var response = ResponseViewModel<bool>.Error(
                exception.Message,
                exception.ErrorCode,
                _environment.IsDevelopment() ? traceId : null);

            await WriteErrorResponseAsync(context, response, exception.HttpStatusCode, traceId);
        }

        private async Task HandleUnauthorizedExceptionAsync(HttpContext context, UnauthorizedAccessException exception, string traceId)
        {
            _logger.LogWarning(
                exception,
                "Unauthorized Access Exception. TraceId: {TraceId}, Message: {Message}, RequestPath: {RequestPath}",
                traceId,
                exception.Message,
                context.Request.Path);

            var response = ResponseViewModel<bool>.Error(
                _localization["AccessDenied"],
                ErrorCode.UnauthorizedAccess,
                _environment.IsDevelopment() ? traceId : null);

            await WriteErrorResponseAsync(context, response, StatusCodes.Status403Forbidden, traceId);
        }

        private async Task HandleUnexpectedExceptionAsync(HttpContext context, Exception exception, string traceId)
        {
            _logger.LogError(
                exception,
                "Unexpected error occurred. TraceId: {TraceId}, RequestPath: {RequestPath}",
                traceId,
                context.Request.Path);

            var response = ResponseViewModel<bool>.Error(
                _environment.IsDevelopment()
                    ? $"An unexpected error occurred. TraceId: {traceId}"
                    : _localization["UnexpectedError"], 
                ErrorCode.InternalServerError,
                _environment.IsDevelopment() ? traceId : null);

            await WriteErrorResponseAsync(context, response, StatusCodes.Status500InternalServerError, traceId);
        }

        private async Task WriteErrorResponseAsync<T>(HttpContext context, ResponseViewModel<T> response, int statusCode, string traceId)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            context.Response.Headers.Add("X-Trace-Id", traceId);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = _environment.IsDevelopment()
            };

            await context.Response.WriteAsJsonAsync(response, jsonOptions);
        }
    }
}
