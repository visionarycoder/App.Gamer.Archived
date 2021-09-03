using System;
using System.Diagnostics.CodeAnalysis;
using Framework;

namespace Access.Board.Interface
{

	public class GameTile : IGameTile
	{

        public Guid GameId { get; }
		public Guid GameTileId { get; }
		public int Row { get; }
		public string Column { get; }
		public string Address => Column + Row;
		
        public string GamePiece { get; private set; }

		public bool IsEmpty => AppConstant.DEFAULT_GAME_PIECE.Equals(GamePiece);

		public GameTile(Guid gameTileId, Guid gameId, int row, string col, [NotNull] string gamePiece = AppConstant.DEFAULT_GAME_PIECE)
        {

            GameTileId = gameTileId;
            GameId = gameId;
			Row = row;
			Column = col;
			GamePiece = gamePiece;

		}

		public void AssignGamePiece(string gamePiece)
		{
			GamePiece = gamePiece;
		}

		public override string ToString()
		{
			return GamePiece.PadRight(1, ' ');
		}

	}

}
