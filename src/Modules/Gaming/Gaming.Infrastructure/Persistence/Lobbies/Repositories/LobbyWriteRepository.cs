using Gaming.Application.Lobbies;
using Gaming.Domain.Lobbies.Entities;
using Gaming.Domain.Lobbies.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Infrastructure.Persistence.Lobbies.Repositories;

internal sealed class LobbyWriteRepository : ILobbyWriteRepository
{
    private readonly GamingContext _context;

    public LobbyWriteRepository(GamingContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async ValueTask<Lobby?> LoadAsync(LobbyId lobbyId, CancellationToken ct = default)
    {
        return await _context.Set<Lobby>().SingleOrDefaultAsync(l => l.Id == lobbyId, ct);
    }

    /// <inheritdoc />
    public void Add(Lobby lobby)
    {
        _context.Add(lobby);
    }

    /// <inheritdoc />
    public void Update(Lobby lobby)
    {
        _context.Update(lobby);
    }

    /// <inheritdoc />
    public void Delete(Lobby lobby)
    {
        _context.Remove(lobby);
    }
}