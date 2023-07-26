// SPDX-License-Identifier: EUPL-1.2
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FabbPers.Logging
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
                _next = next;
                _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }
 
        public async Task Invoke(HttpContext context)
    {
            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation(
                    "Request {host} {method} {url} {query}=> {statusCode}",
                    context.Request?.Host,
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Request?.QueryString,
                    context.Response?.StatusCode);
            }
        }
    }
}