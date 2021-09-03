using System;
using System.Diagnostics;
using System.Linq;
using TicTacToe.Models;

namespace TicTacToe.Engines
{

	public class GamePlayEngine
	{

		private readonly Board board;

		public GamePlayEngine(Board board)
		{
			this.board = board;
		}

		public string AutoPlay()
		{
			var random = new Random();
			var emptyCells = board.Cells.Where(i => i.IsEmpty).ToList();
			var idx = random.Next(0, emptyCells.Count - 1);
			var cell = emptyCells[idx];
			return cell.Address;

		}

		public bool IsPlayable()
		{

			if ( board.IsEmpty )
				return true;

			if ( board.IsFull )
				return false;

			// Check all possible vectors
			// A Col
			if (IsWinningVector(board.A1, board.A2, board.A3) )
				return false;

			// B Col
			if (IsWinningVector(board.B1, board.B2, board.B3) )
				return false;

			// C Col
			if (IsWinningVector(board.C1, board.C2, board.C3) )
				return false;

			// 1 Row
			if (IsWinningVector(board.A1, board.B1, board.C1) )
				return false;

			// 2 Row
			if (IsWinningVector(board.A2, board.B2, board.C2) )
				return false;

			// 3 Row
			if (IsWinningVector(board.A3, board.B3, board.C3) )
				return false;

			// Right Diagonal
			if (IsWinningVector(board.A1, board.B2, board.C3) )
				return false;

			// Left Diagonal
			if (IsWinningVector(board.A3, board.B2, board.C1) )
				return false;

			return true;

		}

		public string FindWinnerGamePiece()
		{

			if ( IsPlayable() )
				throw new ApplicationException("Game is still playable.");

			// Search by Row
			if ( IsWinningVector(board.A1, board.A2, board.A3) )
				return board.A1.GamePiece;
			if ( IsWinningVector(board.B1, board.B2, board.B3) )
				return board.B1.GamePiece;
			if ( IsWinningVector(board.C1, board.C2, board.C3) )
				return board.C1.GamePiece;

			// Search by Column
			if ( IsWinningVector(board.A1, board.B1, board.C1) )
				return board.A1.GamePiece;
			if ( IsWinningVector(board.A2, board.B2, board.C2) )
				return board.A2.GamePiece;
			if ( IsWinningVector(board.A3, board.B3, board.C3) )
				return board.A3.GamePiece;

			// Search Diagonals
			if ( IsWinningVector(board.A1, board.B2, board.C3) )
				return board.A1.GamePiece;
			if ( IsWinningVector(board.A3, board.B2, board.C1) )
				return board.A3.GamePiece;

			return "";

		}

		private bool IsWinningVector(params Cell[] vectors)
		{

			foreach ( var vector in vectors )
				Trace.Write(vector.Address + " [" + vector.GamePiece.PadRight(1, ' ') + "] ");

			if (vectors.Any(i => string.IsNullOrWhiteSpace(i.GamePiece)))
			{
				Trace.WriteLine($" Result: {false}  Has empty cell.");
				return false;
			}

			var results = vectors.Skip(1).All(vector => vectors[0].GamePiece == vector.GamePiece);
			foreach ( var vector in vectors)
				Trace.Write(vector.Address + " [" + vector.GamePiece.PadRight(1,' ') + "] ");
			Trace.WriteLine($" Result: {results} Unmatched.");

			return results;
		}

		public bool IsCellOpen(string address)
		{
			var cell = board.Cells.FirstOrDefault(i => i.Address == address);
			if (cell == null)
				throw new ArgumentException("Address not found.");
			return string.IsNullOrWhiteSpace(cell.GamePiece);
		}

	}
}