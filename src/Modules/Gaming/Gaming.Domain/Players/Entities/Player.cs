using Common.Domain.Primitives;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Domain.Players.Entities;

public sealed class Player : AggregateRoot<PlayerId>
{
    public string Username { get; private set; }
    
    public long TotalScore { get; private set; }
    
    /// <inheritdoc />
    public Player(string username) : base(PlayerId.Create())
    {
        Username = username;
    }

    public void IncreaseTotalScore(long score)
    {
        TotalScore += score;
    }
}