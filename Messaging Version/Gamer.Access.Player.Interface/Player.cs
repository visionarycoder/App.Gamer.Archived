using System;

namespace Gamer.Access.Player.Interface
{
	public class Player 
	{

		public Guid Id { get; init;  }
		public string Name { get; init; }
		public bool IsMachine { get; init; }
		public string GamePiece { get; init; }

	}

	
}

