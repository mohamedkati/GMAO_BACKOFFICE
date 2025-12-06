using FluentValidation;
using GMAO.Application.Common.Exceptions;
using MediatR;

namespace GMAO.Application.Common.Behaviours
{
    /// <summary>
    /// Exécute la validation FluentValidation pour chaque requête MediatR avant le handler.
    /// </summary>
    /// <typeparam name="TRequest">Type de la requête.</typeparam>
    /// <typeparam name="TResponse">Type de la réponse.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
                throw new AppValidationException(failures);

            return await next();
        }
    }
}
