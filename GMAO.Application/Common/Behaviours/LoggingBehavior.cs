using MediatR;
using Microsoft.Extensions.Logging;

namespace GMAO.Application.Common.Behaviours
{
    /// <summary>
    /// Intercepte chaque requête MediatR pour journaliser son exécution.
    /// </summary>
    /// <typeparam name="TRequest">Type de la requête (Command ou Query).</typeparam>
    /// <typeparam name="TResponse">Type de la réponse.</typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("Handling {RequestName} with data: {@Request}", requestName, request);

            try
            {
                var response = await next();

                _logger.LogInformation("Handled {RequestName} successfully.", requestName);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while handling {RequestName} with data: {@Request}", requestName, request);
                throw;
            }
        }
    }
}
