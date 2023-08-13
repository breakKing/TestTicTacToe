﻿using Gaming.Domain.Common;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Domain.Players.Entities;

public sealed class Player : AggregateRoot<PlayerId>
{
    public string Username { get; private set; }

    private Player(string username) : base(PlayerId.CreateNew())
    {
        
    }
    
    /// <inheritdoc />
    public Player(PlayerId playerId, string username) : base(playerId)
    {
        Username = username;
    }
}