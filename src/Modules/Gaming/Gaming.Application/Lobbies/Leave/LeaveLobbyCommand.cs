﻿using Gaming.Application.Common.Handling;

namespace Gaming.Application.Lobbies.Leave;

public sealed record LeaveLobbyCommand(Guid PlayerId, Guid LobbyId) : ICommand;