using System.Transactions;
using Common.Application.DataAccess;
using ErrorOr;
using MediatR;

namespace Common.Application.Pipeline;

public sealed class UnitOfWorkPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : IErrorOr, new()
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkPipelineBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (!IsCommand())
        {
            return await next();
        }

        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        var result = await next();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        transactionScope.Complete();

        return result;
    }

    private static bool IsCommand()
    {
        return typeof(TResponse).Name.EndsWith("Command");
    }
}