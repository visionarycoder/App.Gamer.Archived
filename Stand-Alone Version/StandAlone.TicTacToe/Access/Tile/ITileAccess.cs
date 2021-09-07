using System;
using System.Threading.Tasks;

namespace TicTacToe.Access.Tile
{
    public interface ITileAccess
    {
    
        Task<Tile[]> FindTiles(Guid gameSessionId);
        Task<bool> DeleteTiles(Guid gameSessionId);
        Task<bool> AddTiles(Tile[] tiles);

    }
}