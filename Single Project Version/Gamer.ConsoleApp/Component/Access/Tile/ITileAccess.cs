using System;
using System.Threading.Tasks;

namespace Gamer.Component.Access.Tile
{

	public interface ITileAccess
	{

		Task<bool> ProvisionTiles(Tile[] tiles);
		Task<Tile[]> FindTiles(Guid gameSessionId);
		Task<bool> RemoveTiles(Guid gameSessionId);
		Task<bool> UpdateTile(Tile tile);

	}

}