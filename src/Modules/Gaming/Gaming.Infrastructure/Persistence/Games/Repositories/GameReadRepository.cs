using Gaming.Application.Games;
using Gaming.Application.Players;
using Gaming.Domain.Games.Entities;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Players.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Infrastructure.Persistence.Games.Repositories;

internal sealed class GameReadRepository : IGameReadRepository
{
    private readonly GamingContext _context;

    public GameReadRepository(GamingContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async ValueTask<GameDto?> GetByIdAsync(GameId gameId, CancellationToken ct = default)
    {
        var mainQuery = _context
            .Set<Game>()
            .Where(g => g.Id == gameId)
            .AsNoTracking();

        var joinedQuery = JoinWithFirstPlayer(mainQuery);
        joinedQuery = JoinWithSecondPlayer(joinedQuery);

        return await joinedQuery.SingleOrDefaultAsync(ct);
    }

    private IQueryable<GameDto> JoinWithFirstPlayer(IQueryable<Game> query)
    {
        return query
            .Join(
                _context.Set<Player>(),
                g => g.FirstPlayerId,
                p => p.Id,
                (g, p) => new GameDto
                {
                    Id = g.Id,
                    FirstPlayer = new PlayerDto
                    {
                        Id = p.Id,
                        Username = p.Username
                    },
                    SecondPlayer = new PlayerDto
                    {
                        Id = g.SecondPlayerId,
                        Username = ""
                    },
                    Field = g.Field.Cells,
                    LastMovePlayerId = g.LastMovePlayerId,
                    StartedAt = g.StartedAt,
                    FinishedAt = g.FinishedAt,
                    Result = g.Result
                });
    }
    
    private IQueryable<GameDto> JoinWithSecondPlayer(IQueryable<GameDto> query)
    {
        return query
            .Join(
                _context.Set<Player>(),
                g => g.SecondPlayer.Id,
                p => p.Id,
                (g, p) => new GameDto
                {
                    Id = g.Id,
                    FirstPlayer = g.FirstPlayer,
                    SecondPlayer = new PlayerDto
                    {
                        Id = p.Id,
                        Username = p.Username
                    },
                    Field = g.Field,
                    LastMovePlayerId = g.LastMovePlayerId,
                    StartedAt = g.StartedAt,
                    FinishedAt = g.FinishedAt,
                    Result = g.Result
                });
    }
}