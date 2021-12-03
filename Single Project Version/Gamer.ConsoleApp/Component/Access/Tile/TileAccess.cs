using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
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

		public async Task<bool> ProvisionTiles(Tile[] tiles)
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

		public async Task<Tile[]> FindTiles(Func<Tile,bool> filter)
		{

			var tiles = filter != null
				? cache.Where(filter).ToArray()
				: cache.ToArray();
			return await Task.FromResult(tiles);

		}

		public async Task<bool> RemoveTiles(Func<Tile,bool> filter)
		{

			Contract.Assert(filter != null, "Input filter cannot be null");
			var tiles = await FindTiles(filter);
			var count = 0;
			foreach(var tile in tiles)
			{
				var success = cache.Remove(tile);
				if (success)
					count++;

			}
			var result = count == tiles.Length;
			return await Task.FromResult(result);

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