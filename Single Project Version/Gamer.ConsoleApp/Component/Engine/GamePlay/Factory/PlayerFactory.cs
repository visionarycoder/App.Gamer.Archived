using System;
using Gamer.Component.Access.Player;

namespace Gamer.Component.Engine.GamePlay.Factory
{

	public static class PlayerFactory
	{
	
		public static Player Create(string name, string gamePiece, bool isMachine = false)
		{
		
			var target = new Player
			{
				Id = Guid.NewGuid(),
				Name = name,
				GamePiece = gamePiece,
				IsMachine = isMachine
			};
			return target;

		}

	}

}
