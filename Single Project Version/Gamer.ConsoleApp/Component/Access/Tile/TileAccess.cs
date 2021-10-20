using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.Component.Access.Tile
{

	public class TileAccess : ITileAccess
	{

		private static readonly HashSet<Tile> cache;

		static TileAccess()
		{
			cache = new HashSet<Tile>();
		}

		public async Task<bool> CreateTiles(Tile[] tiles)
		{

			var result = true;
			foreach (var tile in tiles)
			{
				if (cache.FirstOrDefault(i => i.Id == tile.Id) == null)
				{
					result = result && cache.Add(tile);
				}
			}
			return await Task.FromResult(result);

		}

		public async Task<Tile[]> FindTiles(Guid gameSessionId)
		{

			var tiles = cache.Where(i => i.GameSessionId == gameSessionId).ToArray();
			return await Task.FromResult(tiles);

		}

		public async Task<bool> RemoveTiles(Guid gameSessionId)
		{

			var count = cache.RemoveWhere(i => i.GameSessionId == gameSessionId);
			var result = count > 0;
			return await Task.FromResult(result);

		}

		public async Task<bool> UpdateTiles(Tile[] tiles)
		{

			var result = true;
			foreach (var tile in tiles)
			{
				result = result && await UpdateTile(tile);
			}
			return result;

		}

		public async Task<bool> UpdateTile(Tile tile)
		{

			var cachedTile = cache.FirstOrDefault(i => i.Id == tile.Id);
			if (cachedTile != null)
			{
				Trace.WriteLine($"Updated tile to player: {tile.PlayerId} from {cachedTile.PlayerId}");
				cachedTile.PlayerId = tile.PlayerId;
				return true;
			}
			return await Task.FromResult(false);

		}

	}

}