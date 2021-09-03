using System;

namespace Access.Board.Interface
{

	public interface IGameTile
	{

		Guid GameId { get; }
		int Row { get; }
		string Column { get; }
		string Address { get; }
		string GamePiece { get; }
		bool IsEmpty { get; }

		void AssignGamePiece(string gamePiece);

	}

}