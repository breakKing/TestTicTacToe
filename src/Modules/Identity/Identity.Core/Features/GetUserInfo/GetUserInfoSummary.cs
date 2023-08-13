using System.Net;
using Common.Api;

namespace Identity.Core.Features.GetUserInfo;

public sealed class GetUserInfoSummary : EndpointSummaryBase
{
    private static readonly GetUserInfoResponse SuccessResponse = new(
        Guid.NewGuid(),
        "test_user");
    
    public GetUserInfoSummary()
    {
        Summary = "Получение информации о текущем пользователе";
        Description = "Получение краткой информации о текущем пользователе";
        
        AddSuccessResponseExample(HttpStatusCode.OK, SuccessResponse);
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }
}