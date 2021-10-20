using System;
using System.Threading.Tasks;

namespace Gamer.Component.Access.Tile
{

	public interface ITileAccess
	{

		Task<bool> CreateTiles(Tile[] tiles);
		Task<Tile[]> FindTiles(Guid gameSessionId);
		Task<bool> RemoveTiles(Guid gameSessionId);
		Task<bool> UpdateTiles(Tile[] tiles);
		Task<bool> UpdateTile(Tile tile);

	}

}