using Gaming.Application.Players;
using Gaming.Domain.Players.Entities;

namespace Gaming.Infrastructure.Persistence.Players.Repositories;

internal sealed class PlayerWriteRepository : IPlayerWriteRepository
{
    private readonly GamingContext _context;

    public PlayerWriteRepository(GamingContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public void Add(Player player)
    {
        _context.Add(player);
    }
}