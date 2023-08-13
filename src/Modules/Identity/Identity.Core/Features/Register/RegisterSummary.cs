using System.Net;
using Common.Api;
using FastEndpoints;

namespace Identity.Core.Features.Register;

public sealed class RegisterSummary : EndpointSummaryBase
{
    public RegisterSummary()
    {
        Summary = "Регистрация в приложении";
        Description = "Регистрация пользователя с предоставлением его логина (юзернейма) и пароля";
        
        AddSuccessResponseExample(HttpStatusCode.OK, new EmptyResponse());
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }
}