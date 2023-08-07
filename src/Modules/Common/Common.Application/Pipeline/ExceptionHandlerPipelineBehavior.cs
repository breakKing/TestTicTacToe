using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.Application.Pipeline;

public sealed class ExceptionHandlerPipelineBehavior<TRequest, TResponse> : 
    IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull
    where TResponse : IErrorOr, new()
{
    private const string UnhandledExceptionErrorCode = "Exception";
    
    private readonly ILogger<ExceptionHandlerPipelineBehavior<TRequest, TResponse>> _logger;

    public ExceptionHandlerPipelineBehavior(ILogger<ExceptionHandlerPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError("An exception was caught: {@Exception}", ex);

            var error = Error.Unexpected(UnhandledExceptionErrorCode, ex.Message);

            return (TResponse)(dynamic)error;
        }
    }
}