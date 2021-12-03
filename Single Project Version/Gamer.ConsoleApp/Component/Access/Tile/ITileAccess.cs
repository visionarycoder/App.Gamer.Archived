using System;
using System.Threading.Tasks;

namespace Gamer.Component.Access.Tile
{

	public interface ITileAccess
	{

		Task<bool> ProvisionTiles(Tile[] tiles);
		Task<Tile[]> FindTiles(Func<Tile,bool> filter);
		Task<bool> RemoveTiles(Func<Tile, bool> filter);
		Task<bool> UpdateTile(Tile tile);

	}

}