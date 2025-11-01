using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Behaviours
{
    /// <summary>
    /// Mesure le temps d'exécution des requêtes MediatR pour détecter les lenteurs.
    /// </summary>
    /// <typeparam name="TRequest">Type de la requête.</typeparam>
    /// <typeparam name="TResponse">Type de la réponse.</typeparam>
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
        private readonly Stopwatch _timer;

        public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500) // seuil : 500 ms (ajuste selon besoin)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogWarning("Long Running Request: {RequestName} ({ElapsedMilliseconds} ms) {@Request}",
                    requestName, elapsedMilliseconds, request);
            }

            return response;
        }
    }
}
