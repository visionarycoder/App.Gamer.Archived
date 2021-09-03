using System;

namespace TicTacToe.Models
{

	public interface IPlayer
	{
		string Name { get; }
		bool IsMachine { get; }
		string GamePiece { get; }
	}


	public class Player : IPlayer
	{

		public string Name { get; }
		public bool IsMachine { get; }
		public string GamePiece { get; }

		public Player(string name, string gamePiece, bool isMachine = false )
		{
			Name = name;
			GamePiece = gamePiece;
			IsMachine = isMachine;
		}
		
	}

}