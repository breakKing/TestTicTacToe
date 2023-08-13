namespace App.Configuration.Messaging;

internal sealed class RabbitConfiguration
{
    internal const string SectionName = "Rabbit";

    public string Host { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}