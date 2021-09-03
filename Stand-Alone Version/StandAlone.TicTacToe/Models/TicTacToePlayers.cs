using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Models
{

	public interface IPlayers<T, in U>
	{

		IList<T> AllPlayers { get; }
		T CurrentPlayer { get; }
		void IncrementPlayer();
		T ByGamePiece(U gamePiece);

	}

	public class TicTacToePlayers : IPlayers<TicTacToePlayer, string>
	{

		private readonly List<TicTacToePlayer> players;
		private int currentPlayerPointer;

		public IList<TicTacToePlayer> AllPlayers => players;

		public TicTacToePlayer CurrentPlayer => FindCurrentPlayer();

		public TicTacToePlayers(params TicTacToePlayer[] playerArgs)
		{
			players = new List<TicTacToePlayer>();
			foreach ( var player in playerArgs )
				players.Add(player);
		}

		public void IncrementPlayer()
		{
			currentPlayerPointer++;
			if ( currentPlayerPointer >= players.Count )
				currentPlayerPointer = 0;
		}

		private TicTacToePlayer FindCurrentPlayer()
		{

			if (AllPlayers.Count == 0)
				return null;

			if ( currentPlayerPointer >= players.Count )
				currentPlayerPointer = 0;

			return players[currentPlayerPointer];
		}

		public TicTacToePlayer ByGamePiece(string gamePiece)
		{
			var player = players.FirstOrDefault(i => i.GamePiece == gamePiece);
			return player;
		}

	}

}