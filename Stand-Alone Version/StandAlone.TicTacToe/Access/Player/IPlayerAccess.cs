using System;
using System.Threading.Tasks;

namespace TicTacToe.Access.Player
{
    public interface IPlayerAccess
    {
        Task<Player> GetPlayer(Guid playerId);
        Task<Player[]> FindPlayers(Func<Player, bool> filter);
        Task<bool> DeletePlayer(Guid playerId);
        Task<Player> CreatePlayer(string name, string gamePiece, bool isMachine);
        
    }
}