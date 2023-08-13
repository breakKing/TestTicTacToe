using Gaming.Application.Common.Handling;

namespace Gaming.Application.Players.Create;

public sealed record PlayerCreateCommand(Guid Id, string Username) : ICommand;