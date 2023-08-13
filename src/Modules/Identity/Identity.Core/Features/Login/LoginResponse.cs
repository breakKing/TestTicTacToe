namespace Identity.Core.Features.Login;

public sealed record LoginResponse(Guid UserId, string Token);