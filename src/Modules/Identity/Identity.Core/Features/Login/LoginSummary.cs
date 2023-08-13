using System.Net;
using Common.Api;

namespace Identity.Core.Features.Login;

public sealed class LoginSummary : EndpointSummaryBase
{
    private static readonly LoginResponse SuccessResponse = new(
        Guid.NewGuid(),
        "access-token-here");
    
    public LoginSummary()
    {
        Summary = "Логин в приложении";
        Description = "Логин пользователя с предоставлением его юзернейма и пароля";
        
        AddSuccessResponseExample(HttpStatusCode.OK, SuccessResponse);
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }
}