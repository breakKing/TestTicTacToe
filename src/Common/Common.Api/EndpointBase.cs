using System.Net;
using System.Security.Claims;
using ErrorOr;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Common.Api;

/// <summary>
/// Базовый класс эндпоинта
/// </summary>
/// <typeparam name="TRequest">Тип запроса</typeparam>
/// <typeparam name="TResponse">Тип ответа</typeparam>
public abstract class EndpointBase<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    where TRequest : notnull
{
    /// <summary>
    /// Конфигурация описания эндпоинта в сваггере
    /// </summary>
    /// <param name="summary">Основное описание эндпоинта</param>
    /// <param name="statusCodes">Возможные коды ответа от эндпоинта</param>
    protected virtual void ConfigureSwaggerDescription(
        EndpointSummaryBase summary, 
        params HttpStatusCode[] statusCodes)
    {
        DontAutoTag();
        
        Description(desc =>
        {
            foreach (var code in statusCodes)
            {
                desc.Produces<TResponse>((int)code);
            }
        });
        
        Summary(summary);
    }

    protected Guid? GetCurrentUserId()
    {
        var sub = User.FindFirstValue("sub");

        if (Guid.TryParse(sub, out var userId))
        {
            return userId;
        }

        return null;
    }

    protected void AddErrors(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            AddError(error.Description);
        }
    }
}