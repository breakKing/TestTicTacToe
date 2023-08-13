using Gaming.Application.Common.Primitives.Pagination;
using Gaming.Application.Lobbies;
using Gaming.Application.Players;
using Gaming.Domain.Lobbies.Entities;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.Entities;
using Gaming.Domain.Players.ValueObjects;
using Gaming.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Infrastructure.Persistence.Lobbies.Repositories;

internal sealed class LobbyReadRepository : ILobbyReadRepository
{
    private readonly GamingContext _context;

    public LobbyReadRepository(GamingContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async ValueTask<bool> IsPlayerInLobbyAsync(PlayerId playerId, CancellationToken ct = default)
    {
        return await _context.Set<Lobby>()
            .AnyAsync(
                l => l.InitiatorPlayerId == playerId || l.JoinedPlayerId == playerId, 
                ct);
    }

    /// <inheritdoc />
    public async ValueTask<LobbyDto?> GetPlayerLobbyAsync(PlayerId playerId, CancellationToken ct = default)
    {
        var mainQuery = _context.Set<Lobby>()
            .Where(l => l.InitiatorPlayerId == playerId || l.JoinedPlayerId == playerId)
            .AsNoTracking();

        var finalQuery = SelectDto(mainQuery);

        return await finalQuery.SingleOrDefaultAsync(ct);
    }

    /// <inheritdoc />
    public async ValueTask<PagedList<LobbyDto>> GetAvailableLobbiesAsync(
        PlayerId playerId, 
        PaginationRequest paginationRequest,
        CancellationToken ct = default)
    {
        var mainQuery = _context.Set<Lobby>()
            .Where(l => l.JoinedPlayerId == null);

        var count = await mainQuery.CountAsync(ct);

        mainQuery = mainQuery
            .Paginate(paginationRequest)
            .AsNoTracking();
        
        var finalQuery = SelectDto(mainQuery);

        var data = await finalQuery.ToListAsync(ct);

        return new PagedList<LobbyDto>(data, count, paginationRequest.PageNumber, paginationRequest.PageSize);
    }

    private IQueryable<LobbyDto> SelectDto(IQueryable<Lobby> query)
    {
        var resultQuery = JoinWithFirstPlayer(query);
        resultQuery = JoinWithSecondPlayer(resultQuery);

        return resultQuery;
    }
    
    private IQueryable<LobbyDto> JoinWithFirstPlayer(IQueryable<Lobby> query)
    {
        return query
            .Join(
                _context.Set<Player>(),
                l => l.InitiatorPlayerId,
                p => p.Id,
                (l, p) => new LobbyDto
                {
                    Id = l.Id,
                    InitiatorPlayer = new PlayerDto
                    {
                        Id = p.Id,
                        Username = p.Username
                    },
                    JoinedPlayer = l.JoinedPlayerId == null ? 
                        null :
                        new PlayerDto
                        {
                            Id = l.JoinedPlayerId,
                            Username = ""
                        }
                });
    }
    
    private IQueryable<LobbyDto> JoinWithSecondPlayer(IQueryable<LobbyDto> query)
    {
        return query
            .Join(
                _context.Set<Player>(),
                l => l.JoinedPlayer!.Id,
                p => p.Id,
                (l, p) => new LobbyDto
                {
                    Id = l.Id,
                    InitiatorPlayer = l.InitiatorPlayer,
                    JoinedPlayer = new PlayerDto
                    {
                        Id = p.Id,
                        Username = p.Username
                    }
                });
    }
}