using Gaming.Domain.Players.Entities;

namespace Gaming.Application.Players;

public interface IPlayerWriteRepository
{
    void Add(Player player);
}