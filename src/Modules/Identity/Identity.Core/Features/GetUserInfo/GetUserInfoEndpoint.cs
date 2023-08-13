using System.Net;
using Common.Api;
using FastEndpoints;
using Identity.Core.Common.Identity.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Features.GetUserInfo;

public sealed class GetUserInfoEndpoint : EndpointBase<GetUserInfoRequest, Results<Ok<GetUserInfoResponse>, ProblemDetails>>
{
    private const string UnexpectedErrorDescription = "Произошла непредвиденная ошибка при получении пользователя";
    
    private readonly UserManager<User> _userManager;

    public GetUserInfoEndpoint(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Get("current");
        Group<UserGroup>();
        
        ConfigureSwaggerDescription(
            new GetUserInfoSummary(), 
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError);
    }

    /// <inheritdoc />
    public override async Task<Results<Ok<GetUserInfoResponse>, ProblemDetails>> ExecuteAsync(
        GetUserInfoRequest req, 
        CancellationToken ct)
    {
        var userId = GetCurrentUserId()!;

        var user = await _userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
        {
            // В данный блок кода мы никогда не должны попасть в виду того, что пользователь уже авторизован
            AddError(UnexpectedErrorDescription);
            return new ProblemDetails(ValidationFailures);
        }

        return TypedResults.Ok(new GetUserInfoResponse(user.Id, user.UserName!));
    }
}