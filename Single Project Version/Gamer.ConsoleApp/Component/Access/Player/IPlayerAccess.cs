using System;
using System.Threading.Tasks;

namespace Gamer.Component.Access.Player
{

    public interface IPlayerAccess
    {

        Task<Player> CreatePlayer(Player player);
        Task<Player[]> CreatePlayers(Player[] players);
        Task<Player[]> FindPlayers(Func<Player, bool> filter);
        Task<bool> RemovePlayer(Guid playerId);

    }

}