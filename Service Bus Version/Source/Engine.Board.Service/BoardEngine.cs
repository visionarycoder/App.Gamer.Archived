using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.Tile.Interface;
using Gamer.Access.Tile.Service;
using Gamer.Engine.Board.Interface;
using Gamer.Framework;
using ServiceModelEx;

namespace Gamer.Engine.Board.Service
{

	public class BoardEngine : IBoardEngine
	{

		public async Task<Interface.Board> CreateBoard(Guid gameId)
		{

			var tiles = new List<Tile>();
			foreach (var col in new[] { "A", "B", "C" })
				foreach (var row in new[] {1, 2, 3})
					tiles.Add(new Tile
					{
						Row = row,
						Column = col,
						GameId = gameId,
					});

			var board = new Interface.Board(gameId, tiles.ToArray());

			var accessor = InProcFactory.CreateInstance<TileAccessor, ITileAccessor>();
			await accessor.CreateTiles(tiles.ToArray());


			return await Task.FromResult(board);

		}

		public async Task<bool> DestroyBoard(Guid gameId)
		{
			var accessor = InProcFactory.CreateInstance<TileAccessor, ITileAccessor>();
			return  await accessor.DeleteTiles(gameId);
		}

		public async Task<bool> PlaceTile(Guid boardId, string column, int row, string gamePiece)
		{

			var accessor = InProcFactory.CreateInstance<TileAccessor, ITileAccessor>();
			return await accessor.UpdateTile(boardId, column, row, gamePiece);

		}

		public async Task<bool> IsGamePlayable(Guid boardId)
		{

			var accessor = InProcFactory.CreateInstance<TileAccessor, ITileAccessor>();
			var tiles = await accessor.GetTiles(boardId);
			return tiles.All(i => i.GamePiece != Constant.TicTacToe.DEFAULT_GAMEPIECE);

		}

		public async Task<bool> IsPositionAvailable(Guid boardId, string column, int row)
		{

			var accessor = InProcFactory.CreateInstance<TileAccessor, ITileAccessor>();
			var tile = await accessor.GetTile(boardId, column, row);
			return tile.GamePiece == Constant.TicTacToe.DEFAULT_GAMEPIECE;

		}

		public async Task<Interface.Board> GetBoard(Guid boardId)
		{

			var accessor = InProcFactory.CreateInstance<TileAccessor, ITileAccessor>();
			var tiles = await accessor.GetTiles(boardId);
			var board = new Interface.Board(tiles.ToArray());
			return board;

		}

	}

}