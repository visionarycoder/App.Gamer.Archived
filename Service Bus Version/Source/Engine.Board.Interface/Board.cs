using System;
using System.Collections.Generic;
using System.Linq;
using Gamer.Access.Tile.Interface;
using Gamer.Framework;

namespace Gamer.Engine.Board.Interface
{

	public class Board
	{

		public Guid Id { get; }

		private readonly Dictionary<string, Tile> dictionary;
		public KeyValuePair<string, string>[] AddressGamePieceMap => dictionary.Select(i => new KeyValuePair<string, string>(i.Key, i.Value.GamePiece)).ToArray();

		public IList<Tile> Tiles => dictionary.Values.ToList();

		public Tile A1 => dictionary["A1"];
		public Tile A2 => dictionary["A2"];
		public Tile A3 => dictionary["A3"];
		public Tile B1 => dictionary["B1"];
		public Tile B2 => dictionary["B2"];
		public Tile B3 => dictionary["B3"];
		public Tile C1 => dictionary["C1"];
		public Tile C2 => dictionary["C2"];
		public Tile C3 => dictionary["C3"];

		public bool IsEmpty => dictionary.Values.All(i => i.GamePiece == Constant.TicTacToe.DEFAULT_GAMEPIECE);

		public bool IsFull => dictionary.Values.All(i => i.GamePiece != Constant.TicTacToe.DEFAULT_GAMEPIECE);

		public Board()
			: this(Guid.NewGuid())
		{

		}

		public Board(Guid id)
		{

			Id = id;

			dictionary = new Dictionary<string, Tile>
			{
				{"A1", null},
				{"A2", null},
				{"A3", null},
				{"B1", null},
				{"B2", null},
				{"B3", null},
				{"C1", null},
				{"C2", null},
				{"C3", null}
			};

		}

		public Board(Tile[] tiles)
			: this()
		{
			UpdateTiles(tiles);
		}

		public Board(Guid gameId, Tile[] tiles)
			: this(gameId)
		{
			UpdateTiles(tiles);
		}

		public void UpdateTile(Tile tile)
		{

			var address = tile.Column + tile.Row;
			if(! dictionary.ContainsKey(address))
				throw new ArgumentException($"Invalid address: {address}");
			var target = dictionary[address];
			if (target != null)
			{
				dictionary[address].GamePiece = tile.GamePiece;
			}
			else
			{
				dictionary[address] = tile;
			}

		}

		public void UpdateTiles(Tile[] tiles)
		{

			foreach (var tile in tiles)
				UpdateTile(tile);

		}

	}

}
