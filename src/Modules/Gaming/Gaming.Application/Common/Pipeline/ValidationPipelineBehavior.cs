using ErrorOr;
using FluentValidation;
using MediatR;

namespace Gaming.Application.Common.Pipeline;

public sealed class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull
    where TResponse : IErrorOr, new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        
        var context = new ValidationContext<TRequest>(request);

        var validationTasks = _validators
            .Select(x => x.ValidateAsync(context, cancellationToken))
            .ToList();

        var validationResults = await Task.WhenAll(validationTasks);
        
        var errorsDictionary = validationResults
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);
        
        if (errorsDictionary.Count > 0)
        {
            var errors = errorsDictionary
                .SelectMany(e => e.Value)
                .Select(e => Error.Validation(description: e))
                .ToArray();

            return (TResponse)(dynamic)errors;
        }
        
        return await next();
    }
}