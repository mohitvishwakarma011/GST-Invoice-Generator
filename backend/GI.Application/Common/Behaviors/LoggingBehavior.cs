using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace GI.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation(
                "Handling {RequestName} | Payload: {Payload}",
                requestName,
                JsonSerializer.Serialize(request));

            var stopwatch = Stopwatch.StartNew();

            var response = await next();

            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;

            // Log completion
            _logger.LogInformation(
                "Handled {RequestName} in {ElapsedMs}ms",
                requestName,
                elapsed);

            if (elapsed > 500)
            {
                _logger.LogWarning(
                    "Slow request detected: {RequestName} took {ElapsedMs}ms",
                    requestName,
                    elapsed);
            }

            return response;
        }
    }
}
