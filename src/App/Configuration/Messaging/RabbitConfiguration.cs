namespace App.Configuration.Messaging;

internal sealed class RabbitConfiguration
{
    internal const string SectionName = "Rabbit";

    public string Host { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}