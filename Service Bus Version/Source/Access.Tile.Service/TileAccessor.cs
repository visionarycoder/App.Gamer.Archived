using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.Tile.Interface;

namespace Gamer.Access.Tile.Service
{

	public class TileAccessor : ITileAccessor
	{

		public Task<Interface.Tile> GetTile(Guid boardId, string column, int row)
		{
			var context = GetContext();
			var item = context.Tiles.FirstOrDefault(i => i.GameId == boardId && i.Column == column && i.Row == row);
			var task = Task.FromResult(item);
			return task;
		}

		public async Task<Interface.Tile[]> GetTiles(Guid boardId)
		{

			var context = GetContext();
			return await context.Tiles.Where(i => i.GameId == boardId).ToArrayAsync();

		}

		public async Task<bool> UpdateTile(Guid boardId, string column, int row, string gamePiece)
		{

			var context = GetContext();
			var tile = context.Tiles.FirstOrDefault(i => i.GameId == boardId && i.Column == column && i.Row == row);
			if (tile == null)
				throw new ApplicationException("Tile not found");

			tile.GamePiece = gamePiece;
			var count = await context.SaveChangesAsync();
			return count == 1;

		}

		public async Task<bool> CreateTiles(Interface.Tile[] tiles)
		{

			var context = GetContext();
			context.Tiles.AddRange(tiles);
			var count = await context.SaveChangesAsync();
			return count == tiles.Length;

		}

		public async Task<bool> DeleteTiles(Guid gameId)
		{

			var context = GetContext();
			var tiles = context.Tiles.Where(i => i.GameId == gameId);
			var tileCount = tiles.Count();
			context.Tiles.RemoveRange(tiles);
			var count = await context.SaveChangesAsync();
			return count == tileCount;

		}

		public async Task<bool> UpdateTiles(Interface.Tile[] tiles)
		{

			var context = GetContext();
			foreach (var tile in tiles)
				context.Tiles.Attach(tile);

			var count = await context.SaveChangesAsync();
			return count == tiles.Length;

		}

		private TileDB GetContext()
		{
			var context = new TileDB();
			context.Configuration.ProxyCreationEnabled = false;
			context.Configuration.LazyLoadingEnabled = false;
			return context;
		}

	}

}
