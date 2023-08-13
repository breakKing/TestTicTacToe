namespace Identity.IntegrationEvents;

public sealed record UserRegisteredIntegrationEvent(Guid UserId, string Username);